using CsvHelper;
using JSC_LMS.Application.Contracts.Infrastructure;
using JSC_LMS.Application.Features.Events.Queries.GetEventsExport;
using JSC_LMS.Application.Features.Institutes.Queries.InstituteFileExport.InstituteCsvExport;
using JSC_LMS.Application.Features.Principal.Queries.PrincipalFileExport.PrincipalCsvExport;
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
    }
}
