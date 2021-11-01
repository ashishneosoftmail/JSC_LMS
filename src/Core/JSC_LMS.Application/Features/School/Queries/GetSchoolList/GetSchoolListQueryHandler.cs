using AutoMapper;
using JSC_LMS.Application.CommonDtos;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.School.Queries.GetSchoolList
{
    public class GetSchoolListQueryHandler : IRequestHandler<GetSchoolListQuery, Response<IEnumerable<GetSchoolListDto>>>
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly IInstituteRepository _instituteRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;


        public GetSchoolListQueryHandler(IMapper mapper, ISchoolRepository schoolRepository, IInstituteRepository instituteRepository, IAuthenticationService authenticationService, ILogger<GetSchoolListQueryHandler> logger)
        {
            _mapper = mapper;
            _schoolRepository = schoolRepository;
            _instituteRepository = instituteRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public async Task<Response<IEnumerable<GetSchoolListDto>>> Handle(GetSchoolListQuery request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Handle Initiated");
            var allSchool = await _schoolRepository.ListAllAsync();
            List<GetSchoolListDto> schoolList = new List<GetSchoolListDto>();

            foreach (var school in allSchool)
            {
                //var user = await _authenticationService.GetUserById(principal.UserId);
                schoolList.Add(new GetSchoolListDto()
                {
                    Id = school.Id,
                    AddressLine1 = school.Address1,
                    AddressLine2 = school.Address2,
                    Mobile = school.Mobile,
                    ContactPerson = school.ContactPerson,
                    Email = school.Email,
                    IsActive = school.IsActive,
                    SchoolName = school.SchoolName,
                    City = _mapper.Map<CityDto>(school.City),
                    State = _mapper.Map<StateDto>(school.State),
                    Zip = _mapper.Map<ZipDto>(school.Zip),
                    Institute = new InstituteDto()


                    {
                        Id = school.InstituteId,
                        InstituteName = (await _instituteRepository.GetByIdAsync(school.InstituteId)).InstituteName
                    }
                });
            }
                _logger.LogInformation("Hanlde Completed");
                return new Response<IEnumerable<GetSchoolListDto>>(schoolList, "success");

            }
      

    }
}
