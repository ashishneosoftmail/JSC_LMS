using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Academics.Queries.GetAcademicByPagination
{
    public class GetAcademicListByPaginationResponse
    {
        public IEnumerable<GetAcademicListPaginationDto> GetAcademicListPaginationDto { get; set; }
        public int Count { get; set; }
    }
}
