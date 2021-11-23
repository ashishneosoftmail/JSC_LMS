using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.FAQ.Commands.UpdateFAQ
{
    public class UpdateFAQCommandHandler : IRequestHandler<UpdateFAQCommand, Response<int>>
    {

        private readonly IFAQRepository _faqRepository;
        private readonly IMapper _mapper;
        public UpdateFAQCommandHandler(IMapper mapper, IFAQRepository faqRepository)
        {
            _mapper = mapper;
            _faqRepository = faqRepository;
        }
        public async Task<Response<int>> Handle(UpdateFAQCommand request, CancellationToken cancellationToken)
        {
            var knowledgebaseToUpdate = await _faqRepository.GetByIdAsync(request._updateFAQDto.Id);
            Response<int> UpdateKnowledgeBaseCommandResponse = new Response<int>();
            if (knowledgebaseToUpdate == null)
            {
                UpdateKnowledgeBaseCommandResponse.Message = "No Data Found";
                return UpdateKnowledgeBaseCommandResponse;
            }
            knowledgebaseToUpdate.Content = request._updateFAQDto.Content;
            knowledgebaseToUpdate.Question = request._updateFAQDto.Question;
          
            knowledgebaseToUpdate.FAQTitle = request._updateFAQDto.FAQTitle;
            knowledgebaseToUpdate.CategoryId = request._updateFAQDto.CategoryId;
            await _faqRepository.UpdateAsync(knowledgebaseToUpdate);

            return new Response<int>(request._updateFAQDto.Id, "Updated successfully ");
        }
    }
}
