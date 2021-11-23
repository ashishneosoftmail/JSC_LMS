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

namespace JSC_LMS.Application.Features.Academics.Queries.GetAcademicById
{
   public class GetAcademicByIdQueryHandler : IRequestHandler<GetAcademicByIdQuery, Response<GetAcademicByIdDto>>
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
        public GetAcademicByIdQueryHandler(IMapper mapper, IClassRepository classRepository, ISchoolRepository schoolRepository, ISectionRepository sectionRepository, ISubjectRepository subjectRepository, ITeacherRepository teacherRepository, IAcademicRepository academicRepository, IAuthenticationService authenticationService, ILogger<GetAcademicByIdQueryHandler> logger)
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
        public async Task<Response<GetAcademicByIdDto>> Handle(GetAcademicByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            Response<GetAcademicByIdDto> responseData = new Response<GetAcademicByIdDto>();
            var academicdata = await _academicRepository.GetByIdAsync(request.Id);
            if (academicdata == null)
            {
                responseData.Succeeded = true;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }

            var academicData = new GetAcademicByIdDto()
            {
                Id = academicdata.Id,

                IsActive = academicdata.IsActive,
                CutOff = academicdata.CutOff,
                Type=academicdata.Type,
                CreatedDate = (DateTime)academicdata.CreatedDate,

                School = new SchoolDto()
                {
                    Id = academicdata.SchoolId,
                    SchoolName = (await _schoolRepository.GetByIdAsync(academicdata.SchoolId)).SchoolName
                },

                Class = new ClassDto()
                {
                    Id = academicdata.ClassId,
                    ClassName = (await _classRepository.GetByIdAsync(academicdata.ClassId)).ClassName
                },


                Section = new SectionDto()
                {
                    Id = academicdata.SectionId,
                    SectionName = (await _sectionRepository.GetByIdAsync(academicdata.SectionId)).SectionName
                },

                Subject = new SubjectDto()
                {
                    Id = academicdata.SubjectId,
                    SubjectName = (await _subjectRepository.GetByIdAsync(academicdata.SubjectId)).SubjectName
                },
                Teacher = new TeacherDto()
                {
                    Id = academicdata.TeacherId,
                    TeacherName = (await _teacherRepository.GetByIdAsync(academicdata.TeacherId)).TeacherName
                }

            };
            _logger.LogInformation("Hanlde Completed");
            return new Response<GetAcademicByIdDto>(academicData, "success");
        }

    }
}
