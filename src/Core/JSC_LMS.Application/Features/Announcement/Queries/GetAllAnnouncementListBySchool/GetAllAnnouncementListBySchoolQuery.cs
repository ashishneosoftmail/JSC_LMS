using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Announcement.Queries.GetAllAnnouncementListBySchool
{
    public class GetAllAnnouncementListBySchoolQuery : IRequest<Response<IEnumerable<GetAllAnnouncementListBySchoolDto>>>
    {
        public int SchoolId { get; set; }
    }
}
