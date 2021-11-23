using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Circulars.Queries.GetCircularById
{

    public class GetCircularByIdQueryHandler : IRequestHandler<GetCircularByIdQuery, Response<GetCircularByIdDto>>
    {
        private readonly ICircularRepository _circularRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetCircularByIdQueryHandler(IMapper mapper, ICircularRepository circularRepository, ILogger<GetCircularByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _circularRepository = circularRepository;
            _logger = logger;
        }

        public async Task<Response<GetCircularByIdDto>> Handle(GetCircularByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            Response<GetCircularByIdDto> responseData = new Response<GetCircularByIdDto>();
            var circular = await _circularRepository.GetByIdAsync(request.Id);
            if (circular == null)
            {
                responseData.Succeeded = true;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }
            var circularData = new GetCircularByIdDto()
            {
                Id = circular.Id,
                CircularTitle = circular.CircularTitle,
                Description = circular.Description,
                File = circular.File,
                IsActive = circular.IsActive,
                SchoolId = circular.SchoolId,
                Status = circular.Status,
                SchoolData = new SchoolDto() { Id = circular.School.Id, SchoolName = circular.School.SchoolName }
            };
            _logger.LogInformation("Hanlde Completed");
            return new Response<GetCircularByIdDto>(circularData, "success");
        }

    }
}
