using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using reQuest.Backend.Entities;
using reQuest.Backend.Services;
using reQuest.Backend.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
            var quests = _repository.GetQuests(stateFilter:QuestState.Active | QuestState.Approved);
            var viewModel = Mapper.Map<IEnumerable<QuestViewModel>>(quests);

            return View(viewModel.OrderBy(q => q.Ends));
        }
    }
}
