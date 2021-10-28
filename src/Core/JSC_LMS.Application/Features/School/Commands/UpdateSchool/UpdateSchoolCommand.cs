using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.School.Commands.UpdateSchool
{
    public class UpdateSchoolCommand : IRequest<Response<int>>
    {
        public UpdateSchoolDto updateSchoolDto { get; set; }
    }
}
