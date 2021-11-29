using JSC_LMS.Application.Features.School.Commands.CreateSchool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels.SchoolResponseModels
{
    public class SchoolResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public CreateSchoolDto data { get; set; }
    }
}
