using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Class.Commands.UpdateClass
{
   public class UpdateClassDto
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }

        public string ClassName { get; set; }
        public bool IsActive { get; set; }
    }
}
