using JSC_LMS.Application.Features.Principal.Queries.GetPrincipalById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetPrincipalByIdResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public GetPrincipalByIdDto data { get; set; }
    }
}
