using JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAnnouncementByIdResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public GetAnnouncementByIdDto data { get; set; }
    }
}
