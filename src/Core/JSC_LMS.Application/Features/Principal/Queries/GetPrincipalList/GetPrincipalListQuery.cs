using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Principal.Queries.GetPrincipalList
{
    public class GetPrincipalListQuery : IRequest<Response<IEnumerable<GetPrincipalListDto>>>
    {
    }
}
