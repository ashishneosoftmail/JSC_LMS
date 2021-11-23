using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Exceptions;
using JSC_LMS.Application.Response;
using JSC_LMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Events.Queries.GetEventDetail
{
    public class GetEventDetailQueryHandler : IRequestHandler<GetEventDetailQuery, Response<EventDetailVm>>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IAsyncRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        private readonly IDataProtector _protector;
        public GetEventDetailQueryHandler(IMapper mapper, IAsyncRepository<Event> eventRepository, IAsyncRepository<Category> categoryRepository, IDataProtectionProvider provider)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
            _protector = provider.CreateProtector("");
        }

        public async Task<Response<EventDetailVm>> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
        {
            /* string id = _protector.Unprotect(request.Id);

             var @event = await _eventRepository.GetByIdAsync(new Guid(id));
             var eventDetailDto = _mapper.Map<EventDetailVm>(@event);

             var category = await _categoryRepository.GetByIdAsync(@event.CategoryId);

             if (category == null)
             {
                 throw new NotFoundException(nameof(Event), request.Id);
             }
             eventDetailDto.Category = _mapper.Map<CategoryDto>(category);

             var response = new Response<EventDetailVm>(eventDetailDto);
             return response;*/
            return null;
        }
    }
}
