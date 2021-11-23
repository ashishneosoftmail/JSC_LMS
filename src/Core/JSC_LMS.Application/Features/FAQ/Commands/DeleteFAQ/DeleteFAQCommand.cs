using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.FAQ.Commands.DeleteFAQ
{
   public class DeleteFAQCommand : IRequest
    {
        public int Id { get; set; }
    }
}
