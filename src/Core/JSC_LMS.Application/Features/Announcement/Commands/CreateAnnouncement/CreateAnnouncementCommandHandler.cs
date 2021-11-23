using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Announcement.Commands.CreateAnnouncement
{
    public class CreateAnnouncementCommandHandler : IRequestHandler<CreateAnnouncementCommand, Response<CreateAnnouncementDto>>
    {
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly IMapper _mapper;

        public CreateAnnouncementCommandHandler(IMapper mapper, IAnnouncementRepository announcementRepository)
        {
            _mapper = mapper;
            _announcementRepository = announcementRepository;
        }

        public async Task<Response<CreateAnnouncementDto>> Handle(CreateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var createAnnouncementCommandResponse = new Response<CreateAnnouncementDto>();

                var data = new JSC_LMS.Domain.Entities.Announcement()
                {
                    ClassId = request._createAnnouncementDto.ClassId,
                    SectionId = request._createAnnouncementDto.SectionId,
                    AnnouncementMadeBy=request._createAnnouncementDto.AnnouncementMadeBy,
                    TeacherId = request._createAnnouncementDto.TeacherId,
                    SubjectId = request._createAnnouncementDto.SubjectId,
                    SchoolId = request._createAnnouncementDto.SchoolId,
                    AnnouncementTitle = request._createAnnouncementDto.AnnouncementTitle,
                    AnnouncementContent = request._createAnnouncementDto.AnnouncementContent,
                    IsActive = true
                };
                var announcement = await _announcementRepository.AddAsync(data);
            createAnnouncementCommandResponse.Data = _mapper.Map<CreateAnnouncementDto>(announcement);
            createAnnouncementCommandResponse.Succeeded = true;
            createAnnouncementCommandResponse.Message = "success";
            

            return createAnnouncementCommandResponse;
        }

    }
}
