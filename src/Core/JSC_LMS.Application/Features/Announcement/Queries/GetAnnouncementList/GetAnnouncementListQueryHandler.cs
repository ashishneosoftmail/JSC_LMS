using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementList
{
    public class GetAnnouncementListQueryHandler : IRequestHandler<GetAnnouncementListQuery, Response<IEnumerable<GetAnnouncementListDto>>>
    {
        private readonly IClassRepository _classRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetAnnouncementListQueryHandler(IMapper mapper, IClassRepository classRepository, ISchoolRepository schoolRepository, ISectionRepository sectionRepository, ISubjectRepository subjectRepository, ITeacherRepository teacherRepository, IAnnouncementRepository announcementRepository, IAuthenticationService authenticationService, ILogger<GetAnnouncementListQueryHandler> logger)
        {
            _mapper = mapper;
            _classRepository = classRepository;
            _schoolRepository = schoolRepository;
            _sectionRepository = sectionRepository;
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
            _announcementRepository = announcementRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public async Task<Response<IEnumerable<GetAnnouncementListDto>>> Handle(GetAnnouncementListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allAnnouncement = await _announcementRepository.ListAllAsync();
            List<GetAnnouncementListDto> announcementList = new List<GetAnnouncementListDto>();
            foreach (var announcement in allAnnouncement)
            {
                announcementList.Add(new GetAnnouncementListDto()
                {
                    Id = announcement.Id,
                    CreatedDate = (DateTime)announcement.CreatedDate,
                    AnnouncementMadeBy = announcement.AnnouncementMadeBy,
                    AnnouncementTitle = announcement.AnnouncementTitle,
                    AnnouncementContent= announcement.AnnouncementContent,
                    IsActive = announcement.IsActive,
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
                });
            }
             _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetAnnouncementListDto>>(announcementList, "success");
        }

    }
}
