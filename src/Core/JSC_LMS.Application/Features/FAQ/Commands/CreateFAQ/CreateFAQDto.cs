using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.FAQ.Commands.CreateFAQ
{
   public class CreateFAQDto
    {
        [Required(ErrorMessage = "Category is mandatory")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "FAQ Title is mandatory")]
        [StringLength(200, ErrorMessage = "Doc Title length should not be more than 200 characters")]
        public string FAQTitle { get; set; }
        [Required(ErrorMessage = "Question is mandatory")]
        [StringLength(150, ErrorMessage = "Question length should not be more than 150 characters")]
        public string Question { get; set; }
        [Required(ErrorMessage = "Content is mandatory")]
       
        public string Content { get; set; }
        [Required(ErrorMessage = "Status is mandatory")]
        public bool IsActive { get; set; }
    }
}
