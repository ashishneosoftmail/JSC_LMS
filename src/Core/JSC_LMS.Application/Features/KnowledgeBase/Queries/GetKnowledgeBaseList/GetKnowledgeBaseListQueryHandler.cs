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

namespace JSC_LMS.Application.Features.KnowledgeBase.Queries.GetKnowledgeBaseList
{

    public class GetKnowledgeBaseListQueryHandler : IRequestHandler<GetKnowledgeBaseListQuery, Response<IEnumerable<GetKnowledgeBaseListDto>>>
    {
        private readonly IKnowledgeBaseRepository _knowledgebaseRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetKnowledgeBaseListQueryHandler(IMapper mapper, IKnowledgeBaseRepository knowledgebaseRepository, ILogger<GetKnowledgeBaseListQueryHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _knowledgebaseRepository = knowledgebaseRepository;
        }

        public async Task<Response<IEnumerable<GetKnowledgeBaseListDto>>> Handle(GetKnowledgeBaseListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allKnowledgeBase = await _knowledgebaseRepository.ListAllAsync();
            List<GetKnowledgeBaseListDto> knowledegeBaseList = new List<GetKnowledgeBaseListDto>();
            foreach (var knowledegeBase in allKnowledgeBase)
            {
                knowledegeBaseList.Add(new GetKnowledgeBaseListDto()
                {
                    Id = knowledegeBase.Id,
                    CategoryId = knowledegeBase.CategoryId,
                    Category = _mapper.Map<CategoriesDto>(knowledegeBase.Category),
                    DocTitle = knowledegeBase.DocTitle,
                    AddContent = knowledegeBase.AddContent,
                    IsActive = knowledegeBase.IsActive,
                    SlugUrl = knowledegeBase.SlugUrl,
                    SubTitle = knowledegeBase.SubTitle
                });
            }
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetKnowledgeBaseListDto>>(knowledegeBaseList, "success");
        }
    }
}
