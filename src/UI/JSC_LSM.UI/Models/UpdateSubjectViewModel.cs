using JSC_LMS.Application.Features.Subject.Commands.UpdateSubject;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class UpdateSubjectViewModel : UpdateSubjectDto
    {
        public List<SelectListItem> Schools { get; set; }
        public List<SelectListItem> Classes { get; set; }
        public List<SelectListItem> Sections { get; set; }
    }
}
