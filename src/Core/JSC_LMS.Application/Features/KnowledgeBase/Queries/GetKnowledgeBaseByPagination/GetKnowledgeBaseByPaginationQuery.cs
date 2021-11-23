using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.KnowledgeBase.Queries.GetKnowledgeBaseByPagination
{
    public class GetKnowledgeBaseByPaginationQuery : IRequest<Response<IEnumerable<GetKnowledgeBaseByPaginationDto>>>
    {
        public int page { get; set; }
        public int size { get; set; }
    }
}
