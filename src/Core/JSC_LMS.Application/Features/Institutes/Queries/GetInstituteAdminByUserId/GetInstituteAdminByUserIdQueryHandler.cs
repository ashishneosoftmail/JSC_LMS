using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using JSC_LMS.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Institutes.Queries.GetInstituteAdminByUserId
{

    public class GetInstituteAdminByUserIdQueryHandler : IRequestHandler<GetInstituteAdminByUserIdQuery, Response<GetInstituteAdminByUserIdDto>>
    {
        private readonly IInstituteRepository _instituteadminRepository;

        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetInstituteAdminByUserIdQueryHandler(IMapper mapper, IInstituteRepository instituteadminRepository, ILogger<GetInstituteAdminByUserIdQueryHandler> logger)
        {
            _mapper = mapper;
            _instituteadminRepository = instituteadminRepository;
            _logger = logger;
        }

        public async Task<Response<GetInstituteAdminByUserIdDto>> Handle(GetInstituteAdminByUserIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            Response<GetInstituteAdminByUserIdDto> responseData = new Response<GetInstituteAdminByUserIdDto>();
            var instituteadmin = (await _instituteadminRepository.ListAllAsync()).Where<Institute>(e => e.UserId == request.UserId).FirstOrDefault();
            if (instituteadmin == null)
            {
                responseData.Succeeded = true;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }
            var instituteadminData = new GetInstituteAdminByUserIdDto()
            {
                Id = instituteadmin.Id,
                Name = instituteadmin.ContactPerson,
                Mobile = instituteadmin.Mobile
             
            };
            _logger.LogInformation("Hanlde Completed");
            return new Response<GetInstituteAdminByUserIdDto>(instituteadminData, "success");
        }

    }
}
