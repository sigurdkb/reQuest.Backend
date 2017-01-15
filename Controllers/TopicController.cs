using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using reQuest.Backend.Services;
using reQuest.Backend.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace reQuest.Backend.Controllers
{
    [Route("topic")]
    [Authorize]
    public class TopicController : Controller
    {
        private IreQuestRepository _repository;
        public TopicController(IreQuestRepository repository)
        {
            _repository = repository;
        }
        // GET: /<controller>/
        [HttpGet()]
        public IActionResult Index()
        {
            var model = new TopicsViewModel();
            model.Topics = _repository.GetTopics();

            return View(model);
        }
    }
}
