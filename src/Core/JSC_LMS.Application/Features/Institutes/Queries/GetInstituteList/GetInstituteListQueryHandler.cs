using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Institutes.Queries.GetInstituteList
{


    public class GetInstituteListQueryHandler : IRequestHandler<GetInstituteListQuery, Response<IEnumerable<GetInstituteListVm>>>
    {
        private readonly IInstituteRepository _instituteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetInstituteListQueryHandler(IMapper mapper, IInstituteRepository instituteRepository, ILogger<GetInstituteListQueryHandler> logger)
        {
            _mapper = mapper;
            _instituteRepository = instituteRepository;
            _logger = logger;
        }

        public async Task<Response<IEnumerable<GetInstituteListVm>>> Handle(GetInstituteListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allInstitutes = await _instituteRepository.ListAllAsync();
            var institute = _mapper.Map<List<GetInstituteListVm>>(allInstitutes);
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetInstituteListVm>>(institute, "success");
        }

    }
}