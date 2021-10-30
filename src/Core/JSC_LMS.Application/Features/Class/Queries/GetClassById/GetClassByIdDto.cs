using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Class.Queries.GetClassById
{
   public class GetClassByIdDto
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public SchoolDto School { get; set; }
    }
}
