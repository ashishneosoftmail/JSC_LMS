using JSC_LMS.Application.Features.Institutes.Commands.CreateInstitute;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class Institute : CreateInstituteDto
    {

        [Required(ErrorMessage = "Confirm Password is mandatory")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage =
        "Password and Confirm password do not match")]
        public string ConfirmPassword { get; set; }
        public List<SelectListItem> Cities { get; set; }
        public List<SelectListItem> States { get; set; }
        public List<SelectListItem> ZipCode { get; set; }

        [StringLength(100, ErrorMessage = "Email should not be more than 100 characters")]
        [Required(ErrorMessage = "Email is mandatory")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        [Remote("CheckEmailExists", "Institute", HttpMethod = "POST", ErrorMessage = "EmailId already exists in database.")]

        public string EmailId { get; set; }
        [StringLength(100, ErrorMessage = "Username should not be more than 100 characters")]
        [Required(ErrorMessage = "Username is mandatory")]
        [Remote("CheckUserNameExists", "Institute", HttpMethod = "POST", ErrorMessage = "UserName already exists in database.")]
        public string UserNAME { get; set; }

    }
}
