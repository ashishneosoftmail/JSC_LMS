using JSC_LMS.Application.Models.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class Login : AuthenticationRequest
    {
        public List<SelectListItem> Roles { get; set; }
    }
}
