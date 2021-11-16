using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Academics.Queries.GetAcademicByFilter
{
   public class GetAcademicByFilterQueryHandler : IRequestHandler<GetAcademicByFilterQuery, Response<IEnumerable<GetAcademicByFilterDto>>>
    {
        private readonly IClassRepository _classRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IAcademicRepository _academicRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetAcademicByFilterQueryHandler(IMapper mapper, IClassRepository classRepository, ISchoolRepository schoolRepository, ISectionRepository sectionRepository, ISubjectRepository subjectRepository, ITeacherRepository teacherRepository, IAcademicRepository academicRepository, IAuthenticationService authenticationService, ILogger<GetAcademicByFilterQueryHandler> logger)
        {
            _mapper = mapper;
            _classRepository = classRepository;
            _schoolRepository = schoolRepository;
            _sectionRepository = sectionRepository;
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
            _academicRepository = academicRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }


        public async Task<Response<IEnumerable<GetAcademicByFilterDto>>> Handle(GetAcademicByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allAcademic = await _academicRepository.ListAllAsync();


            if (request.SchoolName != "Select School")
            {
                allAcademic = allAcademic.Where<JSC_LMS.Domain.Entities.Academic>(x => (x.School.SchoolName == request.SchoolName)).ToList();

            }
            if (request.ClassName != "Select Class")
            {
                allAcademic = allAcademic.Where<JSC_LMS.Domain.Entities.Academic>(x => (x.Class.ClassName == request.ClassName)).ToList();

            }
            if (request.TeacherName != "Select Teacher")
            {
                allAcademic = allAcademic.Where<JSC_LMS.Domain.Entities.Academic>(x => (x.Teacher.TeacherName == request.TeacherName)).ToList();

            }
            if (request.SectionName != "Select Section")
            {
                allAcademic = allAcademic.Where<JSC_LMS.Domain.Entities.Academic>(x => (x.Section.SectionName == request.SectionName)).ToList();

            }
            if (request.SubjectName != "Select Subject")
            {
                allAcademic = allAcademic.Where<JSC_LMS.Domain.Entities.Academic>(x => (x.Subject.SubjectName == request.SubjectName)).ToList();

            }
            if (request.Type != "Select Type")
            {
                allAcademic = allAcademic.Where<JSC_LMS.Domain.Entities.Academic>(x => (x.Type == request.Type)).ToList();

            }
            if (request.IsActive)
            {
                allAcademic = allAcademic.Where<JSC_LMS.Domain.Entities.Academic>(x => x.IsActive == request.IsActive).ToList();
            }
            else
            {
                allAcademic = allAcademic.Where<JSC_LMS.Domain.Entities.Academic>(x => x.IsActive == request.IsActive).ToList();
            }
            Response<IEnumerable<GetAcademicByFilterDto>> responseData = new Response<IEnumerable<GetAcademicByFilterDto>>();
            List<GetAcademicByFilterDto> academicList = new List<GetAcademicByFilterDto>();
            foreach (var academic in allAcademic)
            {
                academicList.Add(new GetAcademicByFilterDto()
                {
                    Id = academic.Id,
                  
                    IsActive = academic.IsActive,
                    CutOff = academic.CutOff,
                    Type = academic.Type,
                    CreatedDate = (DateTime)academic.CreatedDate,
                 
                    Class = new ClassDto()
                    {
                        Id = academic.ClassId,
                        ClassName = (await _classRepository.GetByIdAsync(academic.ClassId)).ClassName
                    },

                    School = new SchoolFilterDto()
                    {
                        Id = academic.SchoolId,
                        SchoolName = (await _schoolRepository.GetByIdAsync(academic.SchoolId)).SchoolName
                    },

                    Teacher = new TeacherDto()
                    {
                        Id = academic.TeacherId,
                        TeacherName = (await _teacherRepository.GetByIdAsync(academic.TeacherId)).TeacherName
                    },

                    Subject = new SubjectDto()
                    {
                        Id = academic.SubjectId,
                        SubjectName = (await _subjectRepository.GetByIdAsync(academic.SubjectId)).SubjectName
                    },

                    Section = new SectionDto()
                    {
                        Id = academic.SectionId,
                        SectionName = (await _sectionRepository.GetByIdAsync(academic.SectionId)).SectionName
                    }
                });


            }
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetAcademicByFilterDto>>(academicList, "success");
        }
    }
}
