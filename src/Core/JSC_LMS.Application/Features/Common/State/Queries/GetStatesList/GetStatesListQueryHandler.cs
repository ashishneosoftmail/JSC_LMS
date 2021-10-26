using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Responses;
using JSC_LMS.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Common.States.Queries.GetStatesList
{
    public class GetStatesListQueryHandler : IRequestHandler<GetStatesListQuery, Response<IEnumerable<State>>>
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetStatesListQueryHandler(IMapper mapper, IStateRepository stateRepository, ILogger<GetStatesListQueryHandler> logger)
        {
            _mapper = mapper;
            _stateRepository = stateRepository;
            _logger = logger;
        }

        public async Task<Response<IEnumerable<State>>> Handle(GetStatesListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allState = await _stateRepository.ListAllAsync();
            var states = _mapper.Map<IEnumerable<State>>(allState);
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<State>>(states, "success");

        }
    }
}
