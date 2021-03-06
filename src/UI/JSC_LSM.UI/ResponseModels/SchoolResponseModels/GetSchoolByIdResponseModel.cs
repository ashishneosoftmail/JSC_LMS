using JSC_LMS.Application.Features.School.Queries.GetSchoolById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels.SchoolResponseModels
{
    public class GetSchoolByIdResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public GetSchoolByIdDto data { get; set; }
    }
}
