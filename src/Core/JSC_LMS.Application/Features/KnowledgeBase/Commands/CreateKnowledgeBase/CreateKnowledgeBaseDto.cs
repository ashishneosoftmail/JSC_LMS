using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.KnowledgeBase.Commands.CreateKnowledgeBase
{
    public class CreateKnowledgeBaseDto
    {
        [Required(ErrorMessage = "Category is mandatory")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Doc Title is mandatory")]
        [StringLength(200, ErrorMessage = "Doc Title length should not be more than 200 characters")]
        public string DocTitle { get; set; }
        [Required(ErrorMessage = "Sub Title is mandatory")]
        [StringLength(150, ErrorMessage = "Sub Title length should not be more than 150 characters")]
        public string SubTitle { get; set; }
        [Required(ErrorMessage = "Slug Url is mandatory")]
        [StringLength(150, ErrorMessage = "Slug Url length should not be more than 150 characters")]
        public string SlugUrl { get; set; }
        [Required(ErrorMessage = "Add Content is mandatory")]
        public string AddContent { get; set; }
        [Required(ErrorMessage = "Status is mandatory")]
        public bool IsActive { get; set; }
    }
}
