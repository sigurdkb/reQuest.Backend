using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace reQuest.Backend.ViewModels
{
    public class QuestCreateViewModel
    {
        [Required(ErrorMessage = "Du må oppgi en tittel")]
        [MinLength(8), MaxLength(128)]
        [Display(Name = "Tittel")]
		public string Title { get; set; }

        [Required(ErrorMessage = "Du må gi en beskrivelse av problemet")]
        [MinLength(8), MaxLength(1024)]
        [Display(Name = "Beskrivelse")]
		public string Description { get; set; }

        [Required]
        [DataType(DataType.Time, ErrorMessage = "Du må velge en tidsfrist")]
        [Display(Name = "Tidsfrist")]
        public TimeSpan Timeout { get; set; }
        [Required]
        [Remote("VerifyTopicId", "Quest")]
        [Display(Name = "Emne")]
		public string TopicId { get; set; }
        public List<SelectListItem> AllTimeouts { get; set; } = new List<SelectListItem>(); 
        public List<SelectListItem> AllTopics { get; set; } = new List<SelectListItem>();
   }
}
