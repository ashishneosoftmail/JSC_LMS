using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Infrastructure;
using JSC_LMS.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Teachers.Queries.TeacherFileExport.TeacherCsvExport
{
    public class GetTeacherCsvExportQueryHandler : IRequestHandler<GetTeacherCsvExportQuery, TeacherCsvExportFileVm>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ISectionRepository _sectionRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IZipRepository _zipRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ICsvExporter _csvExporter;

        public GetTeacherCsvExportQueryHandler(IMapper mapper, ISubjectRepository subjectRepository, IZipRepository zipRepository, IStateRepository stateRepository, ICityRepository cityRepository, ISchoolRepository schoolRepository, ITeacherRepository teacherRepository, IClassRepository classRepository, ISectionRepository sectionRepository, IAuthenticationService authenticationService, ILogger<GetTeacherCsvExportQueryHandler> logger, ICsvExporter csvExporter)
        {
            _mapper = mapper;
            _teacherRepository = teacherRepository;
            _logger = logger;
            _authenticationService = authenticationService;
            _sectionRepository = sectionRepository;
            _classRepository = classRepository;
            _schoolRepository = schoolRepository;
            _cityRepository = cityRepository;
            _stateRepository = stateRepository;
            _zipRepository = zipRepository;
            _subjectRepository = subjectRepository;
            _csvExporter = csvExporter;
        }
        public async Task<TeacherCsvExportFileVm> Handle(GetTeacherCsvExportQuery request, CancellationToken cancellationToken)
        {
            var allTeachers = await _teacherRepository.ListAllAsync();
            List<TeacherCsvExportDto> teacherList = new List<TeacherCsvExportDto>();
            foreach (var teacher in allTeachers)
            {
                var user = await _authenticationService.GetUserById(teacher.UserId);
                teacherList.Add(new TeacherCsvExportDto()

                {
                    Id = teacher.Id,
                    AddressLine1 = teacher.AddressLine1,
                    AddressLine2 = teacher.AddressLine2,
                    Mobile = teacher.Mobile,
                    Username = user.Username,
                    Email = user.Email,
                    IsActive = teacher.IsActive,
                    // SubjectId = teacher.SubjectId,
                    TeacherName = teacher.TeacherName,
                    UserType = teacher.UserType,
                    SectionId = new SectionDto()
                    {
                        Id = teacher.SectionId,
                        SectionName = (await _sectionRepository.GetByIdAsync(teacher.SectionId)).SectionName
                    },
                    SubjectId = new SubjectDto()
                    {
                        Id = teacher.SubjectId,
                        SubjectName = (await _subjectRepository.GetByIdAsync(teacher.SubjectId)).SubjectName
                    },
                    ClassId = new ClassDto()
                    {
                        Id = teacher.SchoolId,
                        ClassName = (await _classRepository.GetByIdAsync(teacher.ClassId)).ClassName
                    },


                });
            }
            var fileData = _csvExporter.ExportTeacherToCsv(teacherList);

            var eventExportFileDto = new TeacherCsvExportFileVm() { ContentType = "text/csv", Data = (byte[])fileData, TeacherExportFileName = $"Teacher.csv" };

            return eventExportFileDto;

        }
    }
}
