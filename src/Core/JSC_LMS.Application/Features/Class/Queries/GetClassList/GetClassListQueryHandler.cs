using AutoMapper;
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

namespace JSC_LMS.Application.Features.Class.Queries.GetClassList
{
    public class GetClassListQueryHandler : IRequestHandler<GetClassListQuery, Response<IEnumerable<GetClassListDto>>>
    {
        private readonly IClassRepository _classRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetClassListQueryHandler(IMapper mapper, IClassRepository classRepository, ISchoolRepository schoolRepository, IAuthenticationService authenticationService, ILogger<GetClassListQueryHandler> logger)
        {
            _mapper = mapper;
            _classRepository = classRepository;
            _schoolRepository = schoolRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public async Task<Response<IEnumerable<GetClassListDto>>> Handle(GetClassListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allClass = await _classRepository.ListAllAsync();
            List<GetClassListDto> classList = new List<GetClassListDto>();
            foreach (var classes in allClass)
            {
                classList.Add(new GetClassListDto()
                {
                    Id = classes.Id,
                    CreatedDate = (DateTime)classes.CreatedDate,
                    ClassName = classes.ClassName,
                    IsActive = classes.IsActive,
                    School = new SchoolDto()
                    {
                        Id = classes.SchoolId,
                        SchoolName = (await _schoolRepository.GetByIdAsync(classes.SchoolId)).SchoolName
                    }
                });
            }
            /* var principal = _mapper.Map<List<GetPrincipalListDto>>(allPrincipal);
             _logger.LogInformation("Hanlde Completed");
             return new Response<IEnumerable<GetPrincipalListDto>>(principal, "success");*/
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetClassListDto>>(classList, "success");
        }

    }

}

