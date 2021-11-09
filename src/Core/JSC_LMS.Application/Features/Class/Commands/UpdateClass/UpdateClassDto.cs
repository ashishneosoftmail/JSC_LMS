using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.Class.Commands.UpdateClass
{
   public class UpdateClassDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "School Is Required")]
        public int SchoolId { get; set; }

          [StringLength(100, ErrorMessage = "class should not be more than 100 characters")]
        [Required(ErrorMessage = "Class Name Required")]
        public string ClassName { get; set; }
        public bool IsActive { get; set; }
    }
}
