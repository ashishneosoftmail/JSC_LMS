using JSC_LMS.Application.Features.FAQ.Commands.CreateFAQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class AddFAQBaseResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public CreateFAQDto data { get; set; }
    }
}
