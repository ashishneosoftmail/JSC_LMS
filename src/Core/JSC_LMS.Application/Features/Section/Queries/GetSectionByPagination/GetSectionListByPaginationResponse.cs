using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Section.Queries.GetSectionByPagination
{
   public class GetSectionListByPaginationResponse
    {
        public IEnumerable<GetSectionListPaginationDto> GetSectionListPaginationDto { get; set; }
        public int Count { get; set; }
    }
}
