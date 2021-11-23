using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Circulars.Commands.CreateCircular
{
    public class CreateCircularCommand : IRequest<Response<CreateCircularDto>>
    {
        public CreateCircularCommand(CreateCircularDto _createCircularDto)
        {
            createCircularDto = _createCircularDto;
        }
        public CreateCircularDto createCircularDto { get; set; }
    }
}
