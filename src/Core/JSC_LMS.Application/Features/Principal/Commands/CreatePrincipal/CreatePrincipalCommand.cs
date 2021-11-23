using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Principal.Commands.CreatePrincipal
{
    public class CreatePrincipalCommand : IRequest<Response<CreatePrincipalDto>>
    {
        public CreatePrincipalCommand(CreatePrincipalDto _createPrincipalDto)
        {
            createPrincipalDto = _createPrincipalDto;
        }
        public CreatePrincipalDto createPrincipalDto { get; set; }
    }
}
