using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.Section.Commands.CreateSection
{
   public class CreateSectionDto
    {
        [Required(ErrorMessage = "School Is Required")]
        public int SchoolId { get; set; }

        [Required(ErrorMessage = "Class Is Required")]
        public int ClassId { get; set; }

        [StringLength(100, ErrorMessage = "Section Name should not be more than 100 characters")]
        [Required(ErrorMessage = "Section Name Is Required")]
        public string SectionName { get; set; }
        public bool IsActive { get; set; }
    }
}
