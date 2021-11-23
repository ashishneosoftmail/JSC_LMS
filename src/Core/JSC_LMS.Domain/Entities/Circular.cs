using JSC_LMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JSC_LMS.Domain.Entities
{
    public class Circular : AuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string CircularTitle { get; set; }
        public string Description { get; set; }

        public string File { get; set; }
        public bool Status { get; set; }
        public bool IsActive { get; set; }
        public School School { get; set; }
    }
}
