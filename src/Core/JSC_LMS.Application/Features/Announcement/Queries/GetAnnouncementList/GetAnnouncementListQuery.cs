using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementList
{
    public class GetAnnouncementListQuery :IRequest<Response<IEnumerable<GetAnnouncementListDto>>>
    {
    }
}
