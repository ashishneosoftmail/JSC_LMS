using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Subject.Commands.CreateSubject
{
   public class CreateSubjectCommand : IRequest<Response<CreateSubjectDto>>
    {
        public CreateSubjectCommand(CreateSubjectDto _createSubjectDto)
        {
            createSubjectDto = _createSubjectDto;
        }
        public CreateSubjectDto createSubjectDto { get; set; }

    }
}
