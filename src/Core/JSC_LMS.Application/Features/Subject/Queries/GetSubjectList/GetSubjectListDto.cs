using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Subject.Queries.GetSubjectList
{
   public class GetSubjectListDto
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }

        public ClassDto Class { get; set; }
        public SectionDto Section { get; set; }

        public SchoolDto School { get; set; }
    }
}
