﻿using JSC_LMS.Application.Features.Feedback.Queries.GetFeedbackById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetFeedbackByIdResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public GetFeedbackByIdDto data { get; set; }
    }
}
