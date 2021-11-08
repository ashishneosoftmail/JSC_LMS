using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Principal.Queries.PrincipalFileExport.PrincipalExcelExport
{
    public class PrincipalExcelExportFileVm
    {
        public string PrincipalExportFileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}
