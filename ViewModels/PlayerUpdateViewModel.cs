using System.Collections.Generic;

namespace reQuest.Backend.ViewModels
{
    public class PlayerUpdateViewModel
    {
        public List<CompetencyUpdateViewModel> Competencies { get; set; } = new List<CompetencyUpdateViewModel>();
    }
}
