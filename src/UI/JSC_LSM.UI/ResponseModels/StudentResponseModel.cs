using JSC_LMS.Application.Features.Students.Commands.CreateStudent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class StudentResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public CreateStudentDto data { get; set; }
    }
}
