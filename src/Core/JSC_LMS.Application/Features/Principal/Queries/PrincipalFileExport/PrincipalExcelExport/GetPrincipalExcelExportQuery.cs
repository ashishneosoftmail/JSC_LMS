using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Principal.Queries.PrincipalFileExport.PrincipalExcelExport
{
    public class GetPrincipalExcelExportQuery : IRequest<PrincipalExcelExportFileVm>
    {
    }
}
