using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Class.Queries.GetClassByPagination
{
    public class GetClassListPaginationDto
    {

        public int Id { get; set; }
    
        public string ClassName { get; set; }
       
        public DateTime CreatedDate { get; set; }
      
        public bool IsActive { get; set; }
    
        public SchoolPaginationDto School { get; set; }
    }
}
