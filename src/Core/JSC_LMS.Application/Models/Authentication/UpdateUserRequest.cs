using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Models.Authentication
{
  public  class UpdateUserRequest
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
