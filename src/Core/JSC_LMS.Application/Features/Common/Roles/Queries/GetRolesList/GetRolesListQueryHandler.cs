using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Models.Authentication;
using JSC_LMS.Application.Response;
using JSC_LMS.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Common.Roles.Queries.GetRolesList
{
    public class GetRolesListQueryHandler : IRequestHandler<GetRolesListQuery, Response<IEnumerable<RolesResponse>>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetRolesListQueryHandler(IMapper mapper, IRoleRepository roleRepository, ILogger<GetRolesListQueryHandler> logger, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public async Task<Response<IEnumerable<RolesResponse>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allRoles = await _authenticationService.GetAllRoles();
            var role = _mapper.Map<IEnumerable<RolesResponse>>(allRoles);
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<RolesResponse>>(role, "success");

        }
    }
}
