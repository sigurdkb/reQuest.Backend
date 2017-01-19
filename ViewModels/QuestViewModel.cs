using System;
using reQuest.Backend.Entities;

namespace reQuest.Backend.ViewModels
{
    public class QuestViewModel
    {
        public string Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
        public QuestState State { get; set; }
        public DateTime Ends { get; set; }
		public Topic Topic { get; set; }
        public Player Owner { get; set; }
        public bool IsOwner { get; set; }
   }
}
