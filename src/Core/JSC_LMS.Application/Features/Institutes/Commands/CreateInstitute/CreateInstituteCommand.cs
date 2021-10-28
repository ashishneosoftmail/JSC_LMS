﻿using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Institutes.Commands.CreateInstitute
{
    public class CreateInstituteCommand : IRequest<Response<CreateInstituteDto>>
    {
        public CreateInstituteDto createInstituteDto { get; set; }
    }
}
