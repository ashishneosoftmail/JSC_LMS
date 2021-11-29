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

namespace JSC_LMS.Application.Features.Circulars.Queries.GetCircularListByPaginationBySchool
{

    public class GetCircularListByPaginationBySchoolQueryHandler : IRequestHandler<GetCircularListByPaginationBySchoolQuery, Response<IEnumerable<GetCircularListByPaginationBySchoolDto>>>
    {
        private readonly ICircularRepository _circularRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetCircularListByPaginationBySchoolQueryHandler(IMapper mapper, ICircularRepository circularRepository, ILogger
            <GetCircularListByPaginationBySchoolQueryHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _circularRepository = circularRepository;
        }

        public async Task<Response<IEnumerable<GetCircularListByPaginationBySchoolDto>>> Handle(GetCircularListByPaginationBySchoolQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allCircularData = await _circularRepository.PrincipalGetPagedReponseAsyncBySchoolId(request.page, request.size, request.SchoolId);
            List<GetCircularListByPaginationBySchoolDto> circularList = new List<GetCircularListByPaginationBySchoolDto>();
            foreach (var circular in allCircularData)
            {
                circularList.Add(new GetCircularListByPaginationBySchoolDto()
                {
                    Id = circular.Id,
                    CircularTitle = circular.CircularTitle,
                    Description = circular.Description,
                    File = circular.File,
                    IsActive = circular.IsActive,
                    SchoolId = circular.SchoolId,
                    Status = circular.Status,
                    CreatedDate = circular.CreatedDate,
                    SchoolData = new SchoolDto() { Id = circular.School.Id, SchoolName = circular.School.SchoolName }
                });
            }

            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetCircularListByPaginationBySchoolDto>>(circularList, "success");
        }

    }
}
