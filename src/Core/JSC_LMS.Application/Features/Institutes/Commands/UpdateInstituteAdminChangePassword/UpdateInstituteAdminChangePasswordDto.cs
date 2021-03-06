using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.Institutes.Commands.UpdateInstituteAdminChangePassword
{
    public class UpdateInstituteAdminChangePasswordDto
    {
        public string UserId { get; set; }
        [Required(ErrorMessage = "Current Password Is Required")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New Password Is Required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
