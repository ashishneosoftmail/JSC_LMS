using JSC_LMS.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.FAQ.Commands.DeleteFAQ
{
   public class DeleteFAQCommandHandler : IRequestHandler<DeleteFAQCommand>
    {
        private readonly IFAQRepository _faqRepository;
        public DeleteFAQCommandHandler(IFAQRepository faqRepository)
        {
            _faqRepository = faqRepository;
        }

        public async Task<Unit> Handle(DeleteFAQCommand request, CancellationToken cancellationToken)
        {
            var faqToDelete = await _faqRepository.GetByIdAsync(request.Id);
            await _faqRepository.DeleteAsync(faqToDelete);
            return Unit.Value;
        }
    }
}
