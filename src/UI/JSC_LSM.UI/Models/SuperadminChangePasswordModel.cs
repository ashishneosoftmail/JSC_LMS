using JSC_LMS.Application.Features.Superadmin.Commands.UpdateSuperadminPassword;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class SuperadminChangePasswordModel : UpdateSuperadminChangePasswordDto
    {
        [Required(ErrorMessage = "Confirm Password Is Required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage =
           "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
