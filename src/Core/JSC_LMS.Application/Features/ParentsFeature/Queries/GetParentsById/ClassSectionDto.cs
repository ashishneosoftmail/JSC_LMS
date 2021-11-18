using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.ParentsFeature.Queries.GetParentsById

{
    public class SectionDto
    {
        public int Id { get; set; }
        public string SectionName { get; set; }
    }
    public class ClassDto
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
    }
    public class StudentDto
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
    }
}
