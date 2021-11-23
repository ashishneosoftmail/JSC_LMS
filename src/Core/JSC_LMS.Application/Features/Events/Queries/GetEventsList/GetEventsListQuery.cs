using JSC_LMS.Application.Response;
using MediatR;
using System.Collections.Generic;

namespace JSC_LMS.Application.Features.Events.Queries.GetEventsList
{
    public class GetEventsListQuery : IRequest<Response<IEnumerable<EventListVm>>>
    {

    }
}
