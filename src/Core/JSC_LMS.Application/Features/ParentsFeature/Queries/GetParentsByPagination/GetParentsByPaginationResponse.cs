using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.ParentsFeature.Queries.GetParentsByPagination
{
    public class GetParentsByPaginationResponse
    {
        public IEnumerable<GetParentsByPaginationDto> GetParentsListPaginationDto { get; set; }
        public int Count { get; set; }
    }
}
