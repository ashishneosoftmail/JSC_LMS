using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.EventsFeature.Commands.UpdateEvents
{
    public class UpdateEventsCommand : IRequest<Response<int>>
    {
        public UpdateEventsDto updateEventsDto { get; set; }
        public UpdateEventsCommand(UpdateEventsDto _updateEventsDto)
        {
            updateEventsDto = _updateEventsDto;
        }
    }
}
