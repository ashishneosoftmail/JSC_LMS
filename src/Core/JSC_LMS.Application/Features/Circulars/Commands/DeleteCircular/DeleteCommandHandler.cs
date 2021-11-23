using JSC_LMS.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Circulars.Commands.DeleteCircular
{

    public class DeleteCommandHandler : IRequestHandler<DeleteCircularCommand>
    {
        private readonly ICircularRepository _circularRepository;

        public DeleteCommandHandler(ICircularRepository circularRepository)
        {
            _circularRepository = circularRepository;
        }

        public async Task<Unit> Handle(DeleteCircularCommand request, CancellationToken cancellationToken)
        {
            var circularToDelete = await _circularRepository.GetByIdAsync(request.Id);
            await _circularRepository.DeleteAsync(circularToDelete);
            return Unit.Value;
        }
    }
}
