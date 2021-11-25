using JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAnnouncementListByPaginationResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public IEnumerable<GetAnnouncementListByPaginationDto> data { get; set; }
    }
}
