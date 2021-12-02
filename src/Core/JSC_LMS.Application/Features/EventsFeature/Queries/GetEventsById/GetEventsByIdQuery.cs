using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.EventsFeature.Queries.GetEventsById
{
    public class GetEventsByIdQuery : IRequest<Response<GetEventsByIdDto>>
    {
        public int Id { get; set; }
    }
}
