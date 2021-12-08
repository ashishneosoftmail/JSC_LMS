using JSC_LMS.Application.Models.Authentication;
using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Common.Users.Queries.GetUsersList
{
    public class GetUsersListQuery :IRequest<Response<IEnumerable<User>>>
    {
    }
}
