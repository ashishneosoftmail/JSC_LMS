using JSC_LMS.Application.Features.Gallary.Queries.GetAllGallaryList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels.GallaryResponseModel
{
    public class GetAllGallaryListResponseModel
    {
        public bool isSuccess { get; set; }
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public IEnumerable<GetAllGallaryListDto> data { get; set; }
    }
}
