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

namespace JSC_LMS.Application.Features.Academics.Queries.AcademicCsvExport
{
    public class GetAcademicCsvExportQueryHandler : IRequestHandler<GetAcademicCsvExportQuery, AcademicCsvExportFileVm>
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
        private readonly ICsvExporter _csvExporter;


        public GetAcademicCsvExportQueryHandler(IMapper mapper, IClassRepository classRepository, ISchoolRepository schoolRepository, ISectionRepository sectionRepository, ISubjectRepository subjectRepository, ITeacherRepository teacherRepository, IAcademicRepository academicRepository, IAuthenticationService authenticationService, ICsvExporter csvExporter, ILogger<GetAcademicCsvExportQueryHandler> logger)
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
            _csvExporter = csvExporter;
        }

        public async Task<AcademicCsvExportFileVm> Handle(GetAcademicCsvExportQuery request, CancellationToken cancellationToken)
        {
            var allacademics = await _academicRepository.ListAllAsync();
            List<AcademicCsvExportDto> academicList = new List<AcademicCsvExportDto>();
            foreach (var academic in allacademics)
            {

                academicList.Add(new AcademicCsvExportDto()

                {
                    Id = academic.Id,
                    CutOff = academic.CutOff,

                    IsActive = academic.IsActive,

                    Type = academic.Type,

                    Section = new SectionDto()
                    {
                        Id = academic.SectionId,
                        SectionName = (await _sectionRepository.GetByIdAsync(academic.SectionId)).SectionName
                    },
                    Subject = new SubjectDto()
                    {
                        Id = academic.SubjectId,
                        SubjectName = (await _subjectRepository.GetByIdAsync(academic.SubjectId)).SubjectName
                    },
                    Class = new ClassDto()
                    {
                        Id = academic.ClassId,
                        ClassName = (await _classRepository.GetByIdAsync(academic.ClassId)).ClassName
                    },

                    School = new SchoolDto()
                    {
                        Id = academic.SchoolId,
                        SchoolName = (await _schoolRepository.GetByIdAsync(academic.SchoolId)).SchoolName
                    },
                    Teacher = new TeacherDto()
                    {
                        Id = academic.TeacherId,
                        TeacherName = (await _teacherRepository.GetByIdAsync(academic.TeacherId)).TeacherName
                    },


                });
            }
            var fileData = _csvExporter.ExportAcademicToCsv(academicList);

            var eventExportFileDto = new AcademicCsvExportFileVm() { ContentType = "text/csv", Data = (byte[])fileData, AcademicExportFileName = $"Academic.csv" };

            return eventExportFileDto;

        }
    }
}
