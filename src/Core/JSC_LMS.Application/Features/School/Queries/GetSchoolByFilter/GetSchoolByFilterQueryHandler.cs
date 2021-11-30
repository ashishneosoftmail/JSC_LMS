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

namespace JSC_LMS.Application.Features.School.Queries.GetSchoolByFilter
{
  public  class GetSchoolByFilterQueryHandler : IRequestHandler<GetSchoolByFilterQuery, Response<IEnumerable<GetSchoolByFilterDto>>>
    {
        private readonly IInstituteRepository _instituteRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetSchoolByFilterQueryHandler(IMapper mapper, IInstituteRepository instituteRepository, ISchoolRepository schoolRepository, IAuthenticationService authenticationService, ILogger<GetSchoolByFilterQueryHandler> logger)
        {
            _mapper = mapper;
            _instituteRepository = instituteRepository;
            _schoolRepository = schoolRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }
        public async Task<Response<IEnumerable<GetSchoolByFilterDto>>> Handle(GetSchoolByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allSchool = await _schoolRepository.ListAllAsync();
            var searchFilter = (allSchool.Where<JSC_LMS.Domain.Entities.School>(x => (x.Name == request.SchoolName) || (x.CreatedDate?.ToShortDateString() == request.CreatedDate.ToShortDateString()) || (x.IsActive == request.IsActive)).Select(x => (x)));

            if (request.SchoolName != "Select School")
            {
                allSchool = allSchool.Where<JSC_LMS.Domain.Entities.School>(x => (x.SchoolName == request.SchoolName)).ToList();
            }
            if (request.CreatedDate.ToShortDateString() != "01-01-0001")
            {
                allSchool = allSchool.Where<JSC_LMS.Domain.Entities.School>(x => x.CreatedDate?.ToShortDateString() == request.CreatedDate.ToShortDateString()).ToList();
            }
            if (request.IsActive)
            {
                allSchool = allSchool.Where<JSC_LMS.Domain.Entities.School>(x => x.IsActive == request.IsActive).ToList();
            }
            else
            {
                allSchool = allSchool.Where<JSC_LMS.Domain.Entities.School>(x => x.IsActive == request.IsActive).ToList();
            }
            Response<IEnumerable<GetSchoolByFilterDto>> responseData = new Response<IEnumerable<GetSchoolByFilterDto>>();
            List<GetSchoolByFilterDto> schoolList = new List<GetSchoolByFilterDto>();
            foreach (var school in allSchool)
            {
                if (request.InstituteName != "Select InstituteName")
                {
                    var institute = (await _instituteRepository.GetByIdAsync(school.Id)).InstituteName == request.InstituteName;
                    if (institute)
                    {
                  
                        schoolList.Add(new GetSchoolByFilterDto()
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
                            Institute = new InstituteFilterDto()
                            {
                                Id = school.Id,
                                InstituteName = (await _instituteRepository.GetByIdAsync(school.Id)).InstituteName
                            }
                        });
                    }
                }


            }
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetSchoolByFilterDto>>(schoolList, "success");
        }


    }
}
