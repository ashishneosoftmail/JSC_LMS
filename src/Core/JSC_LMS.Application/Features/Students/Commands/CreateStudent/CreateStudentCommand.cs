using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Students.Commands.CreateStudent
{
    public class CreateStudentCommand : IRequest<Response<CreateStudentDto>>
    {
        public CreateStudentCommand(CreateStudentDto _createStudentDto)
        {
            createStudentDto = _createStudentDto;
        }
        public CreateStudentDto createStudentDto { get; set; }
    }
}
