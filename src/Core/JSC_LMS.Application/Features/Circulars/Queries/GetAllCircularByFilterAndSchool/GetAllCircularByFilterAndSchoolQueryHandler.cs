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

namespace JSC_LMS.Application.Features.Circulars.Queries.GetAllCircularByFilterAndSchool
{

    public class GetAllCircularByFilterAndSchoolQueryHandler : IRequestHandler<GetAllCircularByFilterAndSchoolQuery, Response<IEnumerable<GetAllCircularByFilterAndSchoolDto>>>
    {
        private readonly ICircularRepository _circularRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetAllCircularByFilterAndSchoolQueryHandler(IMapper mapper, ICircularRepository circularRepository, ILogger
            <GetAllCircularByFilterAndSchoolQueryHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _circularRepository = circularRepository;
        }

        public async Task<Response<IEnumerable<GetAllCircularByFilterAndSchoolDto>>> Handle(GetAllCircularByFilterAndSchoolQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allCircular = (await _circularRepository.ListAllAsync()).Where<Circular>(x => x.SchoolId == request.SchoolId);


            if (request.CircularTitle != null)
            {
                allCircular = allCircular.Where<Circular>(x => (x.CircularTitle.Contains(request.CircularTitle))).ToList();
            }

            if (request.Description != null)
            {
                allCircular = allCircular.Where<Circular>(x => (x.Description.Contains(request.Description))).ToList();
            }
            if (request.Status)
            {
                allCircular = allCircular.Where<Circular>(x => x.Status == request.Status).ToList();
            }
            else
            {
                allCircular = allCircular.Where<Circular>(x => x.Status == request.Status).ToList();
            }
            List<GetAllCircularByFilterAndSchoolDto> responseData = new List<GetAllCircularByFilterAndSchoolDto>();

            foreach (var circular in allCircular)
            {

                responseData.Add(new GetAllCircularByFilterAndSchoolDto()
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
            return new Response<IEnumerable<GetAllCircularByFilterAndSchoolDto>>(responseData, "success");
        }

    }
}
