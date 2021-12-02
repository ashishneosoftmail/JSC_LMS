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

namespace JSC_LMS.Application.Features.EventsFeature.Queries.GetAllEventsListBySchoolPagination
{
    public class GetAllEventsListBySchoolPaginationQueryHandler : IRequestHandler<GetAllEventsListBySchoolPaginationQuery, Response<IEnumerable<GetAllEventsListBySchoolPaginationDto>>>
    {
        private readonly IEventsTableRepository _eventsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetAllEventsListBySchoolPaginationQueryHandler(IMapper mapper, IEventsTableRepository eventsRepository, ILogger
            <GetAllEventsListBySchoolPaginationQueryHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _eventsRepository = eventsRepository;
        }

        public async Task<Response<IEnumerable<GetAllEventsListBySchoolPaginationDto>>> Handle(GetAllEventsListBySchoolPaginationQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allEventsData = await _eventsRepository.PrincipalGetPagedReponseAsyncBySchoolId(request.page, request.size , request.SchoolId);
            List<GetAllEventsListBySchoolPaginationDto> eventsList = new List<GetAllEventsListBySchoolPaginationDto>();
            foreach (var events in allEventsData)
            {
                eventsList.Add(new GetAllEventsListBySchoolPaginationDto()
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
            return new Response<IEnumerable<GetAllEventsListBySchoolPaginationDto>>(eventsList, "success");
        }

    }
}
