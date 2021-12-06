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

namespace JSC_LMS.Application.Features.EventsFeature.Queries.GetAllEventsList
{
    public class GetAllEventsListQueryHandler : IRequestHandler<GetAllEventsListQuery, Response<IEnumerable<GetAllEventsListDto>>>
    {
        private readonly IEventsTableRepository _eventsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetAllEventsListQueryHandler(IMapper mapper, IEventsTableRepository eventsRepository, ILogger<GetAllEventsListQueryHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _eventsRepository = eventsRepository;
        }

        public async Task<Response<IEnumerable<GetAllEventsListDto>>> Handle(GetAllEventsListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allEventsList = await _eventsRepository.ListAllAsync();
            _logger.LogInformation("Hanlde Completed");
            List<GetAllEventsListDto> eventsList = new List<GetAllEventsListDto>();
            foreach (var events in allEventsList)
            {
                eventsList.Add(new GetAllEventsListDto()
                {
                    Id = events.Id,
                    EventTitle = events.EventTitle,
                    EventCoordinator=events.EventCoordinator,
                    EventDateTime=events.EventDateTime,
                    CoordinatorNumber=events.CoordinatorNumber,
                    SchoolId = events.SchoolId,
                    Status = events.Status,
                    Venue=events.Venue,
                    School = new SchoolDto() { Id = events.School.Id, SchoolName = events.School.SchoolName },
                    CreatedDate=events.CreatedDate
                });
            }
            return new Response<IEnumerable<GetAllEventsListDto>>(eventsList, "success");
        }
    }
}
