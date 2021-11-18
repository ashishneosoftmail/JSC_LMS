using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Models.Authentication
{
    public class UpdateResetPasswordResponse
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public string data { get; set; }
    }
}
