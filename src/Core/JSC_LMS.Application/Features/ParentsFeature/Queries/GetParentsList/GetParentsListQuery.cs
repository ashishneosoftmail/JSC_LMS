using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.ParentsFeature.Queries.GetParentsList
{
    public class GetParentsListQuery : IRequest<Response<IEnumerable<GetParentsListDto>>>
    {
    }
}
