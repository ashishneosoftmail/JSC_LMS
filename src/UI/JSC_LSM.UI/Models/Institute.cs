using JSC_LMS.Application.Features.Institutes.Commands.CreateInstitute;
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
        "Password and confirm password do not match")]
        public string ConfirmPassword { get; set; }
        public List<SelectListItem> Cities { get; set; }
        public List<SelectListItem> States { get; set; }
        public List<SelectListItem> ZipCode { get; set; }

    }
}
