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

namespace JSC_LMS.Application.Features.Principal.Queries.GetPrincipalByFilter
{

    public class GetPrincipalByFilterQueryHandler : IRequestHandler<GetPrincipalByFilterQuery, Response<IEnumerable<GetPrincipalByFilterDto>>>
    {
        private readonly IPrincipalRepository _principalRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetPrincipalByFilterQueryHandler(IMapper mapper, IPrincipalRepository principalRepository, ISchoolRepository schoolRepository, IAuthenticationService authenticationService, ILogger<GetPrincipalByFilterQueryHandler> logger)
        {
            _mapper = mapper;
            _principalRepository = principalRepository;
            _schoolRepository = schoolRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }


        public async Task<Response<IEnumerable<GetPrincipalByFilterDto>>> Handle(GetPrincipalByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allPrincipal = await _principalRepository.ListAllAsync();
            var searchFilter = (allPrincipal.Where<JSC_LMS.Domain.Entities.Principal>(x => (x.Name == request.PrincipalName) && (x.CreatedDate == request.CreatedDate) && (x.IsActive == request.IsActive)).Select(x => (x)));
            Response<IEnumerable<GetPrincipalByFilterDto>> responseData = new Response<IEnumerable<GetPrincipalByFilterDto>>();
            if (searchFilter.Count() < 1)
            {
                responseData.Succeeded = true;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }
            List<GetPrincipalByFilterDto> principalList = new List<GetPrincipalByFilterDto>();
            foreach (var principal in searchFilter)
            {
                var school = (await _schoolRepository.GetByIdAsync(principal.SchoolId)).SchoolName == request.SchoolName;
                if (school)
                {
                    var user = await _authenticationService.GetUserById(principal.UserId);
                    principalList.Add(new GetPrincipalByFilterDto()
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
                        School = new SchoolFilterDto()
                        {
                            Id = principal.SchoolId,
                            SchoolName = (await _schoolRepository.GetByIdAsync(principal.SchoolId)).SchoolName
                        }
                    });
                }
            }
            if (principalList.Count() < 1)
            {
                responseData.Succeeded = true;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetPrincipalByFilterDto>>(principalList, "success");
        }


    }
}
