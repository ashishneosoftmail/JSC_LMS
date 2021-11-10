using AutoMapper;
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

namespace JSC_LMS.Application.Features.Superadmin.Queries.GetSuperadminById
{

    public class GetSuperAdminByIdQueryHandler : IRequestHandler<GetSuperAdminByIdQuery, Response<GetSuperadminByIdDto>>
    {
        private readonly ISuperadminRepository _superadminRepository;

        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetSuperAdminByIdQueryHandler(IMapper mapper, ISuperadminRepository superadminRepository, ILogger<GetSuperAdminByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _superadminRepository = superadminRepository;
            _logger = logger;
        }

        public async Task<Response<GetSuperadminByIdDto>> Handle(GetSuperAdminByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            Response<GetSuperadminByIdDto> responseData = new Response<GetSuperadminByIdDto>();
            var superadmin = (await _superadminRepository.ListAllAsync()).Where<JSC_LMS.Domain.Entities.Superadmin>(e => e.UserId == request.Id).FirstOrDefault();
            if (superadmin == null)
            {
                responseData.Succeeded = true;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }
            var superadminData = new GetSuperadminByIdDto()
            {
                Id = superadmin.Id,
                EmailSupport = superadmin.EmailSupport,
                MobileSupport = superadmin.MobileSupport,
                Name = superadmin.Name,
                LoginImage = superadmin.LoginImage,
                Logo = superadmin.Logo
            };
            _logger.LogInformation("Hanlde Completed");
            return new Response<GetSuperadminByIdDto>(superadminData, "success");
        }

    }
}
