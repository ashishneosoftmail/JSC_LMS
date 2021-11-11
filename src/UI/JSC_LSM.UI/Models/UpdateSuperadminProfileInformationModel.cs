using JSC_LMS.Application.Features.Superadmin.Commands.UpdateSuperadmin;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class UpdateSuperadminProfileInformationModel : UpdateSuperadminDto
    {
        public string LogoFileName { get; set; }
        public string LoginImageFileName { get; set; }
        public IFormFile Logo { get; set; }
        public IFormFile LoginImage { get; set; }
        public SuperadminChangePasswordModel SuperadminChangePasswordModel { get; set; }
    }
}
