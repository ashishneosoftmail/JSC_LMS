using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Class.Queries.GetClassList
{
   public class GetClassListQuery : IRequest<Response<IEnumerable<GetClassListDto>>>
    {

    }
}
