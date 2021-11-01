using JSC_LMS.Application.Features.Events.Queries.GetEventsExport;
using JSC_LMS.Application.Features.Institutes.Queries.InstituteFileExport.InstituteCsvExport;
using JSC_LMS.Application.Features.Principal.Queries.PrincipalFileExport.PrincipalCsvExport;
using JSC_LMS.Application.Features.School.Queries.SchoolFileExport.SchoolCsvExport;
using System.Collections.Generic;

namespace JSC_LMS.Application.Contracts.Infrastructure
{
    public interface ICsvExporter
    {
        byte[] ExportEventsToCsv(List<EventExportDto> eventExportDtos);
        byte[] ExportPrincipalToCsv(List<PrincipalCsvExportDto> principalExportDtos);
        byte[] ExportInstituteToCsv(List<InstituteCsvExportDto> instituteExportDtos);

        byte[] ExportSchoolToCsv(List<SchoolCsvExportDto> schoolExportDtos);
    }
}
