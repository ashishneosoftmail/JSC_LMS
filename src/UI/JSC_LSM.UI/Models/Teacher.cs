using JSC_LMS.Application.Features.Teachers.Commands.CreateTeacher;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class Teacher : CreateTeacherDto
    {
        [Required(ErrorMessage = "Confirm Password Is Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage =
             "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public List<SelectListItem> Cities { get; set; }
        public List<SelectListItem> States { get; set; }
        public List<SelectListItem> ZipCode { get; set; }
        public List<SelectListItem> Schools { get; set; }
        public List<SelectListItem> Section { get; set; }
        public List<SelectListItem> Subject { get; set; }
        public List<SelectListItem> Class { get; set; }
    }
}
