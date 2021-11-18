using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.ParentsFeature.Commands.CreateParents
{
    public class CreateParentsCommand : IRequest<Response<CreateParentsDto>>
    {
        public CreateParentsCommand(CreateParentsDto _createParentDto)
        {
            createParentDto = _createParentDto;
        }
        public CreateParentsDto createParentDto { get; set; }
    }
}
