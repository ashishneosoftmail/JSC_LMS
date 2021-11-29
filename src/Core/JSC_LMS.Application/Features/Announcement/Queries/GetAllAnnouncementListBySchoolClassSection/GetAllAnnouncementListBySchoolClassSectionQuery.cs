using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Announcement.Queries.GetAllAnnouncementListBySchoolClassSection
{
    public class GetAllAnnouncementListBySchoolClassSectionQuery :IRequest<Response<IEnumerable<GetAllAnnouncementListBySchoolClassSectionDto>>>
    {
        public int SchoolId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
    }
}
