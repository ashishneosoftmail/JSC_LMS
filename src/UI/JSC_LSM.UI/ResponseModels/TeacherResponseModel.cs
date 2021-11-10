using JSC_LMS.Application.Features.Teachers.Commands.CreateTeacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class TeacherResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public CreateTeacherDto data { get; set; }
    }
}
