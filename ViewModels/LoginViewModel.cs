using System.ComponentModel.DataAnnotations;

namespace reQuest.Backend.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Du må oppgi brukernavnet ditt")]
        [MinLength(4, ErrorMessage = "For kort"), MaxLength(12, ErrorMessage = "For langt")]
        [Display(Name = "@uia.no")]
        public string Username { get; set; }
    }
}
