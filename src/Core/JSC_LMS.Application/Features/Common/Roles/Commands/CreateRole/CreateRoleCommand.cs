using JSC_LMS.Application.Response;
using MediatR;
using System;

namespace JSC_LMS.Application.Features.Common.Roles.Commands.CreateRole
{
    public class CreateRoleCommand : IRequest<Response<int>>
    {
        public string RoleName { get; set; }
        public bool isActive { get; set; }

    }
}
