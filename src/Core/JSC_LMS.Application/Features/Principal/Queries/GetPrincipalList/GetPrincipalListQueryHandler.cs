using AutoMapper;
using JSC_LMS.Application.CommonDtos;
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

namespace JSC_LMS.Application.Features.Principal.Queries.GetPrincipalList
{

    public class GetPrincipalListQueryHandler : IRequestHandler<GetPrincipalListQuery, Response<IEnumerable<GetPrincipalListDto>>>
    {
        private readonly IPrincipalRepository _principalRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetPrincipalListQueryHandler(IMapper mapper, IPrincipalRepository principalRepository, ISchoolRepository schoolRepository, IAuthenticationService authenticationService, ILogger<GetPrincipalListQueryHandler> logger)
        {
            _mapper = mapper;
            _principalRepository = principalRepository;
            _schoolRepository = schoolRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public async Task<Response<IEnumerable<GetPrincipalListDto>>> Handle(GetPrincipalListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allPrincipal = await _principalRepository.ListAllAsync();
            List<GetPrincipalListDto> principalList = new List<GetPrincipalListDto>();
            foreach (var principal in allPrincipal)
            {
                var user = await _authenticationService.GetUserById(principal.UserId);
                principalList.Add(new GetPrincipalListDto()
                {
                    Id = principal.Id,
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
                });
            }
            /* var principal = _mapper.Map<List<GetPrincipalListDto>>(allPrincipal);
             _logger.LogInformation("Hanlde Completed");
             return new Response<IEnumerable<GetPrincipalListDto>>(principal, "success");*/
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetPrincipalListDto>>(principalList, "success");
        }

    }
}
