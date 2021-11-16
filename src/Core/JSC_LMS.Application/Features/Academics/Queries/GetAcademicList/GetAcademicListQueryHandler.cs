using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Academics.Queries.GetAcademicList
{
    public class GetAcademicListQueryHandler : IRequestHandler<GetAcademicListQuery, Response<IEnumerable<GetAcademicListDto>>>
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
        public GetAcademicListQueryHandler(IMapper mapper, IClassRepository classRepository, ISchoolRepository schoolRepository, ISectionRepository sectionRepository, ISubjectRepository subjectRepository, ITeacherRepository teacherRepository , IAcademicRepository academicRepository,IAuthenticationService authenticationService, ILogger<GetAcademicListQueryHandler> logger)
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

        public async Task<Response<IEnumerable<GetAcademicListDto>>> Handle(GetAcademicListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allAcademics = await _academicRepository.ListAllAsync();
            List<GetAcademicListDto> academicList = new List<GetAcademicListDto>();
            foreach (var academics in allAcademics)
            {
                academicList.Add(new GetAcademicListDto()
                {
                    Id = academics.Id,
                    CreatedDate = (DateTime)academics.CreatedDate,
                    CutOff = academics.CutOff,
                    Type = academics.Type,
                    IsActive = academics.IsActive,
                    School = new SchoolDto()
                    {
                        Id = academics.SchoolId,
                        SchoolName = (await _schoolRepository.GetByIdAsync(academics.SchoolId)).SchoolName
                    },

                     Class = new ClassDto()
                     {
                         Id = academics.ClassId,
                         ClassName = (await _classRepository.GetByIdAsync(academics.ClassId)).ClassName
                     },
                     

                    Section = new SectionDto()
                    {
                        Id = academics.SectionId,
                        SectionName = (await _sectionRepository.GetByIdAsync(academics.SectionId)).SectionName
                    },

                    Subject = new SubjectDto()
                    {
                        Id = academics.SubjectId,
                        SubjectName = (await _subjectRepository.GetByIdAsync(academics.SubjectId)).SubjectName
                    },
                         Teacher = new TeacherDto()
                    {
                        Id = academics.TeacherId,
                        TeacherName = (await _teacherRepository.GetByIdAsync(academics.TeacherId)).TeacherName
                         }
                }) ;
            }
            /* var principal = _mapper.Map<List<GetPrincipalListDto>>(allPrincipal);
             _logger.LogInformation("Hanlde Completed");
             return new Response<IEnumerable<GetPrincipalListDto>>(principal, "success");*/
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetAcademicListDto>>(academicList, "success");
        }

    }
}
