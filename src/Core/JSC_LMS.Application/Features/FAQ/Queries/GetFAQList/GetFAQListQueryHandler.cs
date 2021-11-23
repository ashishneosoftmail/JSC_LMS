using AutoMapper;
using JSC_LMS.Application.CommonDtos;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.FAQ.Queries.GetFAQList
{
   public class GetFAQListQueryHandler : IRequestHandler<GetFAQListQuery, Response<IEnumerable<GetFAQListDto>>>
    {
        private readonly IFAQRepository _faqRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetFAQListQueryHandler(IMapper mapper, IFAQRepository faqRepository, ILogger<GetFAQListQueryHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _faqRepository = faqRepository;
        }

        public async Task<Response<IEnumerable<GetFAQListDto>>> Handle(GetFAQListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allKnowledgeBase = await _faqRepository.ListAllAsync();
            List<GetFAQListDto> knowledegeBaseList = new List<GetFAQListDto>();
            foreach (var knowledegeBase in allKnowledgeBase)
            {
                knowledegeBaseList.Add(new GetFAQListDto()
                {
                    Id = knowledegeBase.Id,
                    CategoryId = knowledegeBase.CategoryId,
                    Category = _mapper.Map<CategoriesDto>(knowledegeBase.Category),
                    FAQTitle = knowledegeBase.FAQTitle,
                    Content = knowledegeBase.Content,
                    IsActive = knowledegeBase.IsActive,
                    Question = knowledegeBase.Question,
                  
                });
            }
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetFAQListDto>>(knowledegeBaseList, "success");
        }

    }
}
