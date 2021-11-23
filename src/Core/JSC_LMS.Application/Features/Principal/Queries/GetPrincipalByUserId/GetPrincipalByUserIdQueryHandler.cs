using AutoMapper;
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

namespace JSC_LMS.Application.Features.Principal.Queries.GetPrincipalByUserId
{
   public class GetPrincipalByUserIdQueryHandler : IRequestHandler<GetPrincipalByUserIdQuery, Response<GetPrincipalByUserIdDto>>
    {
        private readonly IPrincipalRepository _principalRepository;

        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetPrincipalByUserIdQueryHandler(IMapper mapper, IPrincipalRepository principalRepository, ILogger<GetPrincipalByUserIdQueryHandler> logger)
        {
            _mapper = mapper;
            _principalRepository = principalRepository;
            _logger = logger;
        }

        public async Task<Response<GetPrincipalByUserIdDto>> Handle(GetPrincipalByUserIdQuery request, CancellationToken cancellationToken)
        {
            
                _logger.LogInformation("Handle Initiated");
                Response<GetPrincipalByUserIdDto> responseData = new Response<GetPrincipalByUserIdDto>();
                var principal = (await _principalRepository.ListAllAsync()).Where<JSC_LMS.Domain.Entities.Principal>(e => e.UserId == request.UserId).FirstOrDefault();
                if (principal == null)
            {
                responseData.Succeeded = true;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }
            var principalData = new GetPrincipalByUserIdDto()
            {
                Id = principal.Id,
                Name = principal.Name,
                Mobile = principal.Mobile
            };
            _logger.LogInformation("Hanlde Completed");
            return new Response<GetPrincipalByUserIdDto>(principalData, "success");
        }


    }
}
