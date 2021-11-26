using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementbyFilter
{
    public class GetAnnouncementByFilterQuery : IRequest<Response<IEnumerable<GetAnnouncementByFilterDto>>>
    {
        public GetAnnouncementByFilterQuery( int _SchoolId, int _ClassId, int _SectionId, int _SubjectId,  string _TeacherName, string _AnnouncementMadeBy, string _AnnouncementTitle , string _AnnouncementContent,  DateTime _CreatedDate)
        {
            ClassId = _ClassId;
            SchoolId = _SchoolId;
            SubjectId = _SubjectId;
            TeacherName = _TeacherName;
            SectionId = _SectionId;
            AnnouncementMadeBy = _AnnouncementMadeBy;
            AnnouncementTitle = _AnnouncementTitle;
            AnnouncementContent = _AnnouncementContent;           
            CreatedDate = _CreatedDate;
        }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int SchoolId { get; set; }
        public string TeacherName { get; set; }
        public int SectionId { get; set; }
        public string AnnouncementMadeBy { get; set; }
        public string AnnouncementTitle { get; set; }
        public string AnnouncementContent { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
