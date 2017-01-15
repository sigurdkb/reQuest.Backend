using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using reQuest.Backend.Services;
using reQuest.Backend.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace reQuest.Backend.Controllers
{
    [Route("team")]
    [Authorize]
    public class TeamController : Controller
    {
        private IreQuestRepository _repository;
        public TeamController(IreQuestRepository repository)
        {
            _repository = repository;
        }
        // GET: /<controller>/
        [HttpGet()]
        public IActionResult Index()
        {
            var teams = _repository.GetTeams();
            var viewModel = Mapper.Map<IEnumerable<TeamViewModel>>(teams);

            return View(viewModel);
        }
    }
}
