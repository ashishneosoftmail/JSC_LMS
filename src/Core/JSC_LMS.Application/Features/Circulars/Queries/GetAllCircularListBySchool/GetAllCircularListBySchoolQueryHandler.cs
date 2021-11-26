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

namespace JSC_LMS.Application.Features.Circulars.Queries.GetAllCircularListBySchool
{

    public class GetAllCircularListBySchoolQueryHandler : IRequestHandler<GetAllCircularListBySchoolQuery, Response<IEnumerable<GetAllCircularListBySchoolDto>>>
    {
        private readonly ICircularRepository _circularRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetAllCircularListBySchoolQueryHandler(IMapper mapper, ICircularRepository circularRepository, ILogger<GetAllCircularListBySchoolQueryHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _circularRepository = circularRepository;
        }

        public async Task<Response<IEnumerable<GetAllCircularListBySchoolDto>>> Handle(GetAllCircularListBySchoolQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allCircularList = (await _circularRepository.ListAllAsync()).Where<Circular>(x => x.SchoolId == request.SchoolId);
            _logger.LogInformation("Hanlde Completed");
            List<GetAllCircularListBySchoolDto> circularList = new List<GetAllCircularListBySchoolDto>();
            foreach (var circular in allCircularList)
            {
                circularList.Add(new GetAllCircularListBySchoolDto()
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
            return new Response<IEnumerable<GetAllCircularListBySchoolDto>>(circularList, "success");
        }
    }
}
