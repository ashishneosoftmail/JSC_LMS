using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Class.Queries.GetClassByPagination
{
   public class GetClassByPaginationQuery : IRequest<Response<GetClassListByPaginationResponse>>
    {
        public int page { get; set; }
        public int size { get; set; }
    }
}
