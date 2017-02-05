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

            return View(viewModel.OrderBy(q => q.State));
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
                if (!_repository.Commit())
                {
                    return StatusCode(500, "Something went wrong when trying to create the reQuest");
                }

                return RedirectToAction("Index");
            }

            viewModel.AllTimeouts = GetTimeouts();
            viewModel.AllTopics = GetTopics();

            return View(viewModel);
        }

        // POST: /quest/approve
        [HttpPost("approve")]
        [ValidateAntiForgeryToken]
        public IActionResult Approve(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var quest = _repository.GetQuest(id);
            if (quest == null)
            {
                return NotFound();
            }

            var winner = _repository.GetPlayerFromId(quest.WinnerId);
            winner.Competencies.SingleOrDefault(c => c.TopicId == quest.TopicId).Score += 10;
            quest.State = QuestState.Approved;

            if (!_repository.Commit())
            {
                return StatusCode(500, "Something went wrong when trying to approve the reQuest");
            }

            return RedirectToAction("Index");
        }
        // POST: /quest/take
        [HttpPost("take")]
        [ValidateAntiForgeryToken]
        public IActionResult Take(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var quest = _repository.GetQuest(id);
            if (quest == null)
            {
                return NotFound();
            }

            var playerId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            quest.Winner = _repository.GetPlayerFromId(playerId);
            quest.State = QuestState.Done;

            if (!_repository.Commit())
            {
                return StatusCode(500, "Something went wrong when trying to take the reQuest");
            }

            return RedirectToAction("Index");
        }

        // POST: /quest/delete
        [HttpPost("delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var quest = _repository.GetQuest(id);
            if (quest == null)
            {
                return NotFound();
            }

            _repository.DeleteQuest(quest);

            if (!_repository.Commit())
            {
                return StatusCode(500, "Something went wrong when trying to delete the reQuest");
            }

            return RedirectToAction("Index");
        }

        // GET: /quest/reactivate
        [HttpGet("reactivate")]
        public IActionResult Reactivate(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var quest = _repository.GetQuest(id);
            if (quest == null)
            {
                return NotFound();
            }
            
            var viewModel = new QuestReactivateViewModel()
            {
                Title = quest.Title,
                Description = quest.Description,
                Topic = quest.Topic.DisplayName,
                AllTimeouts = GetTimeouts(),
            };
                        
            return View(viewModel);
        }
        // POST: /quest/reactivate
        [HttpPost("reactivate")]
        [ValidateAntiForgeryToken]
        public IActionResult Reactivate(QuestReactivateViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            var quest = _repository.GetQuest(viewModel.Id);
            if (quest == null)
            {
                return NotFound();
            }

            quest.Title = viewModel.Title;
            quest.Description = viewModel.Description;
            quest.Ends = System.DateTime.UtcNow.Add(viewModel.Timeout);
            quest.State = QuestState.Active;

            if (!_repository.Commit())
            {
                return StatusCode(500, "Something went wrong when trying to reactivate the reQuest");
            }

            return RedirectToAction("Index");
        }

        // Remote field validation of QuestCreateViewModel.TopicId
        // GET: /quest/create
        [HttpGet("verifytopicid")]
        public IActionResult VerifyTopicId(string topicId)
        {
            if (string.IsNullOrEmpty(topicId))
            {
                return BadRequest();
            }
            
            if (!_repository.TopicExists(topicId))
            {
                return Json("The chosen topic is not in the list of available topics");
            }
        return Json(true);
        }
 
        
        List<SelectListItem> GetTimeouts()
        {
            var selectListItems = new List<SelectListItem>();

            for (int i = 2; i <= 6; i++)
            {
                selectListItems.Add(new SelectListItem() 
                { 
                    Value = $"0:{i * 5}", 
                    Text = $"{i * 5} minutter" 
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
