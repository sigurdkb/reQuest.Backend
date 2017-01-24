using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using reQuest.Backend.Entities;

namespace reQuest.Backend.ViewModels
{
    public class CompetencyViewModel
    {
		public string Id { get; set; }
        public double Score { get; set; }
		public bool Active { get; set; }
		public string TopicDisplayName { get; set; }
    }
}
