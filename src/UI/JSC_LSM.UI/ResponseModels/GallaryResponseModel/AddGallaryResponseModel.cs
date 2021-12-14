using JSC_LMS.Application.Features.Gallary.Commands.UploadImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels.GallaryResponseModel
{
    public class AddGallaryResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public UploadImageDto data { get; set; }
    }
}
