using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Teachers.Commands.CreateTeacher
{
    public class CreateTeacherCommand : IRequest<Response<CreateTeacherDto>>
    {
        public CreateTeacherDto createTeacherDto { get; set; }
    }
}
