using JSC_LMS.Application.Features.Feedback.Commands.CreateFeedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class FeedbackResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public CreateFeedbackDto data { get; set; }
    }
}
