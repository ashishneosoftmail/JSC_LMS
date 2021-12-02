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

namespace JSC_LMS.Application.Features.EventsFeature.Queries.GetAllEventsListByPagination
{
    public class GetAllEventsListByPaginationQueryHandler : IRequestHandler<GetAllEventsListByPaginationQuery, Response<IEnumerable<GetAllEventsListByPaginationDto>>>
    {
        private readonly IEventsTableRepository _eventsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetAllEventsListByPaginationQueryHandler(IMapper mapper, IEventsTableRepository eventsRepository, ILogger
            <GetAllEventsListByPaginationQueryHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _eventsRepository = eventsRepository;
        }

        public async Task<Response<IEnumerable<GetAllEventsListByPaginationDto>>> Handle(GetAllEventsListByPaginationQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allEventsData = await _eventsRepository.GetPagedReponseAsync(request.page, request.size);
            List<GetAllEventsListByPaginationDto> eventsList = new List<GetAllEventsListByPaginationDto>();
            foreach (var events in allEventsData)
            {
                eventsList.Add(new GetAllEventsListByPaginationDto()
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

            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetAllEventsListByPaginationDto>>(eventsList, "success");
        }

    }
}
