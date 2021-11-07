using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Class.Queries.GetClassByPagination
{
  public class GetClassListByPaginationResponse
    {
        public IEnumerable<GetClassListPaginationDto> GetClassListPaginationDto { get; set; }
        public int Count { get; set; }
    }
}
