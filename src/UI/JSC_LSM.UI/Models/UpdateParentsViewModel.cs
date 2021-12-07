using JSC_LMS.Application.Features.ParentsFeature.Commands.UpdateParents;
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
    }
}
