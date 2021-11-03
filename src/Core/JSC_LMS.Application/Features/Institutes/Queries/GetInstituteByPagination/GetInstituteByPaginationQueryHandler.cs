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

namespace JSC_LMS.Application.Features.Institutes.Queries.GetInstituteByPagination
{
   


    public class GetInstituteByPaginationQueryHandler : IRequestHandler<GetInstituteByPaginationQuery, Response<GetInstituteListByPaginationResponse>>
    {
        private readonly IInstituteRepository _instituteRepository;        
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetInstituteByPaginationQueryHandler(IMapper mapper, IInstituteRepository instituteRepository, IAuthenticationService authenticationService, ILogger
            <GetInstituteByPaginationQueryHandler> logger)
        {
            _mapper = mapper;
            _instituteRepository = instituteRepository;            
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public async Task<Response<GetInstituteListByPaginationResponse>> Handle(GetInstituteByPaginationQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allInstituteCount = await _instituteRepository.ListAllAsync();
            var allInstitute = await _instituteRepository.GetPagedReponseAsync(request.page, request.size);
            GetInstituteListByPaginationResponse getInstituteListByPaginationResponse = new GetInstituteListByPaginationResponse();
            List<GetInstituteListPaginationDto> instituteList = new List<GetInstituteListPaginationDto>();
            foreach (var institute in allInstitute)
            {
                var user = await _authenticationService.GetUserById(institute.UserId);
                instituteList.Add(new GetInstituteListPaginationDto()
                {
                    Id = institute.Id,
                    InstituteName = institute.InstituteName,
                    AddressLine1 = institute.AddressLine1,
                    AddressLine2 = institute.AddressLine2,
                    Mobile = institute.Mobile,
                    Username = user.Username,
                    Email = user.Email,
                    IsActive = institute.IsActive,
                    ContactPerson = institute.ContactPerson,
                    LicenseExpiry = (DateTime)institute.LicenseExpiry,
                    CreatedDate = (DateTime)institute.CreatedDate,
                    City = _mapper.Map<CityDto>(institute.City),
                    State = _mapper.Map<StateDto>(institute.State),
                    Zip = _mapper.Map<ZipDto>(institute.Zip),
                });
            }
            getInstituteListByPaginationResponse.GetInstituteListPaginationDto = instituteList;
            getInstituteListByPaginationResponse.Count = allInstituteCount.Count();

            /* var principal = _mapper.Map<List<GetPrincipalListDto>>(allPrincipal);
             _logger.LogInformation("Hanlde Completed");
             return new Response<IEnumerable<GetPrincipalListDto>>(principal, "success");*/
            _logger.LogInformation("Hanlde Completed");
            return new Response<GetInstituteListByPaginationResponse>(getInstituteListByPaginationResponse, "success");
        }

    }
}
