using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Announcement.Commands.UpdateAnnouncement
{
    public class UpdateAnnouncementCommand : IRequest<Response<int>>
    {
        public UpdateAnnouncementCommand(UpdateAnnouncementDto _updateAnnouncementDto)
        {
            updateAnnouncementDto = _updateAnnouncementDto;
        }
        public UpdateAnnouncementDto updateAnnouncementDto { get; set; }
    }
}
