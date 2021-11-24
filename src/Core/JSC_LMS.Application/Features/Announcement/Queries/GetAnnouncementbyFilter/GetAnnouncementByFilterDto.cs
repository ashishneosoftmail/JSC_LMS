using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementbyFilter
{
  public   class GetAnnouncementByFilterDto
    {
        public int Id { get; set; }
        public string AnnouncementMadeBy { get; set; }
        public string AnnouncementTitle { get; set; }
        public string AnnouncementContent { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public SchoolDto School { get; set; }
        public ClassDto Class { get; set; }
        public SectionDto Section { get; set; }
        public SubjectDto Subject { get; set; }
        public TeacherDto Teacher { get; set; }

    }
}
