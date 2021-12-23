using JSC_LMS.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Gallary.Commands.DeleteAllImage
{
   public class DeleteAllImageCommandHandler : IRequestHandler<DeleteAllImageCommand>
    {
        private readonly IGallaryRepository _gallaryRepository;

        public DeleteAllImageCommandHandler(IGallaryRepository gallaryRepository)
        {
            _gallaryRepository = gallaryRepository;
        }
        public async Task<Unit> Handle(DeleteAllImageCommand request, CancellationToken cancellationToken)
        {
            var imageToDelete = await _gallaryRepository.ListAllAsync();
            await _gallaryRepository.DeleteAllAsync(imageToDelete);
            return Unit.Value;
        }
    }
}
