using JSC_LMS.Application.Response;
using MediatR;
using System;

namespace JSC_LMS.Application.Features.Common.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool isActive { get; set; }
    }
}
