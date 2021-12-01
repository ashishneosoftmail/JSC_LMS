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

namespace JSC_LMS.Application.Features.Section.Queries.GetSectionFilter
{
   public class GetSectionFilterQueryHandler : IRequestHandler<GetSectionFilterQuery, Response<IEnumerable<GetSectionFilterDto>>>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ISectionRepository _sectionRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISchoolRepository _schoolRepository;

       

        public GetSectionFilterQueryHandler(IMapper mapper, ISchoolRepository schoolRepository, IClassRepository classRepository, ISectionRepository sectionRepository, IAuthenticationService authenticationService, ILogger<GetSectionFilterQueryHandler> logger)
        {
            _mapper = mapper;

            _logger = logger;
            _authenticationService = authenticationService;
            _sectionRepository = sectionRepository;
            _classRepository = classRepository;
            _schoolRepository = schoolRepository;

           


        }

        public async Task<Response<IEnumerable<GetSectionFilterDto>>> Handle(GetSectionFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allSection= await _sectionRepository.ListAllAsync();
            var searchFilter = (allSection.Where<JSC_LMS.Domain.Entities.Section>(x => (x.SectionName == request.SectionName) || (x.CreatedDate?.ToShortDateString() == request.CreatedDate.ToShortDateString()) || (x.IsActive == request.IsActive)).Select(x => (x)));

            if (request.SectionName != "Select Section")
            {
                allSection = allSection.Where<JSC_LMS.Domain.Entities.Section>(x => (x.SectionName == request.SectionName)).ToList();
            }
            if (request.CreatedDate.ToShortDateString() != "01-01-0001")
            {
                allSection = allSection.Where<JSC_LMS.Domain.Entities.Section>(x => x.CreatedDate?.ToShortDateString() == request.CreatedDate.ToShortDateString()).ToList();
            }
            if (request.IsActive)
            {
                allSection = allSection.Where<JSC_LMS.Domain.Entities.Section>(x => x.IsActive == request.IsActive).ToList();
            }
            else
            {
                allSection = allSection.Where<JSC_LMS.Domain.Entities.Section>(x => x.IsActive == request.IsActive).ToList();
            }
            Response<IEnumerable<GetSectionFilterDto>> responseData = new Response<IEnumerable<GetSectionFilterDto>>();
            List<GetSectionFilterDto> sectionList = new List<GetSectionFilterDto>();
            foreach (var section in allSection)
            {
                if (request.SchoolName != "Select School")
                {
                    var school = (await _schoolRepository.GetByIdAsync(section.SchoolId)).SchoolName == request.SchoolName;

                    if (school)
                    {
                  
                        sectionList.Add(new GetSectionFilterDto()
                        {
                            Id = section.Id,


                            CreatedDate = (DateTime)section.CreatedDate,

                            IsActive = section.IsActive,

                            SectionName = section.SectionName,


                            SchoolId = new SchoolDto()
                            {
                                Id = section.SchoolId,
                                SchoolName = (await _schoolRepository.GetByIdAsync(section.SchoolId)).SchoolName
                            },
                            ClassId = new ClassDto()
                            {
                                Id = section.ClassId,
                                ClassName = (await _classRepository.GetByIdAsync(section.ClassId)).ClassName
                            }



                        });
                    }
                }
                else
                {
               
                    sectionList.Add(new GetSectionFilterDto()
                    {

                        Id = section.Id,


                        CreatedDate = (DateTime)section.CreatedDate,

                        IsActive = section.IsActive,

                        SectionName = section.SectionName,


                        SchoolId = new SchoolDto()
                        {
                            Id = section.SchoolId,
                            SchoolName = (await _schoolRepository.GetByIdAsync(section.SchoolId)).SchoolName
                        },
                        ClassId = new ClassDto()
                        {
                            Id = section.ClassId,
                            ClassName = (await _classRepository.GetByIdAsync(section.ClassId)).ClassName
                        }

                    });
                }
            }
        
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetSectionFilterDto>>(sectionList, "success");
        }

    }
}
