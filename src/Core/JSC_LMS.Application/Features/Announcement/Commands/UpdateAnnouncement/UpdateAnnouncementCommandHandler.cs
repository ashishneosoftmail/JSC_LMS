using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Exceptions;
using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Announcement.Commands.UpdateAnnouncement
{
    public class UpdateAnnouncementCommandHandler : IRequestHandler<UpdateAnnouncementCommand, Response<int>>
    {
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly IMapper _mapper;

        public UpdateAnnouncementCommandHandler(IMapper mapper, IAnnouncementRepository announcementRepository)
        {
            _mapper = mapper;
            _announcementRepository = announcementRepository;
        }

        public async Task<Response<int>> Handle(UpdateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var announcementToUpdate = await _announcementRepository.GetByIdAsync(request.updateAnnouncementDto.Id);

            if (announcementToUpdate == null)
            {
                throw new NotFoundException(nameof(JSC_LMS.Domain.Entities.Announcement), request.updateAnnouncementDto.Id);
            }



            announcementToUpdate.SchoolId = request.updateAnnouncementDto.SchoolId;
            announcementToUpdate.ClassId = request.updateAnnouncementDto.ClassId;
            announcementToUpdate.SectionId = request.updateAnnouncementDto.SectionId;
            announcementToUpdate.SubjectId = request.updateAnnouncementDto.SubjectId;
            announcementToUpdate.TeacherId = request.updateAnnouncementDto.TeacherId;
            announcementToUpdate.AnnouncementTitle = request.updateAnnouncementDto.AnnouncementTitle;
            announcementToUpdate.AnnouncementContent = request.updateAnnouncementDto.AnnouncementContent;
            announcementToUpdate.AnnouncementMadeBy = request.updateAnnouncementDto.AnnouncementMadeBy;
            announcementToUpdate.IsActive = request.updateAnnouncementDto.IsActive;


            await _announcementRepository.UpdateAsync(announcementToUpdate);

            return new Response<int>(request.updateAnnouncementDto.Id, "Updated successfully ");

        }
    }
}
