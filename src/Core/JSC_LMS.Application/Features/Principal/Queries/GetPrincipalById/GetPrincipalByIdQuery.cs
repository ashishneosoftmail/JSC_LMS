using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Principal.Queries.GetPrincipalById
{
    public class GetPrincipalByIdQuery : IRequest<Response<GetPrincipalByIdDto>>
    {
        public int Id { get; set; }
    }
}
