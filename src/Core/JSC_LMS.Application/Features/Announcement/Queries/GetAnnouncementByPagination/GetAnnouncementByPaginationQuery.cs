
using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination
{
    public class GetAnnouncementByPaginationQuery : IRequest<Response<GetAnnouncementByPaginationResponse>>
    {
        public int page { get; set; }
        public int size { get; set; }
    }
}
