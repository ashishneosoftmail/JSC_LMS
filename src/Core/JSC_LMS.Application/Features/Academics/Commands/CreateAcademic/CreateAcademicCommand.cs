using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Academics.Commands.CreateAcademic
{
   public class CreateAcademicCommand : IRequest<Response<CreateAcademicDto>>
    {
        public CreateAcademicDto _createAcademicDto { get; set; }
        public CreateAcademicCommand(CreateAcademicDto createAcademicDto)
        {
            _createAcademicDto = createAcademicDto;
        }

    }
}
