using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;

namespace JSC_LMS.Application.Features.Superadmin.Commands.UpdateSuperadminPassword
{
    public class UpdateSuperadminChangePasswordDto
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
