using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Subject.Queries.GetSubjectFilter
{
  public  class GetSubjectFilterDto
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }

    
  

        public DateTime CreatedDate { get; set; }
      
        public ClassDto ClassId { get; set; }
        public SchoolDto SchoolId { get; set; }
        // public int SectionId { get; set; }
        public SectionDto SectionId { get; internal set; }
        public bool IsActive { get; set; }
    }
}
