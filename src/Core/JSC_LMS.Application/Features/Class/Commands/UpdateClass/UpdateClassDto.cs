using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.Class.Commands.UpdateClass
{
   public class UpdateClassDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "School is mandatory")]
        public int SchoolId { get; set; }

          [StringLength(100, ErrorMessage = "Class should not be more than 100 characters")]
        [Required(ErrorMessage = "Class Name is mandatory")]
        public string ClassName { get; set; }
        public bool IsActive { get; set; }
    }
}
