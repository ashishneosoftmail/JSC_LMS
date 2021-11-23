using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.FAQ.Commands.UpdateFAQ
{
   public class UpdateFAQDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Category Is Required")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "FAQTitle Is Required")]
        [StringLength(200, ErrorMessage = "FAQTitle length should not be more than 200 characters")]
        public string FAQTitle { get; set; }
        [Required(ErrorMessage = "QuestionIs Required")]
        [StringLength(150, ErrorMessage = "Question length should not be more than 150 characters")]
        public string Question { get; set; }
       
    
        public string Content { get; set; }
        public bool IsActive { get; set; }
    }
}
