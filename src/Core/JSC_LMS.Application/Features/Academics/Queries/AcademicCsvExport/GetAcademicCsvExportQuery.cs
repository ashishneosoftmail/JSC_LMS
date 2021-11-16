using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Academics.Queries.AcademicCsvExport
{
   public class GetAcademicCsvExportQuery : IRequest<AcademicCsvExportFileVm>
    {
    }
}
