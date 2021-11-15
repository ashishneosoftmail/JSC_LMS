using JSC_LMS.Application.Features.Institutes.Queries.GetInstituteAdminByUserId;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetInstituteAdminByUserIdResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public GetInstituteAdminByUserIdDto data { get; set; }
    }
}
