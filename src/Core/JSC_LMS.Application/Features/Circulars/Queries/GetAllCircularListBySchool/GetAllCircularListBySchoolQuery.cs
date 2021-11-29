using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Circulars.Queries.GetAllCircularListBySchool
{

    public class GetAllCircularListBySchoolQuery : IRequest<Response<IEnumerable<GetAllCircularListBySchoolDto>>>
    {
        public int SchoolId { get; set; }
    }
}
