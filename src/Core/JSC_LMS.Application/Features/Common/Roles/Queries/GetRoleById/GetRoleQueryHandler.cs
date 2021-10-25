using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Responses;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Common.Roles.Queries.GetRoleById
{
    public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, Response<RoleListVm>>
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;

        public GetRoleQueryHandler(IMapper mapper, IRoleRepository roleRepository)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        public async Task<Response<RoleListVm>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            var list = await _roleRepository.GetByIdAsync(request.Id);
            var roleList = _mapper.Map<RoleListVm>(list);
            return new Response<RoleListVm>(roleList, "success");
        }


    }
}
