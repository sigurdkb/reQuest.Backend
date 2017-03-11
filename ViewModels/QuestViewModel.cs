using System;
using System.ComponentModel.DataAnnotations;
using reQuest.Backend.Entities;

namespace reQuest.Backend.ViewModels
{
    public class QuestViewModel
    {
        public string Id { get; set; }
		public string Title { get; set; }
        [Display(Name = "Beskrivelse")]
		public string Description { get; set; }
        public QuestState State { get; set; }
        [Display(Name = "Tidsfrist")]
        public TimeSpan Timeout { get; set; }
		public Topic Topic { get; set; }
        public Player Owner { get; set; }
        public bool IsOwner { get; set; }
   }
}
