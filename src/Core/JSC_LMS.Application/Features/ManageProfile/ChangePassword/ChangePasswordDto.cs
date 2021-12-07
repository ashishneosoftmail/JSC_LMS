using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.ManageProfile.ChangePassword
{
   public class ChangePasswordDto
    {
        public string UserId { get; set; }
        [Required(ErrorMessage = "Current Password is mandatory")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New Password is mandatory")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
