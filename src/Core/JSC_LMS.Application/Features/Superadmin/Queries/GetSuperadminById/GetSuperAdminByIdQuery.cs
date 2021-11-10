using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Superadmin.Queries.GetSuperadminById
{
    public class GetSuperAdminByIdQuery : IRequest<Response<GetSuperadminByIdDto>>
    {
        public string Id { get; set; }
    }
}
