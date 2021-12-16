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

namespace JSC_LMS.Application.Features.Gallary.Queries.GetAllGallaryList
{
   public class GetAllGallaryQueryHandler
    {
        public class GetAllCircularQueryHandler : IRequestHandler<GetAllGallaryQuery, Response<IEnumerable<GetAllGallaryListDto>>>
        {
            private readonly IGallaryRepository _gallaryRepository;
            private readonly IMapper _mapper;
            private readonly ILogger _logger;
            public GetAllCircularQueryHandler(IMapper mapper, IGallaryRepository gallaryRepository, ILogger<GetAllGallaryQueryHandler> logger)
            {
                _mapper = mapper;
                _logger = logger;
                _gallaryRepository = gallaryRepository;
            }

            public async Task<Response<IEnumerable<GetAllGallaryListDto>>> Handle(GetAllGallaryQuery request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("Handle Initiated");

                var allGallaryList = await _gallaryRepository.ListAllAsync();

                _logger.LogInformation("Hanlde Completed");

                List<GetAllGallaryListDto> gallaryList = new List<GetAllGallaryListDto>();

                foreach (var gallary in allGallaryList)
                {
                    gallaryList.Add(new GetAllGallaryListDto()
                    {
                        Id = gallary.Id,
                        FileName = gallary.FileName,
                        FileType = gallary.FileType,
                        image = gallary.image,
                        EventsTableId = gallary.EventsTableId,
                        
                        IsActive = gallary.IsActive,
                        SchoolId = gallary.SchoolId,
                        
                        SchoolData = new SchoolDto() 
                        { 
                            Id = gallary.School.Id, SchoolName = gallary.School.SchoolName },
                        
                        EventsData=new EventsTableDto()
                        {
                            Id = gallary.EventsTable.Id,
                            EventTitle = gallary.EventsTable.EventTitle

                        }
                    });
                }
                return new Response<IEnumerable<GetAllGallaryListDto>>(gallaryList, "success");
            }
        }

    }
}
