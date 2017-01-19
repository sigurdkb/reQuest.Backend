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
    [Route("player")]
    public class PlayerController : Controller
    {
        private IreQuestRepository _reQuestRepo;
        public PlayerController(IreQuestRepository reQuestRepo)
        {
            _reQuestRepo = reQuestRepo; 
        }
        // GET: /<controller>/
        [HttpGet()]
        public IActionResult Index()
        {
            return Ok();
        }
        [Authorize]
        [HttpGet("details")]
        public IActionResult Details()
        {
            var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var player = _reQuestRepo.GetPlayerFromId(id);

            var viewModel = Mapper.Map<PlayerViewModel>(player);

            return View(viewModel);
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
