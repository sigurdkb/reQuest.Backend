using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using reQuest.Backend.Entities;

namespace reQuest.Backend.ViewModels
{
    public class PlayerViewModel
    {
        [Display(Name = "Navn")]
        public string Name { get; set; }
        [Display(Name = "Kallenavn")]
		public string Nic { get; set; }
        
        [Display(Name = "Epost")]
        public string Email { get; set; }
        [Display(Name = "Lag")]
        public Team Team { get; set; }
        public List<CompetencyViewModel> Competencies { get; set; } = new List<CompetencyViewModel>();
        [Display(Name = "Poeng")]
        public double Score
        {
            get
            {
                return Competencies.Sum(c => c.Score);
            }
        }
    }
}
