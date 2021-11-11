using System;
using System.Collections.Generic;
using System.Text;
#region- Response for get teachers' list with custom pagination : by Shivani Goswami
namespace JSC_LMS.Application.Features.Teachers.Queries.GetTeacherByPagination
{
    public class GetTeacherListByPaginationResponse
    {
        public IEnumerable<GetTeacherListPaginationDto> GetTeacherListPaginationDto { get; set; }
        public int Count { get; set; }
    }
}
#endregion