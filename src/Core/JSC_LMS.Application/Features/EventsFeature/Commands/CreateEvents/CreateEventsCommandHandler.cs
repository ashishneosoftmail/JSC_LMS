using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using JSC_LMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.EventsFeature.Commands.CreateEvents
{
    public class CreateEventsCommandHandler : IRequestHandler<CreateEventsCommand, Response<CreateEventsDto>>
    {
        private readonly IEventsTableRepository _eventsTableRepository;
        private readonly IMapper _mapper;


        public CreateEventsCommandHandler(IMapper mapper, IEventsTableRepository eventsTableRepository)
        {
            _mapper = mapper;
            _eventsTableRepository = eventsTableRepository;
        }

        public async Task<Response<CreateEventsDto>> Handle(CreateEventsCommand request, CancellationToken cancellationToken)
        {
            var createEventsDto = new Response<CreateEventsDto>();

            var eventsData = await _eventsTableRepository.AddAsync(_mapper.Map<EventsTable>(request.createEventsDto));
            createEventsDto.Data = _mapper.Map<CreateEventsDto>(eventsData);
            createEventsDto.Succeeded = true;
            createEventsDto.Message = "success";
            return createEventsDto;
        }

    }
}
