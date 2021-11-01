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

namespace JSC_LMS.Application.Features.School.Queries.GetSchoolById
{
    public class GetSchoolByIdQueryHandler : IRequestHandler<GetSchoolByIdQuery, Response<GetSchoolByIdDto>>
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly IInstituteRepository _instituteRepository;

        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetSchoolByIdQueryHandler(IMapper mapper, ISchoolRepository schoolRepository, IInstituteRepository instituteRepository, IAuthenticationService authenticationService, ILogger<GetSchoolByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _schoolRepository = schoolRepository;
            _instituteRepository = instituteRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public async Task<Response<GetSchoolByIdDto>> Handle(GetSchoolByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            Response<GetSchoolByIdDto> responseData = new Response<GetSchoolByIdDto>();
            var school = await _schoolRepository.GetByIdAsync(request.Id);
            if (school == null)
            {
                responseData.Succeeded = true;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }
            //var user = await _authenticationService.GetUserById(school.UserId);

            var schoolData = new GetSchoolByIdDto()
            {
                Id = school.Id,
                AddressLine1 = school.Address1,
                AddressLine2 = school.Address2,
                SchoolName = school.SchoolName,
                Mobile = school.Mobile,
                ContactPerson = school.ContactPerson,
                Email =school.Email,
                IsActive = school.IsActive, 
                City = _mapper.Map<CityDto>(school.City),
                State = _mapper.Map<StateDto>(school.State),
                Zip = _mapper.Map<ZipDto>(school.Zip),
               Institute = new InstituteDto()
               {
                   Id = school.InstituteId,
                   InstituteName = (await _instituteRepository.GetByIdAsync(school.InstituteId)).InstituteName
               }
            };
            _logger.LogInformation("Hanlde Completed");
            return new Response<GetSchoolByIdDto>(schoolData, "success");
        }
    }
}
