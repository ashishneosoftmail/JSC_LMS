using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Circulars.Queries.GetCircularListByPagination
{
    public class GetAllCircularListByPaginationQuery : IRequest<Response<IEnumerable<GetAllCircularListByPaginationDto>>>
    {
        public int page { get; set; }
        public int size { get; set; }
    }
}
