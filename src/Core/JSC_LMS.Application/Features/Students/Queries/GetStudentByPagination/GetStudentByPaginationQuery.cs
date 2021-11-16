using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Students.Queries.GetStudentByPagination
{
    public class GetStudentByPaginationQuery : IRequest<Response<GetStudentListByPaginationResponse>>
    {
        public int page { get; set; }
        public int size { get; set; }
    }
}
