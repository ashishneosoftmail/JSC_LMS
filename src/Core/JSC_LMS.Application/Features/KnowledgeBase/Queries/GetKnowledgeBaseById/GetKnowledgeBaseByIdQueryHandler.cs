using AutoMapper;
using JSC_LMS.Application.CommonDtos;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.KnowledgeBase.Queries.GetKnowledgeBaseById
{
    public class GetKnowledgeBaseByIdQueryHandler : IRequestHandler<GetKnowledgeBaseByIdQuery, Response<GetKnowledgeBaseByIdDto>>
    {
        private readonly IKnowledgeBaseRepository _knowledgebaseRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetKnowledgeBaseByIdQueryHandler(IMapper mapper, IKnowledgeBaseRepository knowledgebaseRepository, ILogger<GetKnowledgeBaseByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _knowledgebaseRepository = knowledgebaseRepository;
            _logger = logger;
        }

        public async Task<Response<GetKnowledgeBaseByIdDto>> Handle(GetKnowledgeBaseByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            Response<GetKnowledgeBaseByIdDto> responseData = new Response<GetKnowledgeBaseByIdDto>();
            var knowledgeBase = await _knowledgebaseRepository.GetByIdAsync(request.Id);
            if (knowledgeBase == null)
            {
                responseData.Succeeded = true;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }
            var knowledgeBaseData = new GetKnowledgeBaseByIdDto()
            {
                Id = knowledgeBase.Id,
                CategoryId = knowledgeBase.CategoryId,
                Category =new CategoriesDto() { Id=knowledgeBase.Category.Id,CategoryName=knowledgeBase.Category.CategoryName},
                DocTitle = knowledgeBase.DocTitle,
                AddContent = knowledgeBase.AddContent,
                IsActive = knowledgeBase.IsActive,
                SlugUrl = knowledgeBase.SlugUrl,
                SubTitle = knowledgeBase.SubTitle
            };
            _logger.LogInformation("Hanlde Completed");
            return new Response<GetKnowledgeBaseByIdDto>(knowledgeBaseData, "success");
        }

    }
}
