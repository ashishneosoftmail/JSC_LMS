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

namespace JSC_LMS.Application.Features.Class.Queries.GetClassByPagination
{
   public class GetClassByPaginationQueryHandler : IRequestHandler<GetClassByPaginationQuery, Response<GetClassListByPaginationResponse>>
    {
        private readonly IClassRepository _classRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetClassByPaginationQueryHandler(IMapper mapper, IClassRepository classRepository, ISchoolRepository schoolRepository, IAuthenticationService authenticationService, ILogger<GetClassByPaginationQueryHandler> logger)
        {
            _mapper = mapper;
            _classRepository = classRepository;
            _schoolRepository = schoolRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public async Task<Response<GetClassListByPaginationResponse>> Handle(GetClassByPaginationQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allClassCount = await _classRepository.ListAllAsync();
            var allClass = await _classRepository.GetPagedReponseAsync(request.page, request.size);
            GetClassListByPaginationResponse getclassListByPaginationResponse = new GetClassListByPaginationResponse();
            List<GetClassListPaginationDto> classList = new List<GetClassListPaginationDto>();
            foreach (var classes in allClass)
            {

                classList.Add(new GetClassListPaginationDto()
                {
                    Id = classes.Id,
               
                    IsActive = classes.IsActive,
                    ClassName = classes.ClassName,
                    CreatedDate = (DateTime)classes.CreatedDate,
                 
                    School = new SchoolPaginationDto()
                    {
                        Id = classes.SchoolId,
                        SchoolName = (await _schoolRepository.GetByIdAsync(classes.SchoolId)).SchoolName
                    }
                });
            }
            getclassListByPaginationResponse.GetClassListPaginationDto = classList;
            getclassListByPaginationResponse.Count = allClassCount.Count();

            /* var principal = _mapper.Map<List<GetPrincipalListDto>>(allPrincipal);
             _logger.LogInformation("Hanlde Completed");
             return new Response<IEnumerable<GetPrincipalListDto>>(principal, "success");*/
            _logger.LogInformation("Hanlde Completed");
            return new Response<GetClassListByPaginationResponse>(getclassListByPaginationResponse, "success");
        }

    }
}
