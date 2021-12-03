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
   public class GetAllGallaryByFilterQueryHandler : IRequestHandler<GetAllGallaryByFilterQuery, Response<IEnumerable<GetAllGallaryByFilterDto>>>
    {
        private readonly IEventsTableRepository _eventTableRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IGallaryRepository _gallaryRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetAllGallaryByFilterQueryHandler(IMapper mapper, IEventsTableRepository eventTableRepository, ISchoolRepository schoolRepository, IAuthenticationService authenticationService, ILogger<GetAllGallaryByFilterQueryHandler> logger)
        {
            _mapper = mapper;
            _eventTableRepository = eventTableRepository;
            _schoolRepository = schoolRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public async Task<Response<IEnumerable<GetAllGallaryByFilterDto>>> Handle(GetAllGallaryByFilterQuery request, CancellationToken cancellationToken)
        {
            var allClass = await _gallaryRepository.ListAllAsync();
            _logger.LogInformation("Handle Initiated");
         
            if (request.SchoolName != "Select School")
            {
                allClass = allClass.Where<JSC_LMS.Domain.Entities.Gallary>(x => (x.School.SchoolName == request.SchoolName)).ToList();
            }
            if (request.EventTitle != "Select Event")
            {
                allClass = allClass.Where<JSC_LMS.Domain.Entities.Gallary>(x => (x.EventsTable.EventTitle == request.EventTitle)).ToList();
            }
         
            else
            {
                allClass = allClass.Where<JSC_LMS.Domain.Entities.Gallary>(x => x.School.SchoolName == request.SchoolName).ToList();
            }
            Response<IEnumerable<GetAllGallaryByFilterDto>> responseData = new Response<IEnumerable<GetAllGallaryByFilterDto>>();
            List<GetAllGallaryByFilterDto> gallaryList = new List<GetAllGallaryByFilterDto>();
            foreach (var gallary in allClass)
            {
                if (request.SchoolName != "Select School")
                {
                    var school = (await _schoolRepository.GetByIdAsync(gallary.SchoolId)).SchoolName == request.SchoolName;
                    if (school)
                    {

                        gallaryList.Add(new GetAllGallaryByFilterDto()
                        {
                            Id = gallary.Id,
                           
                            FileName = gallary.FileName,
                            FileType = gallary.FileType,
                            image = gallary.image,
                            EventsTableId = gallary.EventsTableId,
                            IsActive = gallary.IsActive,
                            SchoolId = gallary.SchoolId,
                          


                            SchoolData = new SchoolDto()
                            {
                                Id = gallary.SchoolId,
                                SchoolName = (await _schoolRepository.GetByIdAsync(gallary.SchoolId)).SchoolName
                            },

                            EventsData=new EventsTableDto()
                            {
                                Id = gallary.Id,
                                EventTitle = (await _eventTableRepository.GetByIdAsync(gallary.EventsTableId)).EventTitle

                            }
                        });
                    }
                }
                else
                {

                    gallaryList.Add(new GetAllGallaryByFilterDto()
                    {
                        Id = gallary.Id,
                        FileName = gallary.FileName,
                        FileType = gallary.FileType,
                        image = gallary.image,
                        EventsTableId = gallary.EventsTableId,
                        IsActive = gallary.IsActive,
                        SchoolId = gallary.SchoolId,

                        SchoolData = new SchoolDto() { Id = gallary.School.Id, SchoolName = gallary.School.SchoolName },
                        EventsData = new EventsTableDto()
                        {
                            Id = gallary.EventsTable.Id,
                            EventTitle = gallary.EventsTable.EventTitle

                        }
                    });
                }
            }
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetAllGallaryByFilterDto>>(gallaryList, "success");
        }

    }
}
