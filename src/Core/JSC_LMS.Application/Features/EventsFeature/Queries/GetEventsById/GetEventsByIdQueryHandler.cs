using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.EventsFeature.Queries.GetEventsById
{
   
    public class GetEventsByIdQueryHandler : IRequestHandler<GetEventsByIdQuery, Response<GetEventsByIdDto>>
    {
        private readonly IEventsTableRepository _eventsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetEventsByIdQueryHandler(IMapper mapper, IEventsTableRepository eventsRepository, ILogger<GetEventsByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _eventsRepository = eventsRepository;
            _logger = logger;
        }

        public async Task<Response<GetEventsByIdDto>> Handle(GetEventsByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            Response<GetEventsByIdDto> responseData = new Response<GetEventsByIdDto>();
            var events = await _eventsRepository.GetByIdAsync(request.Id);
            if (events == null)
            {
                responseData.Succeeded = true;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }
            var eventsData = new GetEventsByIdDto()
            {
                Id = events.Id,
                EventTitle = events.EventTitle,
                Description = events.Description,
                Venue=events.Venue,
                EventDateTime=events.EventDateTime,
                EventCoordinator=events.EventCoordinator,
                CoordinatorNumber=events.CoordinatorNumber,
                Image=events.Image,
                File = events.File,
                IsActive = events.IsActive,
                SchoolId = events.SchoolId,
                Status = events.Status,
                School = new SchoolDto() { Id = events.School.Id, SchoolName = events.School.SchoolName }
            };
            _logger.LogInformation("Hanlde Completed");
            return new Response<GetEventsByIdDto>(eventsData, "success");
        }

    }
}
