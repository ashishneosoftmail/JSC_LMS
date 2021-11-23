using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.FAQ.Commands.CreateFAQ
{
   public class CreateFAQCommandHandler : IRequestHandler<CreateFAQCommand, Response<CreateFAQDto>>
    {
        private readonly IFAQRepository _faqRepository;
        private readonly IMapper _mapper;


        public CreateFAQCommandHandler(IMapper mapper, IFAQRepository faqRepository)
        {
            _mapper = mapper;
            _faqRepository = faqRepository;
        }

        public async Task<Response<CreateFAQDto>> Handle(CreateFAQCommand request, CancellationToken cancellationToken)
        {
            var createKnowledgeCommandResponse = new Response<CreateFAQDto>();

            var knowledge = await _faqRepository.AddAsync(_mapper.Map<JSC_LMS.Domain.Entities.FAQ>(request._createKnowledgeBaseDto));
            createKnowledgeCommandResponse.Data = _mapper.Map<CreateFAQDto>(knowledge);
            createKnowledgeCommandResponse.Succeeded = true;
            createKnowledgeCommandResponse.Message = "success";
            return createKnowledgeCommandResponse;
        }
    }
}
