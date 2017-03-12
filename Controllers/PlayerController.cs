using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public IActionResult RegisterPushToken(string pushToken)
        {
            if (string.IsNullOrEmpty(pushToken))
            {
                return BadRequest();
            }

            if (!User.Identity.IsAuthenticated)
            {
                return Json("Push token can only be registereed to authenticated players");
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
