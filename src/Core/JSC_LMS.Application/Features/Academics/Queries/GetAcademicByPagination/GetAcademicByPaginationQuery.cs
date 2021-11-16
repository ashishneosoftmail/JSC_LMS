using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Academics.Queries.GetAcademicByPagination
{
    public class GetAcademicByPaginationQuery : IRequest<Response<GetAcademicListByPaginationResponse>>
    {
        public int page { get; set; }
        public int size { get; set; }
    }
}
