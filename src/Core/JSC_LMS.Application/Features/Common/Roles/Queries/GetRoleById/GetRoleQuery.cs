using JSC_LMS.Application.Response;
using MediatR;
using System.Collections.Generic;

namespace JSC_LMS.Application.Features.Common.Roles.Queries.GetRoleById
{
    public class GetRoleQuery : IRequest<Response<RoleListVm>>
    {
        public int Id { get; set; }
    }
}
