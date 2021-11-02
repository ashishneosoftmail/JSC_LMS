using JSC_LMS.Application.Features.Class.Commands.CreateClass;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class ClassModel : CreateClassDto
    {
        public List<SelectListItem> Schools { get; set; }
    }
}
