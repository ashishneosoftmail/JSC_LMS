using MediatR;
using System;

namespace JSC_LMS.Application.Features.Common.Roles.Commands.DeleteRole
{
    public class DeleteRoleCommand : IRequest
    {
        public int Id { get; set; }
    }
}
