using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Teachers.Queries.GetTeacherList
{
    public class GetTeacherListQuery : IRequest<Response<IEnumerable<GetTeacherListDto>>>
    {
    }
}
