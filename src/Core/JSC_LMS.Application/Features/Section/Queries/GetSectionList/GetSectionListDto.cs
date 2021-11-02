using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Section.Queries.GetSectionList
{
  public  class GetSectionListDto
    {
        public int Id { get; set; }
        public string SectionName { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public SchoolDto School { get; set; }
        public ClassDto Class { get; set; }

    }
}
