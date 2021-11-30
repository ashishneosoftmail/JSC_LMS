using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Students.Queries.GetAllStudentListBySchool
{
    public class GetAllStudentListBySchoolQuery : IRequest<Response<IEnumerable<GetAllStudentListBySchoolDto>>>
    {
        public int SchoolId { get; set; }
    }
}
