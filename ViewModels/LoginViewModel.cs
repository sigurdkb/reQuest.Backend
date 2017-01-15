using System.ComponentModel.DataAnnotations;

namespace reQuest.Backend.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [MinLength(4), MaxLength(12)]
        [Display(Name = "@uia.no")]
        public string Username { get; set; }
    }
}
