using AutoMapper;
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

namespace JSC_LMS.Application.Features.Class.Queries.GetClassByFilter
{
    public class GetClassByFilterQueryHandler : IRequestHandler<GetClassByFilterQuery, Response<IEnumerable<GetClassByFilterDto>>>
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


        public async Task<Response<IEnumerable<GetClassByFilterDto>>> Handle(GetClassByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allClass = await _classRepository.ListAllAsync();
            var searchFilter = (allClass.Where<JSC_LMS.Domain.Entities.Class>(x => (x.ClassName == request.ClassName) || (x.CreatedDate?.ToShortDateString() == request.CreatedDate.ToShortDateString()) || (x.IsActive == request.IsActive)).Select(x => (x)));

            if (request.ClassName != "Select Class")
            {
                allClass = allClass.Where<JSC_LMS.Domain.Entities.Class>(x => (x.ClassName == request.ClassName)).ToList();
            }
            if (request.CreatedDate.ToShortDateString() != "01-01-0001")
            {
                allClass = allClass.Where<JSC_LMS.Domain.Entities.Class>(x => x.CreatedDate?.ToShortDateString() == request.CreatedDate.ToShortDateString()).ToList();
            }
            if (request.IsActive)
            {
                allClass = allClass.Where<JSC_LMS.Domain.Entities.Class>(x => x.IsActive == request.IsActive).ToList();
            }
            else
            {
                allClass = allClass.Where<JSC_LMS.Domain.Entities.Class>(x => x.IsActive == request.IsActive).ToList();
            }
            Response<IEnumerable<GetClassByFilterDto>> responseData = new Response<IEnumerable<GetClassByFilterDto>>();
            List<GetClassByFilterDto> classList = new List<GetClassByFilterDto>();
            foreach (var classes in allClass)
            {
                if (request.SchoolName != "Select School")
                {
                    var school = (await _schoolRepository.GetByIdAsync(classes.SchoolId)).SchoolName == request.SchoolName;
                    if (school)
                    {
                     
                        classList.Add(new GetClassByFilterDto()
                        {
                            Id = classes.Id,

                            ClassName = classes.ClassName,


                            IsActive = classes.IsActive,

                            CreatedDate = (DateTime)classes.CreatedDate,

                            School = new SchoolFilterDtoVms()
                            {
                                Id = classes.SchoolId,
                                SchoolName = (await _schoolRepository.GetByIdAsync(classes.SchoolId)).SchoolName
                            }
                        });
                    }
                }
                else
                {
                    
                    classList.Add(new GetClassByFilterDto()
                    {
                        Id = classes.Id,

                        ClassName = classes.ClassName,


                        IsActive = classes.IsActive,

                        CreatedDate = (DateTime)classes.CreatedDate,

                        School = new SchoolFilterDtoVms()
                        {
                            Id = classes.SchoolId,
                            SchoolName = (await _schoolRepository.GetByIdAsync(classes.SchoolId)).SchoolName
                        }
                    });
                }
            }
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetClassByFilterDto>>(classList, "success");
        }



    }
}

