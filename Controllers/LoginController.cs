using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using reQuest.Backend.Entities;
using reQuest.Backend.Services;
using reQuest.Backend.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace reQuest.Backend
{
    [Route("login")]
    public class LoginController : Controller
    {
        private IreQuestRepository _reQuestRepo;
        public LoginController(IreQuestRepository reQuestRepo)
        {
            _reQuestRepo = reQuestRepo; 
        }
        // GET: /<controller>/
        [HttpGet()]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("callback")]
        public async Task<IActionResult> Callback(string code, string state = "", string error = "")
        {
            // Create and authenticate client
            var client = new HttpClient();
            var authString = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Startup.Configuration["dataporten:client_id"]}:{Startup.Configuration["dataporten:client_secret"]}"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

            // Configure OAuth POST request
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", Startup.Configuration["dataporten:redirect_uri"]),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("client_id", Startup.Configuration["dataporten:client_id"]),
            });

            // Retrieve OAuth response
            var response = await client.PostAsync(Startup.Configuration["dataporten:token"], content);
            var responseBody = await response.Content.ReadAsStringAsync();
            var jsonToken = JsonConvert.DeserializeObject<DataportenTokenDto>(responseBody);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jsonToken.Access_Token);
            var userinfoResponse = await client.GetAsync(Startup.Configuration["dataporten:userinfo"]);
            // var userinfoResponse = await client.GetAsync("https://auth.dataporten.no/openid/userinfo");
            var userinfoResponseBody = await userinfoResponse.Content.ReadAsStringAsync();
            var jsonUserinfo = JsonConvert.DeserializeAnonymousType(userinfoResponseBody, new { User = new DataportenUserDto(), Audience = string.Empty});

            // Check if user already exists. If not, create and import user data.
            var player = _reQuestRepo.GetPlayerFromExternalId(jsonUserinfo.User.UserId);
            if (player == null)
            {
                player = new Player()
                {
                    ExternalId = jsonUserinfo.User.UserId,
                    Username = jsonUserinfo.User.UserId_Sec.Find(s => s.Contains("feide")).Split(':')[1],
                    Name = jsonUserinfo.User.Name,
                    Email = jsonUserinfo.User.Email,
                    Team = _reQuestRepo.GetRandomTeam()
                };

                _reQuestRepo.AddPlayer(player);
            
                // Import course data.
                var groupinfoResponse = await client.GetAsync($"{Startup.Configuration["dataporten:groups_api"]}/me/groups?showAll=true");
                var groupinfoResponseBody = await groupinfoResponse.Content.ReadAsStringAsync();
                var jsonGroupinfo = JsonConvert.DeserializeObject<List<DataportenGroupDto>>(groupinfoResponseBody);

                var topicGroups = jsonGroupinfo.FindAll(g => g.Type == "fc:fs:emne" && g.Membership.Fsroles.Contains("STUDENT"));
                foreach (var topicGroup in topicGroups)
                {
                    var topic = _reQuestRepo.GetTopicFromExternalId(topicGroup.Id);
                    if (topic == null)
                    {
                        topic = new Topic()
                        {
                            ExternalId = topicGroup.Id,
                            ShortName = topicGroup.Id.Split(':')[5],
                            DisplayName = $"{topicGroup.Id.Split(':')[5]} {topicGroup.DisplayName}",
                            Url = topicGroup.Url
                        };
                        _reQuestRepo.AddTopic(topic);
                    }
                    player.Competencies.Add(new Competency() { Topic = topic, Score = 0.0 });
                }
                _reQuestRepo.Commit();
            }
            
            // Create claims
            var claims = new List<Claim> {
            new Claim("Access_Token", jsonToken.Access_Token, ClaimValueTypes.String, Startup.Configuration["dataporten:authority"]),
            new Claim("Id_Token", jsonToken.Id_Token, ClaimValueTypes.String, Startup.Configuration["dataporten:authority"]),
            new Claim(ClaimTypes.NameIdentifier, player.Id, ClaimValueTypes.String, Startup.Configuration["dataporten:authority"]),
            new Claim(ClaimTypes.Name, player.Name, ClaimValueTypes.String, Startup.Configuration["dataporten:authority"]),
            new Claim(ClaimTypes.Upn, player.Username, ClaimValueTypes.String, Startup.Configuration["dataporten:authority"])
            
            };

            var userIdentity = new ClaimsIdentity(claims, "Passport");

            var userPrincipal = new ClaimsPrincipal(userIdentity);

            await HttpContext.Authentication.SignInAsync("reQuestCookie", userPrincipal,
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                    IsPersistent = false,
                    AllowRefresh = false
                });



            return RedirectToAction("Index", "Home");
        }

        [HttpPost("")]
        public IActionResult Login(string returnUrl = null, string username = null)
        {
            if (!User.Identity.IsAuthenticated)
            {
                var url = $"{Startup.Configuration["dataporten:authorization"]}" +
                      $"?client_id={Startup.Configuration["dataporten:client_id"]}" +
                      $"&redirect_uri={Startup.Configuration["dataporten:redirect_uri"]}" +
                      $"&response_type=code" +
                      $"&scope=openid+profile+groups+userid+userid-feide+email" +
                      @"&acresponse={""type"":""saml"",""id"":""https://idp.feide.no"",""subid"":""uia.no""}";

                return Redirect(url);
            }

            if (!Url.IsLocalUrl(returnUrl))
            {
                return RedirectToAction("Index", "Home");
            }

            return Redirect(returnUrl);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("reQuestCookie");

            return RedirectToAction("Index", "Home");
        }

        // [HttpGet("register")]
        // public IActionResult Register()
        // {
        //     return View();
        // }
        // [HttpPost("register")]
        // public async Task<IActionResult> Register(string code, string state = "", string error = "")
        // {

        // }

    }
}
