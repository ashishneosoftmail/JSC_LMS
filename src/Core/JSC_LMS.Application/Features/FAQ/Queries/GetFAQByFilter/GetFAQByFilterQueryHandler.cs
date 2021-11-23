using AutoMapper;
using JSC_LMS.Application.CommonDtos;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.FAQ.Queries.GetFAQByFilter
{
   public class GetFAQByFilterQueryHandler : IRequestHandler<GetFAQByFilterQuery, Response<IEnumerable<GetFAQByFilterDto>>>
    {
        private readonly IFAQRepository _faqRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetFAQByFilterQueryHandler(IMapper mapper, IFAQRepository faqRepository, ILogger<GetFAQByFilterQueryHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _faqRepository = faqRepository;
        }

        public async Task<Response<IEnumerable<GetFAQByFilterDto>>> Handle(GetFAQByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allKnowledgeBase = await _faqRepository.ListAllAsync();


            if (request.FAQTitle != null)
            {
                allKnowledgeBase = allKnowledgeBase.Where<JSC_LMS.Domain.Entities.FAQ>(x => (x.FAQTitle == request.FAQTitle)).ToList();
            }

            if (request.IsActive)
            {
                allKnowledgeBase = allKnowledgeBase.Where<JSC_LMS.Domain.Entities.FAQ>(x => x.IsActive == request.IsActive).ToList();
            }
            if (request.Category != "Select Category")
            {
                allKnowledgeBase = allKnowledgeBase.Where<JSC_LMS.Domain.Entities.FAQ>(x => x.Category.CategoryName == request.Category).ToList();

            }
            List<GetFAQByFilterDto> responseData = new List<GetFAQByFilterDto>();

            foreach (var knowledegeBase in allKnowledgeBase)
            {

                responseData.Add(new GetFAQByFilterDto()
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
            return new Response<IEnumerable<GetFAQByFilterDto>>(responseData, "success");
        }


    }
}
