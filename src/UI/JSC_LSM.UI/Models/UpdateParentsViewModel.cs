using JSC_LMS.Application.Features.ParentsFeature.Commands.UpdateParents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class UpdateParentsViewModel : UpdateParentsDto
    {
        [Required(ErrorMessage = "Students data is required")]
        public List<int> StudentId { get; set; }
        public List<SelectListItem> Students { get; set; }
        public List<SelectListItem> Classes { get; set; }
        public List<SelectListItem> Sections { get; set; }
        public List<SelectListItem> Cities { get; set; }
        public List<SelectListItem> States { get; set; }
        public List<SelectListItem> ZipCode { get; set; }
        [StringLength(100, ErrorMessage = "Email should not be more than 100 characters")]
        [Required(ErrorMessage = "Email is mandatory")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        [Remote("CheckEmailExistsForUpdateParents", "User", HttpMethod = "POST", ErrorMessage = "EmailId already exists in database.")]

        public string EmailId { get; set; }
        [StringLength(100, ErrorMessage = "Username should not be more than 100 characters")]
        [Required(ErrorMessage = "Username is mandatory")]
        [Remote("CheckUsernameExistsForUpdateParents", "User", HttpMethod = "POST", ErrorMessage = "UserName already exists in database.")]
        public string UserNAME { get; set; }
    }
}
