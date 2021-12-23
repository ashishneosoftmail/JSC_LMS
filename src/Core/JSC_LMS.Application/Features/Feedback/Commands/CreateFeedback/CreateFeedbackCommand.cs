using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Feedback.Commands.CreateFeedback
{
    public class CreateFeedbackCommand : IRequest<Response<CreateFeedbackDto>>
    {


        public CreateFeedbackCommand(CreateFeedbackDto _createFeedbackDto)
        {
            createFeedbackDto = _createFeedbackDto;
        }
        public CreateFeedbackDto createFeedbackDto { get; set; }

        
    }
}
