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
        [Required(ErrorMessage = "Schhol name Is Required")]
        public int SchoolId { get; set; }
        [Required(ErrorMessage = "Class Is Required")]
        public int ClassId { get; set; }
        [Required(ErrorMessage = "Section Is Required")]
        public int SectionId { get; set; }
        [Required(ErrorMessage = "Subject Is Required")]
        public int SubjectId { get; set; }
        [Required(ErrorMessage = "Teacher's name Is Required")]
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "Announcement Made By Is Required")]
        public string AnnouncementMadeBy { get; set; }
        [Required(ErrorMessage = "Title Is Required")]
        [StringLength(200, ErrorMessage = "Announcement Title length should not be more than 200 characters")]
        public string AnnouncementTitle { get; set; }
        [Required(ErrorMessage = "Please give the announcement in brief details")]
        public string AnnouncementContent { get; set; }

    }
}
