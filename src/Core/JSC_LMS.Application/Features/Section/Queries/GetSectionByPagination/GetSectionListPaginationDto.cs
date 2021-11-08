using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Section.Queries.GetSectionByPagination
{
    public class GetSectionListPaginationDto
    {
        public int Id { get; set; }
      
        public string SectionName { get; set; }
      
        public DateTime CreatedDate { get; set; }
       
        public bool IsActive { get; set; }
       
        public SchoolPaginationDto School { get; set; }

        public ClassPaginationDto Class { get; set; }
    }
}
