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

namespace JSC_LMS.Application.Features.Common.Roles.Queries.GetRolesList
{
    public class GetRolesListQueryHandler : IRequestHandler<GetRolesListQuery, Response<IEnumerable<Role>>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetRolesListQueryHandler(IMapper mapper, IRoleRepository roleRepository, ILogger<GetRolesListQueryHandler> logger)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
            _logger = logger;
        }

        public async Task<Response<IEnumerable<Role>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allRoles = await _roleRepository.ListAllAsync();
            var role = _mapper.Map<IEnumerable<Role>>(allRoles);
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<Role>>(role, "success");

        }
    }
}
