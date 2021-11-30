using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.ParentsFeature.Queries.GetParentsListByPaginationBySchool
{
    public class GetParentsListByPaginationBySchoolQuery : IRequest<Response<IEnumerable<GetParentsListByPaginationBySchoolDto>>>
    {
        public int page { get; set; }
        public int size { get; set; }
        public int SchoolId { get; set; }
    }
}
