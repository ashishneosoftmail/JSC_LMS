using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination
{
    public class GetAnnouncementByPaginationResponse
    {
        public IEnumerable<GetAnnouncementListByPaginationDto> GetAnnouncementListPaginationDto { get; set; }
        public int Count { get; set; }
    }
}
