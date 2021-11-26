using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Announcement.Queries.GetAllAnnouncementListBySchool
{
    public class GetAllAnnouncementListBySchoolDto
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
    public class SchoolDto
    {
        public int Id { get; set; }
        public string SchoolName { get; set; }
    }

    public class ClassDto
    {
        public int Id { get; set; }
        public string ClassName { get; set; }

    }
    public class SectionDto
    {
        public int Id { get; set; }
        public string SectionName { get; set; }
    }
    public class SubjectDto
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
    }

    public class TeacherDto
    {
        public int Id { get; set; }
        public string TeacherName { get; set; }

    }
}
