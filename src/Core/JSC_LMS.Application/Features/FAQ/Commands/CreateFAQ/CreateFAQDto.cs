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
        [Required(ErrorMessage = "Question Is Required")]
        [StringLength(150, ErrorMessage = "Question length should not be more than 150 characters")]
        public string Question { get; set; }
        [Required(ErrorMessage = "Content Is Required")]
        [StringLength(150, ErrorMessage = "Content length should not be more than 350 characters")]
       
        public string Content { get; set; }
        public bool IsActive { get; set; }
    }
}
