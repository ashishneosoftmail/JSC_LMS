using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementbyFilter
{
    public class GetAnnouncementByFilterQueryHandler : IRequestHandler<GetAnnouncementByFilterQuery, Response<IEnumerable<GetAnnouncementByFilterDto>>>
    {
        private readonly IClassRepository _classRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetAnnouncementByFilterQueryHandler(IMapper mapper, IClassRepository classRepository, ISchoolRepository schoolRepository, ISectionRepository sectionRepository, ISubjectRepository subjectRepository, ITeacherRepository teacherRepository, IAnnouncementRepository announcementRepository, ILogger<GetAnnouncementByFilterQueryHandler> logger)
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


        public async Task<Response<IEnumerable<GetAnnouncementByFilterDto>>> Handle(GetAnnouncementByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allAnnouncement = await _announcementRepository.ListAllAsync();


            if (request.SchoolId>0)
            {
                allAnnouncement = allAnnouncement.Where<JSC_LMS.Domain.Entities.Announcement>(x => (x.SchoolId == request.SchoolId)).ToList();

            }
            if (request.ClassId > 0)
            {
                allAnnouncement = allAnnouncement.Where<JSC_LMS.Domain.Entities.Announcement>(x => (x.ClassId == request.ClassId)).ToList();

            }
            if (request.TeacherName != "Select Teacher")
            {
                allAnnouncement = allAnnouncement.Where<JSC_LMS.Domain.Entities.Announcement>(x => (x.Teacher.TeacherName == request.TeacherName)).ToList();

            }
            if (request.SectionId > 0)
            {
                allAnnouncement = allAnnouncement.Where<JSC_LMS.Domain.Entities.Announcement>(x => (x.SectionId == request.SectionId)).ToList();

            }
            if (request.SubjectId > 0)
            {
                allAnnouncement = allAnnouncement.Where<JSC_LMS.Domain.Entities.Announcement>(x => (x.SubjectId == request.SubjectId)).ToList();

            }
            if (request.AnnouncementTitle != null)
            {
                allAnnouncement = allAnnouncement.Where<JSC_LMS.Domain.Entities.Announcement>(x => (x.AnnouncementTitle.Contains(request.AnnouncementTitle))).ToList();

            }
            if (request.AnnouncementContent != null)
            {
                allAnnouncement = allAnnouncement.Where<JSC_LMS.Domain.Entities.Announcement>(x => (x.AnnouncementContent.Contains(request.AnnouncementContent))).ToList();

            }
            if (request.AnnouncementMadeBy != "Select Type")
            {
                allAnnouncement = allAnnouncement.Where<JSC_LMS.Domain.Entities.Announcement>(x => (x.AnnouncementMadeBy == request.AnnouncementMadeBy)).ToList();

            }
            if (request.CreatedDate.ToShortDateString() != "01-01-0001")
            {
                allAnnouncement = allAnnouncement.Where<JSC_LMS.Domain.Entities.Announcement>(x => x.CreatedDate?.ToShortDateString() == request.CreatedDate.ToShortDateString()).ToList();
            }

            Response<IEnumerable<GetAnnouncementByFilterDto>> responseData = new Response<IEnumerable<GetAnnouncementByFilterDto>>();
            List<GetAnnouncementByFilterDto> announcementList = new List<GetAnnouncementByFilterDto>();
            foreach (var announcement in allAnnouncement)
            {
                announcementList.Add(new GetAnnouncementByFilterDto()
                {
                    Id = announcement.Id,
                    IsActive = announcement.IsActive,
                    AnnouncementContent = announcement.AnnouncementContent,
                    AnnouncementMadeBy = announcement.AnnouncementMadeBy,
                    AnnouncementTitle = announcement.AnnouncementTitle,
                    CreatedDate = (DateTime)announcement.CreatedDate,

                    Class = new ClassDto()
                    {
                        Id = announcement.ClassId,
                        ClassName = (await _classRepository.GetByIdAsync(announcement.ClassId)).ClassName
                    },

                    School = new SchoolDto()
                    {
                        Id = announcement.SchoolId,
                        SchoolName = (await _schoolRepository.GetByIdAsync(announcement.SchoolId)).SchoolName
                    },

                    Teacher = new TeacherDto()
                    {
                        Id = announcement.TeacherId,
                        TeacherName = (await _teacherRepository.GetByIdAsync(announcement.TeacherId)).TeacherName
                    },

                    Subject = new SubjectDto()
                    {
                        Id = announcement.SubjectId,
                        SubjectName = (await _subjectRepository.GetByIdAsync(announcement.SubjectId)).SubjectName
                    },

                    Section = new SectionDto()
                    {
                        Id = announcement.SectionId,
                        SectionName = (await _sectionRepository.GetByIdAsync(announcement.SectionId)).SectionName
                    }
                });


            }
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetAnnouncementByFilterDto>>(announcementList, "success");
        }
    }
}
