using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Academics.Queries.AcademicCsvExport
{
   public class AcademicCsvExportDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal CutOff { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public SchoolDto School { get; set; }
        public ClassDto Class { get; set; }
        public SectionDto Section { get; set; }
        public SubjectDto Subject { get; set; }
        public TeacherDto Teacher { get; set; }
    }
}
