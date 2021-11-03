using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Subject.Queries.GetSubjectFilter
{
    public class SchoolDto
    {
        public int Id { get; set; }
        public string SchoolName { get; set; }
    }
    public class ClassDto
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
    }
    public class SectionDto
    {
        public int Id { get; set; }
        public string SectionName { get; set; }
    }
}
