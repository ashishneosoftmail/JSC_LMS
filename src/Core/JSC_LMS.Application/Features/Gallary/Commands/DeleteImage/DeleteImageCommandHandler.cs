using JSC_LMS.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Gallary.Commands.DeleteImage
{
   
        public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand>
        {
            private readonly IGallaryRepository _gallaryRepository;

            public DeleteImageCommandHandler(IGallaryRepository gallaryRepository)
            {
            _gallaryRepository = gallaryRepository;
            }

            public async Task<Unit> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
            {
                var imageToDelete = await _gallaryRepository.GetByIdAsync(request.Id);
                await _gallaryRepository.DeleteAsync(imageToDelete);
                return Unit.Value;
            }
        }
    }

