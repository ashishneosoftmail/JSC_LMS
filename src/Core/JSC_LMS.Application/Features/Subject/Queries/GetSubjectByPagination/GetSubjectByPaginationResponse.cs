using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Subject.Queries.GetSubjectByPagination
{
    public class GetSubjectByPaginationResponse
    {
        public IEnumerable<GetSubjectListPaginationDto> GetSubjectListPaginationDto { get; set; }
        public int Count { get; set; }
    }
}
