using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Section.Queries.GetSectionList
{
   public class GetSectionListQuery : IRequest<Response<IEnumerable<GetSectionListDto>>>
    {

    }
}
