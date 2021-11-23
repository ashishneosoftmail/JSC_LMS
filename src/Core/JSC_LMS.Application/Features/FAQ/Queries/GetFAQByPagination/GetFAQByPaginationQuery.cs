using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.FAQ.Queries.GetFAQByPagination
{
    public class GetFAQByPaginationQuery : IRequest<Response<IEnumerable<GetFAQByPaginationDto>>>
    {
        public int page { get; set; }
        public int size { get; set; }
    }
}
