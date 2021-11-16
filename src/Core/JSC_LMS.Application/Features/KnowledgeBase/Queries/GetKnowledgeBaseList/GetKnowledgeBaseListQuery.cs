using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.KnowledgeBase.Queries.GetKnowledgeBaseList
{
    public class GetKnowledgeBaseListQuery : IRequest<Response<IEnumerable<GetKnowledgeBaseListDto>>>
    {
    }
}
