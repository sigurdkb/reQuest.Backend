using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using reQuest.Backend.Services;
using reQuest.Backend.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace reQuest.Backend
{
    [Authorize]
    [Route("player")]
    public class PlayerController : Controller
    {
        private IreQuestRepository _repository;
        public PlayerController(IreQuestRepository repository)
        {
            _repository = repository;
        }
        // GET: /<controller>/
        [HttpGet()]
        public IActionResult Index()
        {
            return Ok();
        }
        [HttpGet("details")]
        public IActionResult Details()
        {
            var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var player = _repository.GetPlayerFromId(id);

            var viewModel = Mapper.Map<PlayerViewModel>(player);

            // Should be done in custom automapper
            foreach (var competency in viewModel.Competencies)
            {
                competency.TopicDisplayName = player.Competencies.Single(c => c.Id == competency.Id).Topic.DisplayName;
            }

            // Order competencies alpabetically
            viewModel.Competencies.Sort((x,y) => x.TopicDisplayName.CompareTo(y.TopicDisplayName));

            // Temporary verification
            viewModel.Nic = player.PushToken;

            return View(viewModel);
        }

        // POST: /player/details
        [HttpPost("details")]
        [ValidateAntiForgeryToken]
        public IActionResult Details(PlayerUpdateViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var player = _repository.GetPlayerFromId(id);

            // var result = Mapper.Map(viewModel, player);

            foreach (var competency in player.Competencies)
            {
                competency.Active = viewModel.Competencies.SingleOrDefault(c => c.Id == competency.Id).Active;
            }

            if (!_repository.Commit())
            {
                return StatusCode(500, "Something went wrong when trying to update the player");
            }


            return RedirectToAction("Details", "Player");
        }

        [HttpPost("registerpushtoken")]
        public IActionResult RegisterPushToken([FromBody] string pushToken)
        {
            if (string.IsNullOrEmpty(pushToken))
            {
                return BadRequest();
            }

            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var playerId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var player = _repository.GetPlayerFromId(playerId);
            player.PushToken = pushToken;

            if (!_repository.Commit())
            {
                return StatusCode(500, "Something went wrong when trying to update the player");
            }

                
            return Ok();
        }

        // [HttpPost("refreshtopics")]
        // public async Task<IActionResult> RefreshTopics()
        // {
        //     if (!User.Identity.IsAuthenticated)
        //     {
        //         return Unauthorized();
        //     }

        //     // Retrieve player information
        //     var playerId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
        //     var player = _repository.GetPlayerFromId(playerId);
        //     var accessToken = User.Claims.FirstOrDefault(c => c.Type == "Access_Token").Value;

        //     var client = new HttpClient();
        //     client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        //     var groupinfoResponse = await client.GetAsync($"{Startup.Configuration["dataporten:groups_api"]}/me/groups?showAll=true");
        //     var groupinfoResponseBody = await groupinfoResponse.Content.ReadAsStringAsync();
        //     var jsonGroupinfo = JsonConvert.DeserializeObject<List<DataportenGroupDto>>(groupinfoResponseBody);

        //     var topicGroups = jsonGroupinfo.FindAll(g => g.Type == "fc:fs:emne" && g.Membership.Fsroles.Contains("STUDENT"));
        //     if (topicGroups.Count != 0)
        //     {
        //         player.Competencies.Clear();
        //     }
        //     foreach (var topicGroup in topicGroups)
        //     {
        //         var topic = _repository.GetTopicFromExternalId(topicGroup.Id);
        //         if (topic == null)
        //         {
        //             topic = new Topic()
        //             {
        //                 ExternalId = topicGroup.Id,
        //                 ShortName = topicGroup.Id.Split(':')[5],
        //                 DisplayName = $"{topicGroup.Id.Split(':')[5]} {topicGroup.DisplayName}",
        //                 Url = topicGroup.Url
        //             };
        //             _repository.AddTopic(topic);
        //         }
        //         player.Competencies.Add(new Competency() { Topic = topic, Score = 0.0 });
        //     }

        //     if (!_repository.Commit())
        //     {
        //         return StatusCode(500, "Something went wrong when trying to update the player");
        //     }

                
        //     return RedirectToAction("Details", "Player");
        // }


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
