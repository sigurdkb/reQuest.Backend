using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using reQuest.Backend.Entities;
using reQuest.Backend.Services;
using reQuest.Backend.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace reQuest.Backend.Controllers
{
    [Route("quest")]
    [Authorize]
    public class QuestController : Controller
    {
        private IreQuestRepository _repository;
        public QuestController(IreQuestRepository repository)
        {
            _repository = repository;
        }

        // GET: /quest/
        [HttpGet()]
        public IActionResult Index()
        {
            var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var currentPlayer = _repository.GetPlayerFromId(id);

            var quests = _repository.GetPlayerQuests(currentPlayer);
            var viewModel = Mapper.Map<IEnumerable<QuestViewModel>>(quests);

            //TODO: Implement in custom automapper value resolver https://github.com/AutoMapper/AutoMapper/wiki/Custom-value-resolvers
            foreach (var questView in viewModel)
            {
                questView.IsOwner = questView.Owner == currentPlayer;
            }

            return View(viewModel);
        }

        // GET: /quest/create
        [HttpGet("create")]
        public IActionResult Create()
        {
            var viewModel = new QuestCreateViewModel()
            {
                AllTopics = GetTopics(),
                AllTimeouts = GetTimeouts()
            };
                        
            return View(viewModel);
        }
        
        // POST: /quest/create
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(QuestCreateViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            if (!_repository.TopicExists(viewModel.TopicId))
            {
                ModelState.AddModelError("TopicId", "The chosen topic is not in the list of available topics");
            }
            if (ModelState.IsValid)
            {
                var playerId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

                var quest = new Quest()
                {
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    Created = System.DateTime.UtcNow,
                    Ends = System.DateTime.UtcNow.Add(viewModel.Timeout),
                    Topic = _repository.GetTopicFromId(viewModel.TopicId),
                    Owner = _repository.GetPlayerFromId(playerId)
                };
                
                _repository.AddQuest(quest);
                _repository.Commit();

                return RedirectToAction("Index");
            }

            viewModel.AllTimeouts = GetTimeouts();
            viewModel.AllTopics = GetTopics();

            return View(viewModel);
        }
        
        List<SelectListItem> GetTimeouts()
        {
            var selectListItems = new List<SelectListItem>();

            for (int i = 2; i <= 6; i++)
            {
                selectListItems.Add(new SelectListItem() 
                { 
                    Value = $"0:{i * 5}", 
                    Text = $"{i * 5} minutes" 
                });
            }
            return selectListItems;

        }
        List<SelectListItem> GetTopics()
        {
            var selectListItems = new List<SelectListItem>();

            var topics = _repository.GetTopics().ToList();
            foreach (var topic in topics)
            {
                selectListItems.Add(new SelectListItem() 
                { 
                    Value = topic.Id, 
                    Text = topic.DisplayName
                });
            }
            return selectListItems;
        }
    }

}
