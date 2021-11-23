using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.School.Queries.GetSchoolList
{
    public class GetSchoolListQuery :   IRequest<Response<IEnumerable<GetSchoolListDto>>>
    {
    }
}
