using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Announcement.Commands.CreateAnnouncement
{
    public class CreateAnnouncementCommand : IRequest<Response<CreateAnnouncementDto>>
    {
        public CreateAnnouncementDto _createAnnouncementDto { get; set; }
        public CreateAnnouncementCommand(CreateAnnouncementDto createAnnouncementDto)
        {
            _createAnnouncementDto = createAnnouncementDto;
        }

    }
}
