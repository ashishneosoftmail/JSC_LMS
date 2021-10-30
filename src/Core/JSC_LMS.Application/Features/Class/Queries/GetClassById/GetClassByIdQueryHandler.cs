using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Features.Class.Queries.GetClassById;
using JSC_LMS.Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Class.Queries.GetClassById
{
 
    
        public class GetClassByIdQueryHandler : IRequestHandler<GetClassByIdQuery, Response<GetClassByIdDto>>
        {
            private readonly IClassRepository _classRepository;
            private readonly ISchoolRepository _schoolRepository;
            private readonly IAuthenticationService _authenticationService;
            private readonly IMapper _mapper;
            private readonly ILogger _logger;
            public GetClassByIdQueryHandler(IMapper mapper, IClassRepository classRepository, ISchoolRepository schoolRepository, IAuthenticationService authenticationService, ILogger<GetClassByIdQueryHandler> logger)
            {
                _mapper = mapper;
                _classRepository = classRepository;
                _schoolRepository = schoolRepository;
                _logger = logger;
                _authenticationService = authenticationService;
            }

            public async Task<Response<GetClassByIdDto>> Handle(GetClassByIdQuery request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("Handle Initiated");
                Response<GetClassByIdDto> responseData = new Response<GetClassByIdDto>();
                var classdata = await _classRepository.GetByIdAsync(request.Id);
                if (classdata == null)
                {
                    responseData.Succeeded = true;
                    responseData.Message = "Data Doesn't Exist";
                    responseData.Data = null;
                    return responseData;
                }
              
                var classData = new GetClassByIdDto()
                {
                    Id = classdata.Id,
                 
                    IsActive = classdata.IsActive,
                    ClassName = classdata.ClassName,
                    CreatedDate = (DateTime)classdata.CreatedDate,
                   
                    School = new SchoolDto()
                    {
                        Id = classdata.SchoolId,
                        SchoolName = (await _schoolRepository.GetByIdAsync(classdata.SchoolId)).SchoolName
                    }
                };
                _logger.LogInformation("Hanlde Completed");
                return new Response<GetClassByIdDto>(classData, "success");
            }

        }

    }

