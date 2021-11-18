using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Models.Authentication
{
    public class UpdateResetPasswordRequest
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
