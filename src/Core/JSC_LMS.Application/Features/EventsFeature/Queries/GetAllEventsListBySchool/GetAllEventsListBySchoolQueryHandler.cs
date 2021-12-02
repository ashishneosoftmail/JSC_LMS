using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using JSC_LMS.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace JSC_LMS.Application.Features.EventsFeature.Queries.GetAllEventsListBySchool
{
    public class GetAllEventsListBySchoolQueryHandler : IRequestHandler<GetAllEventsListBySchoolQuery, Response<IEnumerable<GetAllEventsListBySchoolDto>>>
    {
        private readonly IEventsTableRepository _eventsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetAllEventsListBySchoolQueryHandler(IMapper mapper, IEventsTableRepository eventsRepository, ILogger<GetAllEventsListBySchoolQueryHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _eventsRepository = eventsRepository;
        }

        public async Task<Response<IEnumerable<GetAllEventsListBySchoolDto>>> Handle(GetAllEventsListBySchoolQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allEventsList = (await _eventsRepository.ListAllAsync()).Where<EventsTable>(x=> x.SchoolId == request.SchoolId);
            _logger.LogInformation("Hanlde Completed");
            List<GetAllEventsListBySchoolDto> eventsList = new List<GetAllEventsListBySchoolDto>();
            foreach (var events in allEventsList)
            {
                eventsList.Add(new GetAllEventsListBySchoolDto()
                {
                    Id = events.Id,
                    EventTitle = events.EventTitle,
                    EventCoordinator = events.EventCoordinator,
                    EventDateTime = events.EventDateTime,
                    CoordinatorNumber = events.CoordinatorNumber,
                    SchoolId = events.SchoolId,
                    Status = events.Status,
                    Venue = events.Venue,
                    School = new SchoolDto() { Id = events.School.Id, SchoolName = events.School.SchoolName }
                });
            }
            return new Response<IEnumerable<GetAllEventsListBySchoolDto>>(eventsList, "success");
        }
    }
}
