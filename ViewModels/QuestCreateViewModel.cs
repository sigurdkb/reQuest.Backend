using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace reQuest.Backend.ViewModels
{
    public class QuestCreateViewModel
    {
        [Required(ErrorMessage = "Please provide a title")]
        [MinLength(8), MaxLength(128)]
		public string Title { get; set; }

        [Required(ErrorMessage = "Please provide a desctiption of the problem")]
        [MinLength(8), MaxLength(1024)]
		public string Description { get; set; }

        [Required]
        [DataType(DataType.Time, ErrorMessage = "You need to set the timeout")]
        public TimeSpan Timeout { get; set; }
        [Required]
        [Display(Name = "Topic")]
        [Remote("VerifyTopicId", "Quest")]
		public string TopicId { get; set; }
        public List<SelectListItem> AllTimeouts { get; set; } = new List<SelectListItem>(); 
        public List<SelectListItem> AllTopics { get; set; } = new List<SelectListItem>();
   }
}
