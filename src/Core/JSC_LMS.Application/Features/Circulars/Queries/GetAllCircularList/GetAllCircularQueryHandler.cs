using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using JSC_LMS.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Circulars.Queries.GetAllCircularList
{
    public class GetAllCircularQueryHandler : IRequestHandler<GetAllCircularQuery, Response<IEnumerable<GetAllCircularListDto>>>
    {
        private readonly ICircularRepository _circularRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetAllCircularQueryHandler(IMapper mapper, ICircularRepository circularRepository, ILogger<GetAllCircularQueryHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _circularRepository = circularRepository;
        }

        public async Task<Response<IEnumerable<GetAllCircularListDto>>> Handle(GetAllCircularQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allCircularList = await _circularRepository.ListAllAsync();
            _logger.LogInformation("Hanlde Completed");
            List<GetAllCircularListDto> circularList = new List<GetAllCircularListDto>();
            foreach (var circular in allCircularList)
            {
                circularList.Add(new GetAllCircularListDto()
                {
                    Id = circular.Id,
                    CircularTitle = circular.CircularTitle,
                    Description = circular.Description,
                    File = circular.File,
                    IsActive = circular.IsActive,
                    SchoolId = circular.SchoolId,
                    Status = circular.Status,
                    SchoolData = new SchoolDto() { Id = circular.School.Id, SchoolName = circular.School.SchoolName }
                });
            }
            return new Response<IEnumerable<GetAllCircularListDto>>(circularList, "success");
        }
    }
}
