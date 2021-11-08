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

namespace JSC_LMS.Application.Features.Institutes.Queries.GetInstituteFilter
{
    public class GetInstituteFilterQueryHandler : IRequestHandler<GetInstituteFilterQuery, Response<IEnumerable<GetInstituteFilterVm>>>
    {
        private readonly IInstituteRepository _instituteRepository;        
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetInstituteFilterQueryHandler(IMapper mapper, IInstituteRepository instituteRepository,  IAuthenticationService authenticationService, ILogger<GetInstituteFilterQueryHandler> logger)
        {
            _mapper = mapper;
            _instituteRepository = instituteRepository;            
            _logger = logger;
            _authenticationService = authenticationService;
        }


        public async Task<Response<IEnumerable<GetInstituteFilterVm>>> Handle(GetInstituteFilterQuery request, CancellationToken cancellationToken)
        {
            
            _logger.LogInformation("Handle Initiated");
            var allInstitute = await _instituteRepository.ListAllAsync();
            var searchFilter = (allInstitute.Where<JSC_LMS.Domain.Entities.Institute>(x => (x.InstituteName == request.InstituteName) && (x.LicenseExpiry.ToShortDateString() == request.LicenseExpiry.ToShortDateString()) && (x.IsActive == request.IsActive) && (x.City.CityName == request.Cityname) && (x.State.StateName == request.Statename)).Select(x => (x)));


            Response<IEnumerable<GetInstituteFilterVm>> responseData = new Response<IEnumerable<GetInstituteFilterVm>>();
            if (searchFilter.Count() < 1)
            {
                responseData.Succeeded = true;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }
            List<GetInstituteFilterVm> instituteList = new List<GetInstituteFilterVm>();
            foreach (var institute in searchFilter)
            {
               
                    var user = await _authenticationService.GetUserById(institute.UserId);
                instituteList.Add(new GetInstituteFilterVm()
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
                        CreatedDate =(DateTime)institute.CreatedDate,
                        City = _mapper.Map<CityDto>(institute.City),
                        State = _mapper.Map<StateDto>(institute.State),
                        Zip = _mapper.Map<ZipDto>(institute.Zip),
                        
                    });
                
            }
            //if (instituteList.Count() < 1)
            //{
            //    responseData.Succeeded = true;
            //    responseData.Message = "Data Doesn't Exist";
            //    responseData.Data = null;
            //    return responseData;
            //}
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetInstituteFilterVm>>(instituteList, "success");
        }


    }
}
