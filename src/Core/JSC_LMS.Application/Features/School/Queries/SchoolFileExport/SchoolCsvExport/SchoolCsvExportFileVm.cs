namespace JSC_LMS.Application.Features.School.Queries.SchoolFileExport.SchoolCsvExport
{
    public class SchoolCsvExportFileVm
    {
        public string SchoolExportFileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}