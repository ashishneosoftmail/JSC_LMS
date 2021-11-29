using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Announcement.Queries.GetAllAnnouncementListBySchoolClassSectionPagination
{
    
    public class GetAllAnnouncementListBySchoolClassSectionPaginationQuery : IRequest<Response<IEnumerable<GetAllAnnouncementListBySchoolClassSectionPaginationDto>>>
    {
        public int page { get; set; }
        public int size { get; set; }
        public int SchoolId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
    }
}
