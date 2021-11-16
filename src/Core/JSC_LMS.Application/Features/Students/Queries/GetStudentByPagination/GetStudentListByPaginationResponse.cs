using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Students.Queries.GetStudentByPagination
{
    
    public class GetStudentListByPaginationResponse
    {
        public IEnumerable<GetStudentListPaginationDto> GetStudentListPaginationDto { get; set; }
        public int Count { get; set; }
    }
}
