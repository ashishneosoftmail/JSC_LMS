using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Exceptions;
using JSC_LMS.Application.Response;
using JSC_LMS.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Response<Guid>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public UpdateEventCommandHandler(IMapper mapper, IEventRepository eventRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        public async Task<Response<Guid>> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {

            /* var eventToUpdate = await _eventRepository.GetByIdAsync(request.EventId);

             if (eventToUpdate == null)
             {
                 throw new NotFoundException(nameof(Event), request.EventId);
             }

             var validator = new UpdateEventCommandValidator();
             var validationResult = await validator.ValidateAsync(request);

             if (validationResult.Errors.Count > 0)
                 throw new ValidationException(validationResult);

             _mapper.Map(request, eventToUpdate, typeof(UpdateEventCommand), typeof(Event));

             await _eventRepository.UpdateAsync(eventToUpdate);

             return new Response<Guid>(request.EventId, "Updated successfully ");
 */
            return null;
        }
    }
}