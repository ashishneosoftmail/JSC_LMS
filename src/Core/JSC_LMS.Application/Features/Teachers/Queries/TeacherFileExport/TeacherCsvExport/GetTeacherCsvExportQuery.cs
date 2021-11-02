using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Teachers.Queries.TeacherFileExport.TeacherCsvExport
{
    public class GetTeacherCsvExportQuery : IRequest<TeacherCsvExportFileVm>
    {
    }
}
