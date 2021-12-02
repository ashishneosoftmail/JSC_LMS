using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.EventsFeature.Commands.CreateEvents
{
    public class CreateEventsCommand : IRequest<Response<CreateEventsDto>>
    {
        public CreateEventsCommand(CreateEventsDto _createEventsDto)
        {
            createEventsDto = _createEventsDto;
        }
        public CreateEventsDto createEventsDto { get; set; }
    }
}
