using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Principal.Queries.PrincipalFileExport.PrincipalCsvExport
{
    public class PrincipalCsvExportFileVm
    {
        public string PrincipalExportFileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}
