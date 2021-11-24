using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementbyFilter
{
    public class GetAnnouncementByFilterQuery : IRequest<Response<IEnumerable<GetAnnouncementByFilterDto>>>
    {
        public GetAnnouncementByFilterQuery( string _SchoolName, string _ClassName, string _SectionName, string _SubjectName,  string _TeacherName, string _AnnouncementMadeBy, string _AnnouncementTitle , string _AnnouncementContent,  DateTime _CreatedDate)
        {
            ClassName = _ClassName;
            SchoolName = _SchoolName;
            SubjectName = _SubjectName;
            TeacherName = _TeacherName;
            SectionName = _SectionName;
            AnnouncementMadeBy = _AnnouncementMadeBy;
            AnnouncementTitle = _AnnouncementTitle;
            AnnouncementContent = _AnnouncementContent;           
            CreatedDate = _CreatedDate;
        }
        public string ClassName { get; set; }
        public string SubjectName { get; set; }
        public string SchoolName { get; set; }
        public string TeacherName { get; set; }
        public string SectionName { get; set; }
        public string AnnouncementMadeBy { get; set; }
        public string AnnouncementTitle { get; set; }
        public string AnnouncementContent { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
