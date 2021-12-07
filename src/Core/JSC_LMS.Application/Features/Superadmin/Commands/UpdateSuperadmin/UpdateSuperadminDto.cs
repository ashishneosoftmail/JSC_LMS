using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.Superadmin.Commands.UpdateSuperadmin
{
    public class UpdateSuperadminDto
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Name  is mandatory")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Phone Number is mandatory")]
        /* [RegularExpression(@"^\+?([0-9]{2})\)?[-. ]?([0-9]{5})[-. ]?([0-9]{5})$", ErrorMessage = "Please enter correct mobile number")]*/
        [RegularExpression(@"[6-9]\d{9}", ErrorMessage = "Please enter correct mobile number")]
        public string MobileSupport { get; set; }
        [Required(ErrorMessage = "Email is mandatory")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string EmailSupport { get; set; }
    }
}
