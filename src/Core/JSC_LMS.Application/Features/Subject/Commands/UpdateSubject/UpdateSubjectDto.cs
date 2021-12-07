using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.Subject.Commands.UpdateSubject
{
   public class UpdateSubjectDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "School is mandatory")]
        public int SchoolId { get; set; }
        [Required(ErrorMessage = "Class is mandatory")]
        public int ClassId { get; set; }

        [Required(ErrorMessage = "Section is mandatory")]
        public int SectionId { get; set; }

        [StringLength(100, ErrorMessage = "Subject should not be more than 100 characters")]
        [Required(ErrorMessage = "Subject Name is mandatory")]
        public string SubjectName { get; set; }
        public bool IsActive { get; set; }
    }
}
