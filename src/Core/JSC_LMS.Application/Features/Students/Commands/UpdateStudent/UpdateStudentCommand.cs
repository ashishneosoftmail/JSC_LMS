using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Students.Commands.UpdateStudent
{
    public class UpdateStudentCommand : IRequest<Response<int>>
    {
        public UpdateStudentCommand(UpdateStudentDto _updateStudentDto)
        {
            updateStudentDto = _updateStudentDto;
        }
        public UpdateStudentDto updateStudentDto { get; set; }
    }
}
