using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.EventsFeature.Queries.GetAllEventsList
{
    public class GetAllEventsListQuery : IRequest<Response<IEnumerable<GetAllEventsListDto>>>
    {
    }
}
