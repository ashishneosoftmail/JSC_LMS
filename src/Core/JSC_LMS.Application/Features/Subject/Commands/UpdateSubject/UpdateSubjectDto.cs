using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.Subject.Commands.UpdateSubject
{
   public class UpdateSubjectDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "School Is Required")]
        public int SchoolId { get; set; }
        [Required(ErrorMessage = "Class Is Required")]
        public int ClassId { get; set; }

        [Required(ErrorMessage = "Section Is Required")]
        public int SectionId { get; set; }

        [StringLength(100, ErrorMessage = "Subject should not be more than 100 characters")]
        [Required(ErrorMessage = "Subject Name Required")]
        public string SubjectName { get; set; }
        public bool IsActive { get; set; }
    }
}
