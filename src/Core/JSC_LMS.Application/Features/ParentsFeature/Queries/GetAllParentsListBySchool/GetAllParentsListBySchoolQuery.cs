using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.ParentsFeature.Queries.GetAllParentsListBySchool
{
    public class GetAllParentsListBySchoolQuery : IRequest<Response<IEnumerable<GetAllParentsListBySchoolDto>>>
    {
        public int SchoolId { get; set; }
    }
}
