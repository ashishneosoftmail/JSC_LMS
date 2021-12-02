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

namespace JSC_LMS.Application.Features.Gallary.Queries.GetGallaryById
{


        public class GetGallaryByIdQueryHandler : IRequestHandler<GetGallaryByIdQuery, Response<GetGallaryByIdDto>>
        {
            private readonly IGallaryRepository _gallaryRepository;
            private readonly IMapper _mapper;
            private readonly ILogger _logger;
            public GetGallaryByIdQueryHandler(IMapper mapper, IGallaryRepository gallaryRepository, ILogger<GetGallaryByIdQueryHandler> logger)
            {
                _mapper = mapper;
            _gallaryRepository = gallaryRepository;
                _logger = logger;
            }

            public async Task<Response<GetGallaryByIdDto>> Handle(GetGallaryByIdQuery request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("Handle Initiated");
                Response<GetGallaryByIdDto> responseData = new Response<GetGallaryByIdDto>();
                var gallary = await _gallaryRepository.GetByIdAsync(request.Id);
                if (gallary == null)
                {
                    responseData.Succeeded = true;
                    responseData.Message = "Data Doesn't Exist";
                    responseData.Data = null;
                    return responseData;
                }
                var gallaryData = new GetGallaryByIdDto()
                {
                    Id = gallary.Id,
                    FileName = gallary.FileName,
                    FileType = gallary.FileType,
                    EventsTableId = gallary.EventsTableId,
                    IsActive = gallary.IsActive,
                    SchoolId = gallary.SchoolId,
                    image = gallary.image,
                    SchoolData = new SchoolDto() { Id = gallary.School.Id, SchoolName = gallary.School.SchoolName },
                    EventsData=new EventsTableDto() { Id = gallary.EventsTable.Id, EventTitle = gallary.EventsTable.EventTitle }
                };
                _logger.LogInformation("Hanlde Completed");
                return new Response<GetGallaryByIdDto>(gallaryData, "success");
            }

        }
    }

