using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Students.Queries.GetStudentList
{
    public class GetStudentListQuery : IRequest<Response<IEnumerable<GetStudentListDto>>>
    {
    }
}
