using JSC_LMS.Application.Features.Events.Queries.GetEventsExport;
using System.Collections.Generic;

namespace JSC_LMS.Application.Contracts.Infrastructure
{
    public interface ICsvExporter
    {
        byte[] ExportEventsToCsv(List<EventExportDto> eventExportDtos);
    }
}
