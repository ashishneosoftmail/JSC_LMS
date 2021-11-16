using CsvHelper;
using JSC_LMS.Application.Contracts.Infrastructure;
using JSC_LMS.Application.Features.Academics.Queries.AcademicCsvExport;
using JSC_LMS.Application.Features.Events.Queries.GetEventsExport;
using JSC_LMS.Application.Features.Institutes.Queries.InstituteFileExport.InstituteCsvExport;
using JSC_LMS.Application.Features.Principal.Queries.PrincipalFileExport.PrincipalCsvExport;
using JSC_LMS.Application.Features.School.Queries.SchoolFileExport.SchoolCsvExport;
using JSC_LMS.Application.Features.Teachers.Queries.TeacherFileExport.TeacherCsvExport;
using System.Collections.Generic;
using System.IO;

namespace JSC_LMS.Infrastructure
{
    public class CsvExporter : ICsvExporter
    {
        public byte[] ExportEventsToCsv(List<EventExportDto> eventExportDtos)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter);
                csvWriter.WriteRecords(eventExportDtos);
            }

            return memoryStream.ToArray();
        }

        public byte[] ExportPrincipalToCsv(List<PrincipalCsvExportDto> principalExportDtos)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter);
                csvWriter.WriteRecords(principalExportDtos);
            }

            return memoryStream.ToArray();
        }

        public byte[] ExportInstituteToCsv(List<InstituteCsvExportDto> instituteExportDtos)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter);
                csvWriter.WriteRecords(instituteExportDtos);
            }

            return memoryStream.ToArray();
        }


        public byte[] ExportSchoolToCsv(List<SchoolCsvExportDto> schoolExportDtos)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter);
                csvWriter.WriteRecords(schoolExportDtos);
            }

            return memoryStream.ToArray();
        }

        public byte[] ExportTeacherToCsv(List<TeacherCsvExportDto> teacherExportDtos)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter);
                csvWriter.WriteRecords(teacherExportDtos);
            }

            return memoryStream.ToArray();
        }

        public byte[] ExportAcademicToCsv(List<AcademicCsvExportDto> academicExportDtos)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter);
                csvWriter.WriteRecords(academicExportDtos);
            }

            return memoryStream.ToArray();
        }
    }
}
