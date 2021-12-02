using JSC_LMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JSC_LMS.Domain.Entities
{
   public class Gallary:AuditableEntity
    {
        public int Id { get; set; }
        public int  EventsTableId { get; set; }

        public int SchoolId { get; set; }
        public EventsTable EventsTable { get; set; }
        public School School { get; set; }
        public string image { get; set; }

        public string FileName { get; set; }
        public string FileType { get; set; }

   
        public bool IsActive { get; set; }




    }
}
