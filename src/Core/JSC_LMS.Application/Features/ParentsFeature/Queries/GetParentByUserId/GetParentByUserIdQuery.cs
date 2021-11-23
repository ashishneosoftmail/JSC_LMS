
using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.ParentsFeature.Queries.GetParentByUserId
{
    public class GetParentByUserIdQuery : IRequest<Response<GetParentByUserIdDto>>
    {
        public string UserId { get; set; }
    }
}
