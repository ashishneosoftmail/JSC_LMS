using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Principal.Queries.PrincipalFileExport.PrincipalCsvExport
{
    public class GetPrincipalCsvExportQuery : IRequest<PrincipalCsvExportFileVm>
    {
    }
}
