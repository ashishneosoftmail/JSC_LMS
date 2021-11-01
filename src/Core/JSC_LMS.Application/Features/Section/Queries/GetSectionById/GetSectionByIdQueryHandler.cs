using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Section.Queries.GetSectionById
{
   public class GetSectionByIdQueryHandler : IRequestHandler<GetSectionByIdQuery, Response<GetSectionByIdDto>>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetSectionByIdQueryHandler(IMapper mapper, ISectionRepository sectionRepository, IClassRepository classRepository, ISchoolRepository schoolRepository, IAuthenticationService authenticationService, ILogger<GetSectionByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _sectionRepository = sectionRepository;
            _classRepository = classRepository;
            _schoolRepository = schoolRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public async Task<Response<GetSectionByIdDto>> Handle(GetSectionByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            Response<GetSectionByIdDto> responseData = new Response<GetSectionByIdDto>();
            var classdata = await _sectionRepository.GetByIdAsync(request.Id);
            if (classdata == null)
            {
                responseData.Succeeded = true;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }

            var classData = new GetSectionByIdDto()
            {
                Id = classdata.Id,

                IsActive = classdata.IsActive,
                SectionName = classdata.SectionName,
                CreatedDate = (DateTime)classdata.CreatedDate,

                School = new SchoolDto()
                {
                    Id = classdata.SchoolId,
                    SchoolName = (await _schoolRepository.GetByIdAsync(classdata.SchoolId)).SchoolName
                },

                Class = new ClassDto()
                {
                    Id = classdata.SchoolId,
                    ClassName = (await _schoolRepository.GetByIdAsync(classdata.ClassId)).ClassName
                }

            };
            _logger.LogInformation("Hanlde Completed");
            return new Response<GetSectionByIdDto>(classData, "success");
        }

    }

}

