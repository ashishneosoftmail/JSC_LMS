using JSC_LMS.Application.Features.Academics.Queries.GetAcademicById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAcademicByIdResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public GetAcademicByIdDto data { get; set; }
    }
}
