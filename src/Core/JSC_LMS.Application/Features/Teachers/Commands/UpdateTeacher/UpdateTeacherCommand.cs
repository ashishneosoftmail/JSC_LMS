using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Teachers.Commands.UpdateTeacher
{
    public class UpdateTeacherCommand : IRequest<Response<int>>
    {
        public UpdateTeacherDto updateTeacherDto { get; set; }

       
        public UpdateTeacherCommand(UpdateTeacherDto _updateTeacherDto)
        {
            updateTeacherDto = _updateTeacherDto;
        }
    }

}
