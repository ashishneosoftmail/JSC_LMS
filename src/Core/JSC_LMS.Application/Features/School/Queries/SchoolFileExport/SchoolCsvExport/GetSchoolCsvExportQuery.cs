using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.School.Queries.SchoolFileExport.SchoolCsvExport
{
    public class GetSchoolCsvExportQuery :IRequest<SchoolCsvExportFileVm>
    {
    }
}
