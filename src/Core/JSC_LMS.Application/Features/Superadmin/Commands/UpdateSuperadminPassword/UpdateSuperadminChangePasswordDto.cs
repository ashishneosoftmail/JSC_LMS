using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;

namespace JSC_LMS.Application.Features.Superadmin.Commands.UpdateSuperadminPassword
{
    public class UpdateSuperadminChangePasswordDto
    {
        public ClaimsPrincipal User { get; set; }
        public string UserId { get; set; }
        [Required(ErrorMessage = "Current Password Is Required")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New Password Is Required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password Is Required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage =
            "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
