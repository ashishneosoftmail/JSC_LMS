using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Section.Queries.GetSectionByPagination
{
    public class GetSectionByPaginationQueryHandler : IRequestHandler<GetSectionByPaginationQuery, Response<GetSectionListByPaginationResponse>>
    {

        private readonly ISectionRepository _sectionRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IClassRepository _classRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetSectionByPaginationQueryHandler(IMapper mapper, ISectionRepository sectionlRepository,IClassRepository classRepository, ISchoolRepository schoolRepository, IAuthenticationService authenticationService, ILogger
            <GetSectionByPaginationQueryHandler> logger)
        {
            _mapper = mapper;
            _sectionRepository = sectionlRepository;
            _schoolRepository = schoolRepository;
            _classRepository = classRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public async Task<Response<GetSectionListByPaginationResponse>> Handle(GetSectionByPaginationQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allSectionCount = await _sectionRepository.ListAllAsync();
            var allSection = await _sectionRepository.GetPagedReponseAsync(request.page, request.size);
            GetSectionListByPaginationResponse getSectionListByPaginationResponse = new GetSectionListByPaginationResponse();
            List<GetSectionListPaginationDto> sectionList = new List<GetSectionListPaginationDto>();
            foreach (var section in allSection)
            {

                sectionList.Add(new GetSectionListPaginationDto()
                {
                    Id = section.Id,
                  
                  
                    IsActive = section.IsActive,
                    SectionName = section.SectionName,
                    CreatedDate = (DateTime)section.CreatedDate,
                  
                    School = new SchoolPaginationDto()
                    {
                        Id = section.SchoolId,
                        SchoolName = (await _schoolRepository.GetByIdAsync(section.SchoolId)).SchoolName
                    },

                    Class = new ClassPaginationDto()
                    {
                        Id = section.SchoolId,
                        ClassName = (await _classRepository.GetByIdAsync(section.ClassId)).ClassName
                    }
                });
            }
            getSectionListByPaginationResponse.GetSectionListPaginationDto = sectionList;
            getSectionListByPaginationResponse.Count = allSectionCount.Count();

            /* var principal = _mapper.Map<List<GetPrincipalListDto>>(allPrincipal);
             _logger.LogInformation("Hanlde Completed");
             return new Response<IEnumerable<GetPrincipalListDto>>(principal, "success");*/
            _logger.LogInformation("Hanlde Completed");
            return new Response<GetSectionListByPaginationResponse>(getSectionListByPaginationResponse, "success");
        }
    }
}
