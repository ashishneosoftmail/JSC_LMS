using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.KnowledgeBase.Commands.CreateKnowledgeBase
{
    public class CreateKnowledgeBaseCommand : IRequest<Response<CreateKnowledgeBaseDto>>
    {
        public CreateKnowledgeBaseDto _createKnowledgeBaseDto { get; set; }
        public CreateKnowledgeBaseCommand(CreateKnowledgeBaseDto createKnowledgeBaseDto)
        {
            _createKnowledgeBaseDto = createKnowledgeBaseDto;
        }
    }
}
