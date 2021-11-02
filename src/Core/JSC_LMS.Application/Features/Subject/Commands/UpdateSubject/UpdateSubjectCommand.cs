﻿using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Subject.Commands.UpdateSubject
{
   public class UpdateSubjectCommand : IRequest<Response<int>>
    {
        public UpdateSubjectDto updateSubjectDto { get; set; }
    }
}
