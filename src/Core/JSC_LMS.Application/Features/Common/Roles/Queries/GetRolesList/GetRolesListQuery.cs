using JSC_LMS.Application.Models.Authentication;
using JSC_LMS.Application.Response;
using JSC_LMS.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace JSC_LMS.Application.Features.Common.Roles.Queries.GetRolesList
{
    public class GetRolesListQuery : IRequest<Response<IEnumerable<RolesResponse>>>
    {

    }
}
