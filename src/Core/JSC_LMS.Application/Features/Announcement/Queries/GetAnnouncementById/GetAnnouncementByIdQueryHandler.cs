using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementById
{
    public class GetAnnouncementByIdQueryHandler : IRequestHandler<GetAnnouncementByIdQuery, Response<GetAnnouncementByIdDto>>
    {
        private readonly IClassRepository _classRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetAnnouncementByIdQueryHandler(IMapper mapper, IClassRepository classRepository, ISchoolRepository schoolRepository, ISectionRepository sectionRepository, ISubjectRepository subjectRepository, ITeacherRepository teacherRepository, IAnnouncementRepository announcementRepository,  ILogger<GetAnnouncementByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _classRepository = classRepository;
            _schoolRepository = schoolRepository;
            _sectionRepository = sectionRepository;
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
            _announcementRepository = announcementRepository;
            _logger = logger;
        }
        public async Task<Response<GetAnnouncementByIdDto>> Handle(GetAnnouncementByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            Response<GetAnnouncementByIdDto> responseData = new Response<GetAnnouncementByIdDto>();
            var announcement = await _announcementRepository.GetByIdAsync(request.Id);
            if (announcement == null)
            {
                responseData.Succeeded = true;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }

            var announcementdata = new GetAnnouncementByIdDto()
            {
                Id = announcement.Id,

                IsActive = announcement.IsActive,
                AnnouncementContent = announcement.AnnouncementContent,
                AnnouncementTitle = announcement.AnnouncementTitle,
                AnnouncementMadeBy= announcement.AnnouncementMadeBy,
                CreatedDate = (DateTime)announcement.CreatedDate,

                School = new SchoolDto()
                {
                    Id = announcement.SchoolId,
                    SchoolName = (await _schoolRepository.GetByIdAsync(announcement.SchoolId)).SchoolName
                },

                Class = new ClassDto()
                {
                    Id = announcement.ClassId,
                    ClassName = (await _classRepository.GetByIdAsync(announcement.ClassId)).ClassName
                },


                Section = new SectionDto()
                {
                    Id = announcement.SectionId,
                    SectionName = (await _sectionRepository.GetByIdAsync(announcement.SectionId)).SectionName
                },

                Subject = new SubjectDto()
                {
                    Id = announcement.SubjectId,
                    SubjectName = (await _subjectRepository.GetByIdAsync(announcement.SubjectId)).SubjectName
                },
                Teacher = new TeacherDto()
                {
                    Id = announcement.TeacherId,
                    TeacherName = (await _teacherRepository.GetByIdAsync(announcement.TeacherId)).TeacherName
                }

            };
            _logger.LogInformation("Handle Completed");
            return new Response<GetAnnouncementByIdDto>(announcementdata, "success");
        }

    }
}
