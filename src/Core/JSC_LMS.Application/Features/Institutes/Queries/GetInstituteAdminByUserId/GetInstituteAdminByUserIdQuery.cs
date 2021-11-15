using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Institutes.Queries.GetInstituteAdminByUserId
{

    public class GetInstituteAdminByUserIdQuery : IRequest<Response<GetInstituteAdminByUserIdDto>>
    {
        public string UserId { get; set; }
    }
}
