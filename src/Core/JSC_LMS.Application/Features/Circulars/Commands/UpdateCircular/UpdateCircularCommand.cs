using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Circulars.Commands.UpdateCircular
{
    public class UpdateCircularCommand : IRequest<Response<int>>
    {
        public UpdateCircularDto updateCircularDto { get; set; }
        public UpdateCircularCommand(UpdateCircularDto _updateCircularDto)
        {
            updateCircularDto = _updateCircularDto;
        }
    }
}
