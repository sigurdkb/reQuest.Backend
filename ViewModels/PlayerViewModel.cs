using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using reQuest.Backend.Entities;

namespace reQuest.Backend.ViewModels
{
    public class PlayerViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Team Team { get; set; }
        public IEnumerable<Competency> Competencies { get; set; } = new List<Competency>();
        public double Score
        {
            get
            {
                return Competencies.Sum(c => c.Score);
            }
        }
    }
}
