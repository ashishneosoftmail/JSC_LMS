using JSC_LMS.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.KnowledgeBase.Commands.DeleteKnowledgeBase
{
    public class DeleteKnowledgeBaseCommandHandler : IRequestHandler<DeleteKnowledgeBaseCommand>
    {
        private readonly IKnowledgeBaseRepository _knowledgebaseRepository;

        public DeleteKnowledgeBaseCommandHandler(IKnowledgeBaseRepository knowledgebaseRepository)
        {
            _knowledgebaseRepository = knowledgebaseRepository;
        }

        public async Task<Unit> Handle(DeleteKnowledgeBaseCommand request, CancellationToken cancellationToken)
        {
            var knowledgebaseToDelete = await _knowledgebaseRepository.GetByIdAsync(request.Id);
            await _knowledgebaseRepository.DeleteAsync(knowledgebaseToDelete);
            return Unit.Value;
        }
    }
}
