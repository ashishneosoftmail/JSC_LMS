using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Academics.Commands.UpdateAcademic
{
   public class UpdateAcademicDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int SchoolId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public string Type { get; set; }
        public decimal CutOff { get; set; }
    }
}
