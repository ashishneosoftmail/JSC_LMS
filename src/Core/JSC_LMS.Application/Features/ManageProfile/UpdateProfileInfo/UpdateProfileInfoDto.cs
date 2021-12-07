using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.ManageProfile.UpdateProfileInfo
{
  public  class UpdateProfileInfoDto
    {
        public string roleName { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is mandatory")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Phone Number is mandatory")]
        [RegularExpression(@"[6-9]\d{9}", ErrorMessage = "Please enter correct mobile number")]
        public string Mobile { get; set; }
    }
}
