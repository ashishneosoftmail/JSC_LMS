using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Gallary.Queries.GetAllGallaryFilter
{
    public class GetAllGallaryBySchoolId : IRequestHandler<GetAllGallaryByFilterQuery, Response<IEnumerable<GetAllGallaryByFilterDto>>>
    {
        private readonly IEventsTableRepository _eventTableRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IGallaryRepository _gallaryRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetAllGallaryBySchoolId(IMapper mapper, IEventsTableRepository eventTableRepository, ISchoolRepository schoolRepository,IGallaryRepository gallaryRepository, IAuthenticationService authenticationService, ILogger<GetAllGallaryBySchoolId> logger)
        {
            _mapper = mapper;
            _eventTableRepository = eventTableRepository;
            _schoolRepository = schoolRepository;
            _gallaryRepository = gallaryRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public async Task<Response<IEnumerable<GetAllGallaryByFilterDto>>> Handle(GetAllGallaryByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            //var allgallaryschool = (await _schoolRepository.ListAllAsync()).Where(x => x.Id == request.SchoolId).FirstOrDefault();
            var gallaryschool = (await _gallaryRepository.ListAllAsync()).Where<JSC_LMS.Domain.Entities.Gallary>(x => x.SchoolId == request.SchoolId);
       

            List<GetAllGallaryByFilterDto> schoolgallaryList = new List<GetAllGallaryByFilterDto>();
            foreach (var gallary in gallaryschool)
            {

                schoolgallaryList.Add(new GetAllGallaryByFilterDto()
                {
                    Id = gallary.Id,
                    image = gallary.image,
                    EventsTableId = gallary.EventsTableId,
                    FileName = gallary.FileName,
                    FileType = gallary.FileType,
                    IsActive = gallary.IsActive,

                    SchoolData = new SchoolDto()
                    {
                        Id = gallary.SchoolId,
                        SchoolName = (await _schoolRepository.GetByIdAsync(gallary.SchoolId)).SchoolName
                    },

                    EventsData = new EventsTableDto()
                    {
                        Id = gallary.Id,
                        EventTitle = (await _eventTableRepository.GetByIdAsync(gallary.EventsTableId)).EventTitle

                    }
                });

            }

            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetAllGallaryByFilterDto>>(schoolgallaryList, "success");
        }
    }
}

    

