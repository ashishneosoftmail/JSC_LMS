using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.ParentsFeature.Queries.GetParentsByPagination
{
    public class GetParentsByPaginationQuery : IRequest<Response<GetParentsByPaginationResponse>>
    {
        public int page { get; set; }
        public int size { get; set; }
    }
}
