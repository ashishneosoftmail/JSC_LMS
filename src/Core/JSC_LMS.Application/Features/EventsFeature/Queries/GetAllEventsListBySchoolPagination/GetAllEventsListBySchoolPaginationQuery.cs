using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.EventsFeature.Queries.GetAllEventsListBySchoolPagination
{
    public class GetAllEventsListBySchoolPaginationQuery: IRequest<Response<IEnumerable<GetAllEventsListBySchoolPaginationDto>>>
    {
        public int page { get; set; }
        public int size { get; set; }
        public int SchoolId { get; set; }
    }
}
