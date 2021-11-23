using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Teachers.Queries.GetTeacherByUserId
{
    public class GetTeacherByUserIdQuery : IRequest<Response<GetTeacherByUserIdDto>>
    {

        public string UserId { get; set; }
    }
}
