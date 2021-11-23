using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.School.Commands.CreateSchool
{
    public class CreateSchoolCommand : IRequest<Response<CreateSchoolDto>>
    {
        public CreateSchoolDto createSchoolDto { get; set; }
    }
}
