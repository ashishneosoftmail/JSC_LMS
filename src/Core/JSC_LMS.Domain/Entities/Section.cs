using JSC_LMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JSC_LMS.Domain.Entities
{
    public class Section : AuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SchoolId { get; set; }

        public int ClassId { get; set; }

        public string SectionName { get; set; }

        public bool IsActive { get; set; }
        public virtual ICollection<Subject> Subject { get; set; }
        public virtual ICollection<Teacher> Teacher { get; set; }
        public virtual ICollection<Academic> Academic { get; set; }
        public virtual Students Student { get; set; }



    }
}
