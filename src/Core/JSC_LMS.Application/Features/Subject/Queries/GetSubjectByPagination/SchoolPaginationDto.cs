using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Subject.Queries.GetSubjectByPagination
{
    public class SchoolPaginationDto
    {
        public int Id { get; set; }
        public string SchoolName { get; set; }
    }

    public class ClassPaginationDto
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
    }

    public class SectionPaginationDto
    {
        public int Id { get; set; }
        public string SectionName { get; set; }
    }
}
