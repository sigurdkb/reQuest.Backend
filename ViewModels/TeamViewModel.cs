using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using reQuest.Backend.Entities;

namespace reQuest.Backend.ViewModels
{
    public class TeamViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<PlayerViewModel> Players { get; set; } = new List<PlayerViewModel>();
        public double Score
        {
            get
            {
                return Players.Sum(p => p.Score);
            }
        }
    }
}
