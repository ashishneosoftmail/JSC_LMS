using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.School.Commands.UpdateSchool
{
    public class UpdateSchoolCommand : IRequest<Response<int>>
    {
        public UpdateSchoolCommand(UpdateSchoolDto _updateSchoolDto)
        {
            updateSchoolDto = _updateSchoolDto;
        }
        public UpdateSchoolDto updateSchoolDto { get; set; }
    }
}
