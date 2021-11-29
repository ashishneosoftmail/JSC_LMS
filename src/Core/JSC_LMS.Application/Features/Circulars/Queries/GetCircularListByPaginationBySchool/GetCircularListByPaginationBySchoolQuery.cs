using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Circulars.Queries.GetCircularListByPaginationBySchool
{
    public class GetCircularListByPaginationBySchoolQuery : IRequest<Response<IEnumerable<GetCircularListByPaginationBySchoolDto>>>
    {
        public int page { get; set; }
        public int size { get; set; }
        public int SchoolId { get; set; }
    }
}
