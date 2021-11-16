using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.Academics.Commands.CreateAcademic
{
   public class CreateAcademicDto
    {
       
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
