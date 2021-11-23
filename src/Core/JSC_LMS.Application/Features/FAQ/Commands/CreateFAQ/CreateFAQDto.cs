using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.FAQ.Commands.CreateFAQ
{
   public class CreateFAQDto
    {
        [Required(ErrorMessage = "Category Is Required")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "FAQ Title Is Required")]
        [StringLength(200, ErrorMessage = "Doc Title length should not be more than 200 characters")]
        public string FAQTitle { get; set; }
        [Required(ErrorMessage = "Sub Title Is Required")]
        [StringLength(150, ErrorMessage = "Sub Title length should not be more than 150 characters")]
        public string Question { get; set; }
        [Required(ErrorMessage = "Slug Url Is Required")]
        [StringLength(150, ErrorMessage = "Slug Url length should not be more than 150 characters")]
       
        public string Content { get; set; }
        public bool IsActive { get; set; }
    }
}
