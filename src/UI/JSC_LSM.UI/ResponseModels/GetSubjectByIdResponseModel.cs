using JSC_LMS.Application.Features.Subject.Queries.GetSubjectById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetSubjectByIdResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public GetSubjectByIdVm data { get; set; }
    }
}
