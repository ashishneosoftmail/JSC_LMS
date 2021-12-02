using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.EventsFeature.Queries.GetAllEventsListBySchool
{
    public class GetAllEventsListBySchoolQuery : IRequest<Response<IEnumerable<GetAllEventsListBySchoolDto>>>
    {
        public int SchoolId { get; set; }
    }
}
