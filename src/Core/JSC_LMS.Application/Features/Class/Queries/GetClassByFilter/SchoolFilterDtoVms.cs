using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Class.Queries.GetClassByFilter
{
    public class SchoolFilterDtoVms
    {
        public int Id { get; set; }


        public string ClassName { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public SchoolDtoVm School { get; set; }
        public string SchoolName { get; set; }
    }

    public class SchoolDtoVm
    {
        public string SchoolName { get; set; }
        public int Id { get; set; }
    }
}
