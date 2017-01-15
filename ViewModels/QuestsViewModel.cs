using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using reQuest.Backend.Entities;

namespace reQuest.Backend.ViewModels
{
    public class QuestsViewModel
    {
        public IEnumerable<Quest> Quests { get; set; } = new List<Quest>();
        public DateTime Now { get; set; }
        public string Username { get; set; }
    }
}
