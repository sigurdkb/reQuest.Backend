using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using reQuest.Backend.Entities;

namespace reQuest.Backend.ViewModels
{
    public class CompetencyUpdateViewModel
    {
		public string Id { get; set; }
		public bool Active { get; set; }
    }
}
