using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Section.Queries.GetSectionFilter
{
    public class GetSectionFilterDto
    {
        public int Id { get; set; }
        public string SectionName { get; set; }




        public DateTime CreatedDate { get; set; }

        public ClassDto ClassId { get; set; }
        public SchoolDto SchoolId { get; set; }

        public SchoolDto SchoolName { get; set; }
        public ClassDto ClassName { get; set; }


        public bool IsActive { get; set; }
    }
}
