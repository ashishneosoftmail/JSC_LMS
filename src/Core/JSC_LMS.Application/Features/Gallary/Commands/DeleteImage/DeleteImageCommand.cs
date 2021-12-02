using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Gallary.Commands.DeleteImage
{
  public  class DeleteImageCommand : IRequest
    {
        public int Id { get; set; }
    }
}
