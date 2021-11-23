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

namespace JSC_LMS.Application.Features.Circulars.Queries.GetCircularListByPagination
{

    public class GetAllCircularListByPaginationQueryHandler : IRequestHandler<GetAllCircularListByPaginationQuery, Response<IEnumerable<GetAllCircularListByPaginationDto>>>
    {
        private readonly ICircularRepository _circularRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetAllCircularListByPaginationQueryHandler(IMapper mapper, ICircularRepository circularRepository, ILogger
            <GetAllCircularListByPaginationQueryHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _circularRepository = circularRepository;
        }

        public async Task<Response<IEnumerable<GetAllCircularListByPaginationDto>>> Handle(GetAllCircularListByPaginationQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allCircularData = await _circularRepository.GetPagedReponseAsync(request.page, request.size);
            List<GetAllCircularListByPaginationDto> circularList = new List<GetAllCircularListByPaginationDto>();
            foreach (var circular in allCircularData)
            {
                circularList.Add(new GetAllCircularListByPaginationDto()
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
            return new Response<IEnumerable<GetAllCircularListByPaginationDto>>(circularList, "success");
        }

    }
}
