using AutoMapper;
using JSC_LMS.Application.CommonDtos;
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

namespace JSC_LMS.Application.Features.School.Queries.GetSchoolByPagination
{
  public  class GetSchoolByPaginationQueryHandler:IRequestHandler<GetSchoolByPaginationQuery, Response<GetSchoolByPaginationResponse>>
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly IInstituteRepository _instituteRepository;

        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetSchoolByPaginationQueryHandler(IMapper mapper, ISchoolRepository schoolRepository, IInstituteRepository instituteRepository, IAuthenticationService authenticationService, ILogger<GetSchoolByPaginationQueryHandler> logger)
        {
            _mapper = mapper;
            _schoolRepository = schoolRepository;
            _instituteRepository = instituteRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public async Task<Response<GetSchoolByPaginationResponse>> Handle(GetSchoolByPaginationQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allSchoolCount = await _schoolRepository.ListAllAsync();
            var allSchool = await _schoolRepository.GetPagedReponseAsync(request.page, request.size);
            GetSchoolByPaginationResponse getPrincipalListByPaginationResponse = new GetSchoolByPaginationResponse();
            List<GetSchoolByPaginationDto> principalList = new List<GetSchoolByPaginationDto>();
            foreach (var school in allSchool)
            {
              
                principalList.Add(new GetSchoolByPaginationDto()
                {
                    Id = school.Id,
                    AddressLine1 = school.Address1,
                    AddressLine2 = school.Address2,
                    Mobile = school.Mobile,
              
                    Email = school.Email,
                    IsActive = school.IsActive,
                    SchoolName = school.SchoolName,
                    CreatedDate = (DateTime)school.CreatedDate,
                    City = _mapper.Map<CityDto>(school.City),
                    State = _mapper.Map<StateDto>(school.State),
                    Zip = _mapper.Map<ZipDto>(school.Zip),
                    Institute = new InstitutePaginationDto()
                    {
                        Id = school.Id,
                        InstituteName = (await _instituteRepository.GetByIdAsync(school.Id)).InstituteName
                    }
                });
            }
            getPrincipalListByPaginationResponse.GetSchoolByPaginationDto = principalList;
            getPrincipalListByPaginationResponse.Count = allSchoolCount.Count();

            /* var principal = _mapper.Map<List<GetPrincipalListDto>>(allPrincipal);
             _logger.LogInformation("Hanlde Completed");
             return new Response<IEnumerable<GetPrincipalListDto>>(principal, "success");*/
            _logger.LogInformation("Hanlde Completed");
            return new Response<GetSchoolByPaginationResponse>(getPrincipalListByPaginationResponse, "success");
        }


    }
}
