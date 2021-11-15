using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.KnowledgeBase.Commands.UpdateKnowledgeBase
{
    public class UpdateKnowledgeBaseCommand : IRequest<Response<int>>
    {
        public UpdateKnowledgeBaseDto _updateKnowledgeBaseDto { get; set; }
        public UpdateKnowledgeBaseCommand(UpdateKnowledgeBaseDto updateKnowledgeBaseDto)
        {
            _updateKnowledgeBaseDto = updateKnowledgeBaseDto;
        }
    }
}
