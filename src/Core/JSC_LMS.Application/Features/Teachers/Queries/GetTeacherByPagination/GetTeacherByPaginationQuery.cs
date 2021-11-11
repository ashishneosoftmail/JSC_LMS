using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

#region - Query for Get Teacher By Pagination: by Shivani Goswami
namespace JSC_LMS.Application.Features.Teachers.Queries.GetTeacherByPagination
{
    public class GetTeacherByPaginationQuery : IRequest<Response<GetTeacherListByPaginationResponse>>
    {
        public int page { get; set; }
        public int size { get; set; }
    }
}
#endregion