using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Principal.Queries.GetPrincipalByUserId
{
   public class GetPrincipalByUserIdQuery : IRequest<Response<GetPrincipalByUserIdDto>>
    {
        public string UserId { get; set; }
    }
}
