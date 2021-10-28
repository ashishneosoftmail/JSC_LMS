using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Class.Commands.UpdateClass
{
    public class UpdateClassCommand : IRequest<Response<int>>
    {
        public UpdateClassDto updateClassDto { get; set; }
    }
}
