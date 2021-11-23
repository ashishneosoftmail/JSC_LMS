using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.FAQ.Commands.CreateFAQ
{
   public class CreateFAQCommand : IRequest<Response<CreateFAQDto>>
    {
        public CreateFAQDto _createKnowledgeBaseDto { get; set; }
        public CreateFAQCommand(CreateFAQDto createKnowledgeBaseDto)
        {
            _createKnowledgeBaseDto = createKnowledgeBaseDto;
        }
    }
}
