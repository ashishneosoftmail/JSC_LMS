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

namespace JSC_LMS.Application.Features.KnowledgeBase.Queries.GetKnowledgeBaseByPagination
{

    public class GetKnowledgeBaseByPaginationQueryHandler : IRequestHandler<GetKnowledgeBaseByPaginationQuery, Response<IEnumerable<GetKnowledgeBaseByPaginationDto>>>
    {
        private readonly IKnowledgeBaseRepository _knowledgebaseRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetKnowledgeBaseByPaginationQueryHandler(IMapper mapper, IKnowledgeBaseRepository knowledgebaseRepository, ILogger
            <GetKnowledgeBaseByPaginationQueryHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _knowledgebaseRepository = knowledgebaseRepository;
        }

        public async Task<Response<IEnumerable<GetKnowledgeBaseByPaginationDto>>> Handle(GetKnowledgeBaseByPaginationQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allKnowledgeBase = await _knowledgebaseRepository.GetPagedReponseAsync(request.page, request.size);
            List<GetKnowledgeBaseByPaginationDto> knowledgeBaseList = new List<GetKnowledgeBaseByPaginationDto>();
            foreach (var knowledgebase in allKnowledgeBase)
            {
                knowledgeBaseList.Add(new GetKnowledgeBaseByPaginationDto()
                {
                    Id = knowledgebase.Id,
                    CategoryId = knowledgebase.CategoryId,
                    Category = _mapper.Map<CategoriesDto>(knowledgebase.Category),
                    DocTitle = knowledgebase.DocTitle,
                    AddContent = knowledgebase.AddContent,
                    IsActive = knowledgebase.IsActive,
                    SlugUrl = knowledgebase.SlugUrl,
                    SubTitle = knowledgebase.SubTitle
                });
            }

            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetKnowledgeBaseByPaginationDto>>(knowledgeBaseList, "success");
        }

    }
}
