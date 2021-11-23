using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.FAQ.Queries.GetFAQList
{
    public class GetFAQListQuery : IRequest<Response<IEnumerable<GetFAQListDto>>>
    {
    }
}
