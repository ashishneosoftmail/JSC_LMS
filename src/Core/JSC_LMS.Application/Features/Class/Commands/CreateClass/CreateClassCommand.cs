using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Class.Commands.CreateClass
{
  public  class CreateClassCommand: IRequest<Response<CreateClassDto>>
    {
        public CreateClassDto createClassDto { get; set; }

    }
}
