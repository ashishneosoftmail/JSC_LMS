using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Class.Commands.UpdateClass
{
    public class UpdateClassCommand : IRequest<Response<int>>
    {
        public UpdateClassCommand(UpdateClassDto _updateClassDto)
        {
            updateClassDto = _updateClassDto;
        }
        public UpdateClassDto updateClassDto { get; set; }
    }
}
