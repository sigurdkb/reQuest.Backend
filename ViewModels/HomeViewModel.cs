using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using reQuest.Backend.Entities;

namespace reQuest.Backend.ViewModels
{
    public class HomeViewModel
    {
        public List<TeamViewModel> Teams { get; set; }
        public List<QuestViewModel> Quests { get; set; }
    }
}
