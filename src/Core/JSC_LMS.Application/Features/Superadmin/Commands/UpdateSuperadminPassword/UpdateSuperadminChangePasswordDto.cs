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
        [RegularExpression(@"^(?=.*?[A-Z])(?=(.*[a-z]){1,})(?=(.*[\d]){1,})(?=(.*[\W]){1,})(?!.*\s).{8,}$", ErrorMessage = "Password should contain 8 character - one uppercase, one lowercase, one numeric value and a special character.")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New Password is mandatory")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*?[A-Z])(?=(.*[a-z]){1,})(?=(.*[\d]){1,})(?=(.*[\W]){1,})(?!.*\s).{8,}$", ErrorMessage = "Password should contain 8 character - one uppercase, one lowercase, one numeric value and a special character.")]
        public string NewPassword { get; set; }


    }
}
