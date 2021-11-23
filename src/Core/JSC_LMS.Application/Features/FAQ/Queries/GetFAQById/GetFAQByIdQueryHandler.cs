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

namespace JSC_LMS.Application.Features.FAQ.Queries.GetFAQById
{
   public class GetFAQByIdQueryHandler : IRequestHandler<GetFAQByIdQuery, Response<GetFAQByIdDto>>
    {
        private readonly IFAQRepository _faqRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetFAQByIdQueryHandler(IMapper mapper, IFAQRepository faqRepository, ILogger<GetFAQByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _faqRepository = faqRepository;
            _logger = logger;
        }

        public async Task<Response<GetFAQByIdDto>> Handle(GetFAQByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            Response<GetFAQByIdDto> responseData = new Response<GetFAQByIdDto>();
            var knowledgeBase = await _faqRepository.GetByIdAsync(request.Id);
            if (knowledgeBase == null)
            {
                responseData.Succeeded = true;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }
            var knowledgeBaseData = new GetFAQByIdDto()
            {
                Id = knowledgeBase.Id,
                CategoryId = knowledgeBase.CategoryId,
                Category = new CategoriesDto() { Id = knowledgeBase.Category.Id, CategoryName = knowledgeBase.Category.CategoryName },
                FAQTitle = knowledgeBase.FAQTitle,
                Content = knowledgeBase.Content,
                IsActive = knowledgeBase.IsActive,
            
                Question = knowledgeBase.Question
            };
            _logger.LogInformation("Hanlde Completed");
            return new Response<GetFAQByIdDto>(knowledgeBaseData, "success");
        }
    }
}
