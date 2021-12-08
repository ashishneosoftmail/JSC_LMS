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
namespace JSC_LMS.Application.Features.Common.Users.Queries.GetUsersList
{
    public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, Response<IEnumerable<User>>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetUsersListQueryHandler(IMapper mapper,ILogger<GetUsersListQueryHandler> logger, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
           
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public async Task<Response<IEnumerable<User>>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allRoles = await _authenticationService.GetAllUsers();
            var role = _mapper.Map<IEnumerable<User>>(allRoles);
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<User>>(role, "success");

        }
    }
}
