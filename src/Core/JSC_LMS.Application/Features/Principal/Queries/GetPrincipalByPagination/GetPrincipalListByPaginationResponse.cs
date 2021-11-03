using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Principal.Queries.GetPrincipalByPagination
{
    public class GetPrincipalListByPaginationResponse
    {
        public IEnumerable<GetPrincipalListPaginationDto> GetPrincipalListPaginationDto { get; set; }
        public int Count { get; set; }
    }
}
