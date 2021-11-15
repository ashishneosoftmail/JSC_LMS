using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.KnowledgeBase.Commands.CreateKnowledgeBase
{

    public class CreateKnowledgeBaseCommandHandler : IRequestHandler<CreateKnowledgeBaseCommand, Response<CreateKnowledgeBaseDto>>
    {
        private readonly IKnowledgeBaseRepository _knowledgebaseRepository;
        private readonly IMapper _mapper;


        public CreateKnowledgeBaseCommandHandler(IMapper mapper, IKnowledgeBaseRepository knowledgebaseRepository)
        {
            _mapper = mapper;
            _knowledgebaseRepository = knowledgebaseRepository;
        }

        public async Task<Response<CreateKnowledgeBaseDto>> Handle(CreateKnowledgeBaseCommand request, CancellationToken cancellationToken)
        {
            var createKnowledgeCommandResponse = new Response<CreateKnowledgeBaseDto>();

            var knowledge = await _knowledgebaseRepository.AddAsync(_mapper.Map<JSC_LMS.Domain.Entities.KnowledgeBase>(request._createKnowledgeBaseDto));
            createKnowledgeCommandResponse.Data = _mapper.Map<CreateKnowledgeBaseDto>(knowledge);
            createKnowledgeCommandResponse.Succeeded = true;
            createKnowledgeCommandResponse.Message = "success";
            return createKnowledgeCommandResponse;
        }

    }
}
