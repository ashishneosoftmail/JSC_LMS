using JSC_LMS.Application.Features.Announcement.Commands.CreateAnnouncement;
using JSC_LMS.Application.Features.Announcement.Commands.UpdateAnnouncement;
using JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class ManageAnnouncementModel
    {
        public AddAnnouncement AddAnnouncement { get; set; }
        public UpdateAnnouncement UpdateAnnouncement { get; set; }
        public List<SelectListItem> Schools { get; set; }
        public List<SelectListItem> Classes { get; set; }
        public List<SelectListItem> Sections { get; set; }
        public List<SelectListItem> Subjects { get; set; }
        public List<SelectListItem> Teachers { get; set; }
        public Pager Pager { get; set; }
        public IEnumerable<AnnouncementPagination> AnnouncementPagination { get; set; }

    }

    public class AddAnnouncement : CreateAnnouncementDto
    {
    }
    public class UpdateAnnouncement : UpdateAnnouncementDto
    {
    }
    public class AnnouncementPagination : GetAnnouncementListByPaginationDto
    {
    }
}
