using JSC_LMS.Application.Features.Academics.Commands.CreateAcademic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class AcademicResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public CreateAcademicDto data { get; set; }
    }
}
