using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Principal.Queries.GetPrincipalByPagination
{
    public class GetPrincipalByPaginationQuery : IRequest<Response<GetPrincipalListByPaginationResponse>>
    {
        public int page { get; set; }
        public int size { get; set; }
    }
}
