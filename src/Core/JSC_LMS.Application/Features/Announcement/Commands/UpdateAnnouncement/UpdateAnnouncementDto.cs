using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.Announcement.Commands.UpdateAnnouncement
{
    public class UpdateAnnouncementDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Schhol Name is mandatory")]
        public int SchoolId { get; set; }
        [Required(ErrorMessage = "Class is mandatory")]
        public int ClassId { get; set; }
        [Required(ErrorMessage = "Section is mandatory")]
        public int SectionId { get; set; }
        [Required(ErrorMessage = "Subject is mandatory")]
        public int SubjectId { get; set; }
        [Required(ErrorMessage = "Teacher's name is mandatory")]
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "Announcement Made By is mandatory")]
        public string AnnouncementMadeBy { get; set; }
        [Required(ErrorMessage = "Title is mandatory")]
        [StringLength(200, ErrorMessage = "Announcement Title length should not be more than 200 characters")]
        public string AnnouncementTitle { get; set; }
        [Required(ErrorMessage = "Please give the announcement in brief details")]
        public string AnnouncementContent { get; set; }

    }
}
