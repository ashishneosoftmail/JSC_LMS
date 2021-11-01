using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Class.Queries.GetClassByFilter
{
   public class GetClassByFilterDto
    {
       
        public string ClassName { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
       public string SchoolName { get; set; }

        public SchoolFilterDtoVms School { get; set; }

    }
}
