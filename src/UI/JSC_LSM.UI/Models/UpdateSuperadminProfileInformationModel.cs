using JSC_LMS.Application.Features.Superadmin.Commands.UpdateSuperadmin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class UpdateSuperadminProfileInformationModel : UpdateSuperadminDto
    {
        public byte[] Logo { get; set; }
        public byte[] LoginImage { get; set; }
    }
}
