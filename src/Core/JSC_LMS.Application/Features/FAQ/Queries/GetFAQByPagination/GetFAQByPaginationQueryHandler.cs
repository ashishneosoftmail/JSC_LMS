using AutoMapper;
using JSC_LMS.Application.CommonDtos;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.FAQ.Queries.GetFAQByPagination
{
   public class GetFAQByPaginationQueryHandler : IRequestHandler<GetFAQByPaginationQuery, Response<IEnumerable<GetFAQByPaginationDto>>>
    {

        private readonly IFAQRepository _faqRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetFAQByPaginationQueryHandler(IMapper mapper, IFAQRepository faqRepository, ILogger
            <GetFAQByPaginationQueryHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _faqRepository = faqRepository;
        }

        public async Task<Response<IEnumerable<GetFAQByPaginationDto>>> Handle(GetFAQByPaginationQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allKnowledgeBase = await _faqRepository.GetPagedReponseAsync(request.page, request.size);
            List<GetFAQByPaginationDto> knowledgeBaseList = new List<GetFAQByPaginationDto>();
            foreach (var knowledgebase in allKnowledgeBase)
            {
                knowledgeBaseList.Add(new GetFAQByPaginationDto()
                {
                    Id = knowledgebase.Id,
                    CategoryId = knowledgebase.CategoryId,
                    Category = _mapper.Map<CategoriesDto>(knowledgebase.Category),
                    FAQTitle = knowledgebase.FAQTitle,
                    Content = knowledgebase.Content,
                    IsActive = knowledgebase.IsActive,
                    Question = knowledgebase.Question,
                 
                });
            }

            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetFAQByPaginationDto>>(knowledgeBaseList, "success");
        }

    }
}
