using AutoMapper;
using JSC_LMS.Application.CommonDtos;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Features.Institutes.Queries.GetInstituteList;
using JSC_LMS.Application.Features.Principal.Queries.GetPrincipalById;
using JSC_LMS.Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Institutes.Queries.GetInstituteById
{

    //public class GetInstituteQueryHandler : IRequestHandler<GetInstituteQuery, Response<GetInstituteListVm>>
    //{
    //    private readonly IMapper _mapper;
    //    private readonly IInstituteRepository _instituteRepository;

    //    public GetInstituteQueryHandler(IMapper mapper, IInstituteRepository instituteRepository)
    //    {
    //        _mapper = mapper;
    //        _instituteRepository = instituteRepository;
    //    }

    //    public async Task<Response<GetInstituteListVm>> Handle(GetInstituteQuery request, CancellationToken cancellationToken)
    //    {
    //        var list = await _instituteRepository.GetByIdAsync(request.Id);
    //        var instituteList = _mapper.Map<GetInstituteListVm>(list);
    //        return new Response<GetInstituteListVm>(instituteList, "success");
    //    }


    //}

    public class GetInstituteByIdQueryHandler : IRequestHandler<GetInstituteQuery, Response<GetInstituteByIdVm>>
    {
        private readonly IInstituteRepository _instituteRepository;        
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetInstituteByIdQueryHandler(IMapper mapper, IInstituteRepository instituteRepository, IAuthenticationService authenticationService, ILogger<GetPrincipalByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _instituteRepository = instituteRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public async Task<Response<GetInstituteByIdVm>> Handle(GetInstituteQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            Response<GetInstituteByIdVm> responseData = new Response<GetInstituteByIdVm>();
            var institute = await _instituteRepository.GetByIdAsync(request.Id);
            if (institute == null)
            {
                responseData.Succeeded = true;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }
            var user = await _authenticationService.GetUserById(institute.UserId);
            var instituteData = new GetInstituteByIdVm()
            {
                Id = institute.Id,
                UserId = institute.UserId,
                InstituteName = institute.InstituteName,
                InstituteURL = institute.InstituteURL,
                AddressLine1 = institute.AddressLine1,
                AddressLine2 = institute.AddressLine2,
                Mobile = institute.Mobile,
                Username = user.Username,
                Email = user.Email,
                IsActive = institute.IsActive,
                ContactPerson = institute.ContactPerson,
                CreatedDate = (DateTime)institute.CreatedDate,
                City = _mapper.Map<CityDto>(institute.City),
                State = _mapper.Map<StateDto>(institute.State),
                Zip = _mapper.Map<ZipDto>(institute.Zip),
                LicenseExpiry= institute.LicenseExpiry
               
            };
            _logger.LogInformation("Hanlde Completed");
            return new Response<GetInstituteByIdVm>(instituteData, "success");
        }

    }
}
