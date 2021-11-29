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

namespace JSC_LMS.Application.Features.ParentsFeature.Queries.GetParentByUserId
{
    public class GetParentByUserIdQueryHandler : IRequestHandler<GetParentByUserIdQuery, Response<GetParentByUserIdDto>>
    {
        private readonly IParentsRepository _parentRepository;

        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetParentByUserIdQueryHandler(IMapper mapper, IParentsRepository parentRepository, ILogger<GetParentByUserIdQueryHandler> logger)
        {
            _mapper = mapper;
            _parentRepository = parentRepository;
            _logger = logger;
        }

        public async Task<Response<GetParentByUserIdDto>> Handle(GetParentByUserIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            Response<GetParentByUserIdDto> responseData = new Response<GetParentByUserIdDto>();
            var parent = (await _parentRepository.ListAllAsync()).Where<JSC_LMS.Domain.Entities.Parents>(e => e.UserId == request.UserId).FirstOrDefault();
            if (parent == null)
            {
                responseData.Succeeded = true;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }
            var parentData = new GetParentByUserIdDto()
            {
                Id = parent.Id,
                Name = parent.ParentName,
                Mobile = parent.Mobile,
                SchoolId = parent.SchoolId
            };
            _logger.LogInformation("Hanlde Completed");
            return new Response<GetParentByUserIdDto>(parentData, "success");
        }
    }
}
