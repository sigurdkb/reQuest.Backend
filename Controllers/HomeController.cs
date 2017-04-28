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
            var viewModel = new HomeViewModel();

            var teams = _repository.GetTeams();
            var teamsViewModel = Mapper.Map<IEnumerable<TeamViewModel>>(teams);

            var quests = _repository.GetQuests(stateFilter:QuestState.Active);
            var questsViewModel = Mapper.Map<IEnumerable<QuestViewModel>>(quests);

            //TODO: Implement in custom automapper value resolver https://github.com/AutoMapper/AutoMapper/wiki/Custom-value-resolvers
            foreach (var questView in questsViewModel)
            {
                questView.Timeout = quests.SingleOrDefault(q => q.Id == questView.Id).Ends.Subtract(System.DateTime.UtcNow);
            }
            viewModel.Teams = teamsViewModel.OrderByDescending(t => t.Score).ToList();
            viewModel.Quests = questsViewModel.OrderBy(q => q.Timeout).ToList();

            return View(viewModel);
        }
    }
}
