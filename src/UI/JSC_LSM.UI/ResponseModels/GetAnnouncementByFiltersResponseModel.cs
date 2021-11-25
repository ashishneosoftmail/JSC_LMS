using JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementbyFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAnnouncementByFiltersResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public IEnumerable<GetAnnouncementByFilterDto> data { get; set; }
    }
}
