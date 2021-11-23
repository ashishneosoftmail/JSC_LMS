using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.FAQ.Commands.UpdateFAQ
{
    public class UpdateFAQCommand : IRequest<Response<int>>
    {
        public UpdateFAQDto _updateFAQDto { get; set; }
        public UpdateFAQCommand(UpdateFAQDto updateFAQDto)
        {
            _updateFAQDto = updateFAQDto;
        }
    }
}
