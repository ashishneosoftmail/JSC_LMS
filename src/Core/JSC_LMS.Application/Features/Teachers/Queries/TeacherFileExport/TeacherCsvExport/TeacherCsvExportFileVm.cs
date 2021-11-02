using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Teachers.Queries.TeacherFileExport.TeacherCsvExport
{
    public class TeacherCsvExportFileVm
    {
        public string TeacherExportFileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}
