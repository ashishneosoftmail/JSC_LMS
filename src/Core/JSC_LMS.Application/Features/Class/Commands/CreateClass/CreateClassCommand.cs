using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Class.Commands.CreateClass
{
  public  class CreateClassCommand: IRequest<Response<CreateClassDto>>
    {
        public CreateClassCommand(CreateClassDto _createClassDto)
        {
            createClassDto = _createClassDto;
        }
        public CreateClassDto createClassDto { get; set; }

    }
}
