using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Institutes.Queries.InstituteFileExport.InstituteCsvExport
{
    public class GetInstituteCsvExportQuery : IRequest<InstituteCsvExportFileVm>
    {
    }
}
