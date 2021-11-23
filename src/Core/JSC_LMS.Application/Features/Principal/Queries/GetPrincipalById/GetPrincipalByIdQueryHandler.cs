using AutoMapper;
using JSC_LMS.Application.CommonDtos;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using JSC_LMS.Infrastructure.EncryptDecrypt;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Principal.Queries.GetPrincipalById
{

    public class GetPrincipalByIdQueryHandler : IRequestHandler<GetPrincipalByIdQuery, Response<GetPrincipalByIdDto>>
    {
        private readonly IPrincipalRepository _principalRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetPrincipalByIdQueryHandler(IMapper mapper, IPrincipalRepository principalRepository, ISchoolRepository schoolRepository, IAuthenticationService authenticationService, ILogger<GetPrincipalByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _principalRepository = principalRepository;
            _schoolRepository = schoolRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public async Task<Response<GetPrincipalByIdDto>> Handle(GetPrincipalByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            Response<GetPrincipalByIdDto> responseData = new Response<GetPrincipalByIdDto>();
            var principal = await _principalRepository.GetByIdAsync(request.Id);
            if (principal == null)
            {
                responseData.Succeeded = true;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }
            var user = await _authenticationService.GetUserById(principal.UserId);


            var principalData = new GetPrincipalByIdDto()
            {
                Id = principal.Id,
                UserId = principal.UserId,
                AddressLine1 = principal.AddressLine1,
                AddressLine2 = principal.AddressLine2,
                Mobile = principal.Mobile,
                Username = user.Username,
                Email = user.Email,
                IsActive = principal.IsActive,
                Name = principal.Name,
                CreatedDate = (DateTime)principal.CreatedDate,
                City = _mapper.Map<CityDto>(principal.City),
                State = _mapper.Map<StateDto>(principal.State),
                Zip = _mapper.Map<ZipDto>(principal.Zip),
                School = new SchoolDto()
                {
                    Id = principal.SchoolId,
                    SchoolName = (await _schoolRepository.GetByIdAsync(principal.SchoolId)).SchoolName
                }
            };
            _logger.LogInformation("Hanlde Completed");
            return new Response<GetPrincipalByIdDto>(principalData, "success");
        }

    }
}
