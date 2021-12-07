
using JSC_LMS.Application.Models.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class ForgotPasswordChangePasswordModel : UpdateResetPasswordRequest
    {
        [Required(ErrorMessage = "Confirm Password is mandatory")]
        [DataType(DataType.Password)]
       
        [Compare("NewPassword", ErrorMessage =
         "Password and Confirm password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
