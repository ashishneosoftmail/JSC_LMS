using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Subject.Queries.GetSubjectFilter
{
  public  class GetSubjectFilterDto
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }

        public ClassDto ClassId { get; set; }
        public SectionDto SectionId { get; set; }

        public SchoolDto SchoolId { get; set; }
        public DateTime CreatedDate { get; set; }
      
        public ClassDto Class { get; set; }
        public SchoolDto School { get; set; }
        // public int SectionId { get; set; }
        public SectionDto Section { get; set; }
        public bool IsActive { get; set; }
    }
}
