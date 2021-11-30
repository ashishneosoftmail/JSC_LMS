using JSC_LMS.Application.Features.School.Queries.GetSchoolList;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Models.School
{
    public class SchoolResponse
    {
        public string Id { get; set; }
        public string SchoolName { get; set; }
        public InstituteDto Institute { get; set; }
    }
}
