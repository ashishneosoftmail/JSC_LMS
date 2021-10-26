using JSC_LMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JSC_LMS.Domain.Entities
{
    public class Role : AuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
    }
}
