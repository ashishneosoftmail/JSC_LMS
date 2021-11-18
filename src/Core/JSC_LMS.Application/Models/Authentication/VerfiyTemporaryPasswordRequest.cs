using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Models.Authentication
{
    public class VerfiyTemporaryPasswordRequest
    {
        public string Email { get; set; }
        [Required(ErrorMessage="Please Enter Password")]
        public string TemporaryPassword { get; set; }
    }
}
