using AutoMapper;
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

namespace JSC_LMS.Application.Features.Class.Queries.GetClassByFilter
{
    public class GetClassByFilterQueryHandler : IRequestHandler<GetClassByFilterQuery, Response<IEnumerable<SchoolFilterDtoVms>>>
    {
        private readonly IClassRepository _classRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetClassByFilterQueryHandler(IMapper mapper, IClassRepository classRepository, ISchoolRepository schoolRepository, IAuthenticationService authenticationService, ILogger<GetClassByFilterQueryHandler> logger)
        {
            _mapper = mapper;
            _classRepository = classRepository;
            _schoolRepository = schoolRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }


        public async Task<Response<IEnumerable<SchoolFilterDtoVms>>> Handle(GetClassByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allClass = await _classRepository.ListAllAsync();
            var searchFilter = (allClass.Where<JSC_LMS.Domain.Entities.Class>(x => (x.ClassName == request.ClassName) && (x.CreatedDate == request.CreatedDate) && (x.IsActive == request.IsActive)).Select(x => (x)));
            Response<IEnumerable<SchoolFilterDtoVms>> responseData = new Response<IEnumerable<SchoolFilterDtoVms>>();
            if (searchFilter.Count() < 1)
            {
                responseData.Succeeded = true;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }
            List<SchoolFilterDtoVms> classList = new List<SchoolFilterDtoVms>();
            foreach (var classes in searchFilter)
            {
                var school = (await _schoolRepository.GetByIdAsync(classes.SchoolId)).SchoolName == request.SchoolName;
                if (school)
                {

                    classList.Add(new SchoolFilterDtoVms()
                    {
                        Id = classes.Id,

                        ClassName = classes.ClassName,
                       
                       
                        IsActive = classes.IsActive,
                      
                        CreatedDate = (DateTime)classes.CreatedDate,
                       
                        School = new SchoolDtoVm()
                        {
                            Id = classes.SchoolId,
                            SchoolName = (await _schoolRepository.GetByIdAsync(classes.SchoolId)).SchoolName
                        }
                    });
                }
            }
            if (classList.Count() < 1)
            {
                responseData.Succeeded = true;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<SchoolFilterDtoVms>>(classList, "success");
        }


    }
}

