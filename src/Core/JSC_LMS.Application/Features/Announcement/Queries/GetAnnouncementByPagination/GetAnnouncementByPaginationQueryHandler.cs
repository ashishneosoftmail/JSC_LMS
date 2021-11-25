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

namespace JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination
{

    public class GetAnnouncementByPaginationQueryHandler : IRequestHandler<GetAnnouncementByPaginationQuery, Response<IEnumerable<GetAnnouncementListByPaginationDto>>>
    {
        private readonly IClassRepository _classRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetAnnouncementByPaginationQueryHandler(IMapper mapper, IClassRepository classRepository, ISchoolRepository schoolRepository, ISectionRepository sectionRepository, ISubjectRepository subjectRepository, ITeacherRepository teacherRepository, IAnnouncementRepository announcementRepository, ILogger
            <GetAnnouncementByPaginationQueryHandler> logger)
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

        public async Task<Response<IEnumerable<GetAnnouncementListByPaginationDto>>> Handle(GetAnnouncementByPaginationQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allAnnouncement = await _announcementRepository.GetPagedReponseAsync(request.page, request.size);
            List<GetAnnouncementListByPaginationDto> announcementList = new List<GetAnnouncementListByPaginationDto>();
            foreach (var announcement in allAnnouncement)
            {
                announcementList.Add(new GetAnnouncementListByPaginationDto()
                {
                    Id = announcement.Id,
                    AnnouncementMadeBy = announcement.AnnouncementMadeBy,
                    IsActive = announcement.IsActive,
                    AnnouncementTitle = announcement.AnnouncementTitle,
                    AnnouncementContent = announcement.AnnouncementContent,
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
                    Subject = new SubjectDto()
                    {
                        Id = announcement.SubjectId,
                        SubjectName = (await _subjectRepository.GetByIdAsync(announcement.SubjectId)).SubjectName
                    },

                    Teacher = new TeacherDto()
                    {
                        Id = announcement.TeacherId,
                        TeacherName = (await _teacherRepository.GetByIdAsync(announcement.TeacherId)).TeacherName
                    },
                    Section = new SectionDto()
                    {
                        Id = announcement.SectionId,
                        SectionName = (await _sectionRepository.GetByIdAsync(announcement.SectionId)).SectionName
                    }

                });
            }

            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetAnnouncementListByPaginationDto>>(announcementList, "success");
        }

    }



}


