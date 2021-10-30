using JSC_LMS.Application.Features.Events.Queries.GetEventsExport;
using JSC_LMS.Application.Features.Principal.Queries.PrincipalFileExport.PrincipalCsvExport;
using System.Collections.Generic;

namespace JSC_LMS.Application.Contracts.Infrastructure
{
    public interface ICsvExporter
    {
        byte[] ExportEventsToCsv(List<EventExportDto> eventExportDtos);
        byte[] ExportPrincipalToCsv(List<PrincipalCsvExportDto> principalExportDtos);
    }
}
