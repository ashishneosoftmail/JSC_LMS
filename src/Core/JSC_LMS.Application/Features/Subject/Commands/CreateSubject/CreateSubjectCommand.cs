using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Subject.Commands.CreateSubject
{
   public class CreateSubjectCommand : IRequest<Response<CreateSubjectDto>>
    {
        public CreateSubjectDto createSubjectDto { get; set; }

    }
}
