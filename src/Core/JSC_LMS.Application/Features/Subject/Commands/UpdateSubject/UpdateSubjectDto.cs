using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Subject.Commands.UpdateSubject
{
   public class UpdateSubjectDto
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public int ClassId { get; set; }

        public int SectionId { get; set; }

        public string SubjectName { get; set; }
        public bool IsActive { get; set; }
    }
}
