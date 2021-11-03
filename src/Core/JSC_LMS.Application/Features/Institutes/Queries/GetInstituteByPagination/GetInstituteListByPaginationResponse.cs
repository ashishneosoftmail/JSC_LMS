using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Institutes.Queries.GetInstituteByPagination
{
   public class GetInstituteListByPaginationResponse
    {
        public IEnumerable<GetInstituteListPaginationDto> GetInstituteListPaginationDto { get; set; }
        public int Count { get; set; }
    }
}
