using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Institutes.Queries.InstituteFileExport.InstituteCsvExport
{
   public class InstituteCsvExportFileVm
    {
        public string InstituteExportFileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }

    }
}
