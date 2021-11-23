using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Subject.Queries.GetSubjectByPagination
{
   public class GetSubjectByPaginationQuery : IRequest<Response<GetSubjectByPaginationResponse>>
    {
        public int page { get; set; }
        public int size { get; set; }
    }
}
