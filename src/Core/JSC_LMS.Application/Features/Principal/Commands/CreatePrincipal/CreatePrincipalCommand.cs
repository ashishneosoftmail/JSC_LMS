using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Principal.Commands.CreatePrincipal
{
    public class CreatePrincipalCommand : IRequest<Response<CreatePrincipalDto>>
    {
        public CreatePrincipalDto createPrincipalDto { get; set; }
    }
}
