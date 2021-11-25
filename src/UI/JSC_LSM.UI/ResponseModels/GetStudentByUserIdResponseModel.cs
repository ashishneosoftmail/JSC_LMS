using JSC_LMS.Application.Features.Students.Queries.GetStudentByUserId;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetStudentByUserIdResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public GetStudentByUserIdDto data { get; set; }
    }
}
