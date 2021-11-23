using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Circulars.Commands.DeleteCircular
{
    public class DeleteCircularCommand : IRequest
    {
        public int Id { get; set; }
    }
}
