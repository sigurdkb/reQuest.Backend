using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using reQuest.Backend.Entities;
using reQuest.Backend.Services;
using reQuest.Backend.ViewModels;

namespace reQuest.Backend.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private IreQuestRepository _repository;
        public HomeController(IreQuestRepository repository)
        {
            _repository = repository;
        }
        // GET: /<controller>/
        [HttpGet("")]
        public IActionResult Index()
        {
            var quests = _repository.GetQuests(stateFilter:QuestState.Active);
            var viewModel = Mapper.Map<IEnumerable<QuestViewModel>>(quests);

            return View(viewModel.OrderBy(q => q.Ends));
        }
    }
}
