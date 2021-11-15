using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class ManageProfile
    {
        public ProfileInformation ProfileInformation { get; set; }
        public ChangePassword ChangePassword { get; set; }
    }
    public class ProfileInformation
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Phone Number Is Required")]
        [RegularExpression(@"[6-9]\d{9}", ErrorMessage = "Please enter correct mobile number")]
        public string Mobile { get; set; }
    }
    public class ChangePassword
    {
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
