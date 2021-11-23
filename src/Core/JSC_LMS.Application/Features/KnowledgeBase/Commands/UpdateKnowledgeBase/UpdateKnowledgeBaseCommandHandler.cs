using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.KnowledgeBase.Commands.UpdateKnowledgeBase
{

    public class UpdateKnowledgeBaseCommandHandler : IRequestHandler<UpdateKnowledgeBaseCommand, Response<int>>
    {
        private readonly IKnowledgeBaseRepository _knowledgebaseRepository;
        private readonly IMapper _mapper;
        public UpdateKnowledgeBaseCommandHandler(IMapper mapper, IKnowledgeBaseRepository knowledgebaseRepository)
        {
            _mapper = mapper;
            _knowledgebaseRepository = knowledgebaseRepository;
        }

        public async Task<Response<int>> Handle(UpdateKnowledgeBaseCommand request, CancellationToken cancellationToken)
        {
            var knowledgebaseToUpdate = await _knowledgebaseRepository.GetByIdAsync(request._updateKnowledgeBaseDto.Id);
            Response<int> UpdateKnowledgeBaseCommandResponse = new Response<int>();
            if (knowledgebaseToUpdate == null)
            {
                UpdateKnowledgeBaseCommandResponse.Message = "No Data Found";
                return UpdateKnowledgeBaseCommandResponse;
            }
            knowledgebaseToUpdate.AddContent = request._updateKnowledgeBaseDto.AddContent;
            knowledgebaseToUpdate.SlugUrl = request._updateKnowledgeBaseDto.SlugUrl;
            knowledgebaseToUpdate.SubTitle = request._updateKnowledgeBaseDto.SubTitle;
            knowledgebaseToUpdate.DocTitle = request._updateKnowledgeBaseDto.DocTitle;
            knowledgebaseToUpdate.CategoryId = request._updateKnowledgeBaseDto.CategoryId;
            await _knowledgebaseRepository.UpdateAsync(knowledgebaseToUpdate);

            return new Response<int>(request._updateKnowledgeBaseDto.Id, "Updated successfully ");
        }
    }
}
