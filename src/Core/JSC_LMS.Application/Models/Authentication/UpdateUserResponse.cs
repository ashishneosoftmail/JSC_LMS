using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Models.Authentication
{
    public class UpdateUserResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public string UserId { get; set; }
    }
}
