using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.KnowledgeBase.Commands.UpdateKnowledgeBase
{
    public class UpdateKnowledgeBaseDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Category Is Required")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Doc Title Is Required")]
        [StringLength(200, ErrorMessage = "Doc Title length should not be more than 200 characters")]
        public string DocTitle { get; set; }
        [Required(ErrorMessage = "Sub Title Is Required")]
        [StringLength(150, ErrorMessage = "Sub Title length should not be more than 150 characters")]
        public string SubTitle { get; set; }
        [Required(ErrorMessage = "Slug Url Is Required")]
        [StringLength(150, ErrorMessage = "Slug Url length should not be more than 150 characters")]
        public string SlugUrl { get; set; }
        [Required(ErrorMessage = "Add Content Is Required")]
        public string AddContent { get; set; }
        public bool IsActive { get; set; }
    }
}
