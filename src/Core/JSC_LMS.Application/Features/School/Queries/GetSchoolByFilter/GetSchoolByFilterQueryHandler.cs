using AutoMapper;
using JSC_LMS.Application.CommonDtos;
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

namespace JSC_LMS.Application.Features.School.Queries.GetSchoolByFilter
{
    public class GetSchoolByFilterQueryHandler : IRequestHandler<GetSchoolByFilterQuery, Response<IEnumerable<GetSchoolByFilterDto>>>
    {
        
        private readonly ISchoolRepository _schoolRepository;
        private readonly IInstituteRepository _instituteRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private object schoolList;

        public GetSchoolByFilterQueryHandler(IMapper mapper, ISchoolRepository schoolRepository, IInstituteRepository instituteRepository, IAuthenticationService authenticationService, ILogger<GetSchoolByFilterQueryHandler> logger)
        {
            _mapper = mapper;
            _schoolRepository = schoolRepository;
            _instituteRepository = instituteRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }
        public async Task<Response<IEnumerable<GetSchoolByFilterDto>>> Handle(GetSchoolByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allSchool = await _schoolRepository.ListAllAsync();
            var searchFilter = (allSchool.Where<JSC_LMS.Domain.Entities.School>(x => (x.Name == request.SchoolName)  && (x.IsActive == request.IsActive)).Select(x => (x)));
            Response<IEnumerable<GetSchoolByFilterDto>> responseData = new Response<IEnumerable<GetSchoolByFilterDto>>();

            if (searchFilter.Count() < 1)
            {
                responseData.Succeeded = true;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }

            List<GetSchoolByFilterDto> schoolList = new List<GetSchoolByFilterDto>();
            foreach (var school in searchFilter)
            {
                var institute = (await _instituteRepository.GetByIdAsync(school.InstituteId)).InstituteName == request.InstituteName;
                if (institute)
                {
                    //var user = await _authenticationService.GetUserById(principal.UserId);
                    schoolList.Add(new GetSchoolByFilterDto()
                    {
                        Id = school.Id,
                        AddressLine1 = school.Address1,
                        AddressLine2 = school.Address2,
                        SchoolName = school.SchoolName,
                        Mobile = school.Mobile,
                        ContactPerson = school.ContactPerson,
                        Email = school.Email,
                        IsActive = school.IsActive,
                        City = _mapper.Map<CityDto>(school.City),
                        State = _mapper.Map<StateDto>(school.State),
                        Zip = _mapper.Map<ZipDto>(school.Zip),
                        Institute = new InstituteFilterDto()
                        {
                            Id = school.InstituteId,
                            InstituteName = (await _instituteRepository.GetByIdAsync(school.InstituteId)).InstituteName
                        }
                    });
                }
            }


            if (schoolList.Count() < 1)
            {
                responseData.Succeeded = true;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetSchoolByFilterDto>>(schoolList, "success");

        }
    }
}
    
