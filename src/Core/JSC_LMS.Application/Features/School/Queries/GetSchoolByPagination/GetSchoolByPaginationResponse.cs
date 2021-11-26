using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.School.Queries.GetSchoolByPagination
{
    public class GetSchoolByPaginationResponse
    {
        public IEnumerable<GetSchoolByPaginationDto> GetSchoolByPaginationDto { get; set; }
        public int Count { get; set; }
    }
}
