using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Models.Mail
{
    public class SMTPEmailSettings
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
    }
}
