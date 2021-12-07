using JSC_LMS.Application.Features.Principal.Commands.CreatePrincipal;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class PrincipalModel : CreatePrincipalDto
    {
        [Required(ErrorMessage = "Confirm Password is mandatory")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage =
           "Password and Confirm password do not match.")]
        public string ConfirmPassword { get; set; }
        public List<SelectListItem> Schools { get; set; }
        public List<SelectListItem> Cities { get; set; }
        public List<SelectListItem> States { get; set; }
        public List<SelectListItem> ZipCode { get; set; }
    }
}
