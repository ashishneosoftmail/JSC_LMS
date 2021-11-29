using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;

namespace JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementListByPaginationBySchool
{
    public class GetAnnouncementListByPaginationBySchoolQuery : IRequest<Response<IEnumerable<GetAnnouncementListByPaginationBySchoolDto>>>
    {
        public int page { get; set; }
        public int size { get; set; }
        public int SchoolId { get; set; }
    }
}
