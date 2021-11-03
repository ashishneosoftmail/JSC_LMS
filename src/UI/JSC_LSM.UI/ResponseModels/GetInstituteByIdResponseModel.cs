using JSC_LMS.Application.Features.Institutes.Queries.GetInstituteById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetInstituteByIdResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public GetInstituteByIdVm data { get; set; }
    }
}
