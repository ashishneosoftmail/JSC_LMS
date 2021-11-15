using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.KnowledgeBase.Commands.DeleteKnowledgeBase
{
    public class DeleteKnowledgeBaseCommand : IRequest
    {
        public int Id { get; set; }
    }
}
