using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Students.Queries.GetStudentByUserId
{
    public class GetStudentByUserIdQuery : IRequest<Response<GetStudentByUserIdDto>>
    {
        public string UserId { get; set; }
    }

    }

