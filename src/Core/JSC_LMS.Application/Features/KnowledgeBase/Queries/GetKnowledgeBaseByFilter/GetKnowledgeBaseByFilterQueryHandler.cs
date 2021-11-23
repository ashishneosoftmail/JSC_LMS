using AutoMapper;
using JSC_LMS.Application.CommonDtos;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.KnowledgeBase.Queries.GetKnowledgeBaseByFilter
{

    public class GetKnowledgeBaseByFilterQueryHandler : IRequestHandler<GetKnowledgeBaseByFilterQuery, Response<IEnumerable<GetKnowledgeBaseByFilterDto>>>
    {
        private readonly IKnowledgeBaseRepository _knowledgebaseRepository;

        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetKnowledgeBaseByFilterQueryHandler(IMapper mapper, IKnowledgeBaseRepository knowledgebaseRepository, ILogger<GetKnowledgeBaseByFilterQueryHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _knowledgebaseRepository = knowledgebaseRepository;
        }


        public async Task<Response<IEnumerable<GetKnowledgeBaseByFilterDto>>> Handle(GetKnowledgeBaseByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allKnowledgeBase = await _knowledgebaseRepository.ListAllAsync();


            if (request.DocTitle != null)
            {
                allKnowledgeBase = allKnowledgeBase.Where<JSC_LMS.Domain.Entities.KnowledgeBase>(x => (x.DocTitle == request.DocTitle)).ToList();
            }

            if (request.Subtitle != null)
            {
                allKnowledgeBase = allKnowledgeBase.Where<JSC_LMS.Domain.Entities.KnowledgeBase>(x => x.SubTitle == request.Subtitle).ToList();
            }
            if (request.Category != "Select Category")
            {
                allKnowledgeBase = allKnowledgeBase.Where<JSC_LMS.Domain.Entities.KnowledgeBase>(x => x.Category.CategoryName == request.Category).ToList();

            }
            List<GetKnowledgeBaseByFilterDto> responseData = new List<GetKnowledgeBaseByFilterDto>();

            foreach (var knowledegeBase in allKnowledgeBase)
            {

                responseData.Add(new GetKnowledgeBaseByFilterDto()
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
            return new Response<IEnumerable<GetKnowledgeBaseByFilterDto>>(responseData, "success");
        }


    }
}
