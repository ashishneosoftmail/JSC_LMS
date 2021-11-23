using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementById
{
    public class GetAnnouncementByIdQuery : IRequest<Response<GetAnnouncementByIdDto>>
    {
        public int Id { get; set; }
    }
}
