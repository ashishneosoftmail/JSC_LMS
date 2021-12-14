using JSC_LMS.Application.Features.Gallary.Queries.GetGallaryById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels.GallaryResponseModel
{
    public class GetGallaryListByIdResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public GetGallaryByIdDto data { get; set; }
    }
}
