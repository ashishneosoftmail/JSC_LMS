using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Teachers.Queries.TeacherFileExport.TeacherCsvExport
{
    public class SectionDto
    {
        public int Id { get; set; }
        public string SectionName { get; set; }
    }
    public class SubjectDto
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
    }

    public class ClassDto
    {
        public int Id { get; set; }
        public string ClassName { get; set; }


    }
    public class SchoolDto
    {
        public int Id { get; set; }
        public string SchoolName { get; set; }


    }
}
