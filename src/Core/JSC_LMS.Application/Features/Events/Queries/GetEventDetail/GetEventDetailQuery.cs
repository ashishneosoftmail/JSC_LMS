using JSC_LMS.Application.Response;
using MediatR;
using System;

namespace JSC_LMS.Application.Features.Events.Queries.GetEventDetail
{
    public class GetEventDetailQuery : IRequest<Response<EventDetailVm>>
    {
        public string Id { get; set; }
    }
}
