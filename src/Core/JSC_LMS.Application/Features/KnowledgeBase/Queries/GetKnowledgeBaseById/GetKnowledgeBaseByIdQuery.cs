using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.KnowledgeBase.Queries.GetKnowledgeBaseById
{
    public class GetKnowledgeBaseByIdQuery : IRequest<Response<GetKnowledgeBaseByIdDto>>
    {
        public int Id { get; set; }
    }
}
