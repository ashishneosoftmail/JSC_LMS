using JSC_LMS.Application.Features.Gallary.Queries.GetAllGallaryFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels.GallaryResponseModel
{
    public class GetGallaryListBySchoolIdResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public GetAllGallaryByFilterDto data { get; set; }
    }
}
