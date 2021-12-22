using JSC_LMS.Application.Models.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAllFeedbackResponseModel
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public string token { get; set; }
        public string statusCode { get; set; }
        public IEnumerable<FeedbackResponse> data { get; set; }
    }
}
