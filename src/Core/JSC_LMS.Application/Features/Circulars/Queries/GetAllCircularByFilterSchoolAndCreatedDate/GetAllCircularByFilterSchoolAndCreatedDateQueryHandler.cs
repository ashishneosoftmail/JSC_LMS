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

namespace JSC_LMS.Application.Features.Circulars.Queries.GetAllCircularByFilterSchoolAndCreatedDate
{

    public class GetAllCircularByFilterSchoolAndCreatedDateQueryHandler : IRequestHandler<GetAllCircularByFilterSchoolAndCreatedDateQuery, Response<IEnumerable<GetAllCircularByFilterSchoolAndCreatedDateDto>>>
    {
        private readonly ICircularRepository _circularRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetAllCircularByFilterSchoolAndCreatedDateQueryHandler(IMapper mapper, ICircularRepository circularRepository, ILogger
            <GetAllCircularByFilterSchoolAndCreatedDateQueryHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _circularRepository = circularRepository;
        }

        public async Task<Response<IEnumerable<GetAllCircularByFilterSchoolAndCreatedDateDto>>> Handle(GetAllCircularByFilterSchoolAndCreatedDateQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allCircular = (await _circularRepository.ListAllAsync()).Where<Circular>(x => x.SchoolId == request.SchoolId);


            if (request.CircularTitle != null)
            {
                allCircular = allCircular.Where<Circular>(x => (x.CircularTitle.ToUpper().Contains(request.CircularTitle.ToUpper()))).ToList();
            }

            if (request.Description != null)
            {
                allCircular = allCircular.Where<Circular>(x => (x.Description.ToUpper().Contains(request.Description.ToUpper()))).ToList();
            }
            if (request.CreatedDate.ToShortDateString() != "01-01-0001")
            {
                allCircular = allCircular.Where<Circular>(x => x.CreatedDate?.ToShortDateString() == request.CreatedDate.ToShortDateString()).ToList();
            }
            List<GetAllCircularByFilterSchoolAndCreatedDateDto> responseData = new List<GetAllCircularByFilterSchoolAndCreatedDateDto>();

            foreach (var circular in allCircular)
            {

                responseData.Add(new GetAllCircularByFilterSchoolAndCreatedDateDto()
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
            return new Response<IEnumerable<GetAllCircularByFilterSchoolAndCreatedDateDto>>(responseData, "success");
        }

    }
}
