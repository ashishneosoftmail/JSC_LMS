using JSC_LMS.Application.Features.Students.Queries.GetStudentById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetStudentByIdResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public GetStudentByIdDto data { get; set; }
    }
}
