using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Section.Queries.GetSectionList
{
    public class GetSectionListQueryHandler : IRequestHandler<GetSectionListQuery, Response<IEnumerable<GetSectionListDto>>>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetSectionListQueryHandler(IMapper mapper, IClassRepository classRepository, ISectionRepository sectionRepository, ISchoolRepository schoolRepository, IAuthenticationService authenticationService, ILogger<GetSectionListQueryHandler> logger)
        {
            _mapper = mapper;
            _classRepository = classRepository;
            _sectionRepository = sectionRepository;
            _schoolRepository = schoolRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public async Task<Response<IEnumerable<GetSectionListDto>>> Handle(GetSectionListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allSections = await _sectionRepository.ListAllAsync();
            List<GetSectionListDto> sectionList = new List<GetSectionListDto>();
            foreach (var sections in allSections)
            {
                sectionList.Add(new GetSectionListDto()
                {
                    Id = sections.Id,
                    CreatedDate = (DateTime)sections.CreatedDate,
                    SectionName = sections.SectionName,
                    IsActive = sections.IsActive,
                    School = new SchoolDto()
                    {
                        Id = sections.SchoolId,
                        SchoolName = (await _schoolRepository.GetByIdAsync(sections.SchoolId)).SchoolName
                    },

                    Class = new ClassDto()
                    {
                        Id = sections.ClassId,
                        ClassName = (await _classRepository.GetByIdAsync(sections.ClassId)).ClassName
                    }
                });
            }
            /* var principal = _mapper.Map<List<GetPrincipalListDto>>(allPrincipal);
             _logger.LogInformation("Hanlde Completed");
             return new Response<IEnumerable<GetPrincipalListDto>>(principal, "success");*/
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetSectionListDto>>(sectionList, "success");
        }

    }
}
