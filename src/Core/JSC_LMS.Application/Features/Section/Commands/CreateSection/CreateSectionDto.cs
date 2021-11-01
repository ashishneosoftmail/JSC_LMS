using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Section.Commands.CreateSection
{
   public class CreateSectionDto
    {
        public int SchoolId { get; set; }

        public int ClassId { get; set; }

        public string SectionName { get; set; }
        public bool IsActive { get; set; }
    }
}
