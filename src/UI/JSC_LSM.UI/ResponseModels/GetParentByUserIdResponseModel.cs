using JSC_LMS.Application.Features.ParentsFeature.Queries.GetParentByUserId;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetParentByUserIdResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public GetParentByUserIdDto data { get; set; }
    }
}
