using JSC_LMS.Application.Features.Principal.Queries.GetPrincipalByUserId;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetPrincipalByUserIdResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public GetPrincipalByUserIdDto data { get; set; }
    }
}
