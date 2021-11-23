using JSC_LMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Domain.Entities
{
    public class Announcement  : AuditableEntity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int SchoolId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public string AnnouncementMadeBy { get; set; }
        public string AnnouncementTitle { get; set; }
        public string AnnouncementContent { get; set; }
        public virtual School School { get; set; }
        public virtual Class Class { get; set; }
        public virtual Section Section { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Teacher Teacher { get; set; }

    }
}
