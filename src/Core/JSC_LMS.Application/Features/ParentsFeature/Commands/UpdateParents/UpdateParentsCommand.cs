using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.ParentsFeature.Commands.UpdateParents
{
    public class UpdateParentsCommand : IRequest<Response<int>>
    {
        public UpdateParentsCommand(UpdateParentsDto _updateParentsDto)
        {
            updateParentsDto = _updateParentsDto;
        }
        public UpdateParentsDto updateParentsDto { get; set; }
    }
}
