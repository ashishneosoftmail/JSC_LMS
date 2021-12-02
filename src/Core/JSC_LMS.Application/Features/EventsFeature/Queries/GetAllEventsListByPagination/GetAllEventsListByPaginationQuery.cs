using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.EventsFeature.Queries.GetAllEventsListByPagination
{
   public class GetAllEventsListByPaginationQuery: IRequest<Response<IEnumerable<GetAllEventsListByPaginationDto>>>
    {
        public int page { get; set; }
        public int size { get; set; }
    }
}
