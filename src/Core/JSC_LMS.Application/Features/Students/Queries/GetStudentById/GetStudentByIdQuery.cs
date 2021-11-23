using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Students.Queries.GetStudentById
{
    public class GetStudentByIdQuery : IRequest<Response<GetStudentByIdDto>>
    {
        public int Id { get; set; }
    }
}
