using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Academics.Queries.GetAcademicList
{
    public class GetAcademicListQuery : IRequest<Response<IEnumerable<GetAcademicListDto>>>
    {

    }
}
