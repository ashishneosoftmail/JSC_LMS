using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Exceptions;
using JSC_LMS.Application.Response;
using JSC_LMS.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Common.Cities.Queries.GetCitiesListWithStateId
{
    public class GetCitiesListByStateIdQueryHandler : IRequestHandler<GetCitiesListByStateIdQuery, Response<IEnumerable<City>>>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetCitiesListByStateIdQueryHandler(IMapper mapper, ICityRepository cityRepository, IStateRepository stateRepository, ILogger<GetCitiesListByStateIdQueryHandler> logger)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
            _logger = logger;
            _stateRepository = stateRepository;
        }

        public async Task<Response<IEnumerable<City>>> Handle(GetCitiesListByStateIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var checkState = await _stateRepository.GetByIdAsync(request.StateId);
            if (checkState == null)
            {
                throw new NotFoundException(nameof(State), request.StateId);
            }
            var allCitiesByStateId = (await _cityRepository.ListAllAsync()).Where<City>(e => e.StateId == request.StateId);
            //var cities = _mapper.Map<IEnumerable<City>>(allCitiesByStateId);
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<City>>(allCitiesByStateId, "success");

        }
    }
}
