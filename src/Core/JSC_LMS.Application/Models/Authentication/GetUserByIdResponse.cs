using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Models.Authentication
{
   public class GetUserByIdResponse
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Pasword { get; set; }
    }
}
