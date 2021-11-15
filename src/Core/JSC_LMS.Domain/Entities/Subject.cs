using JSC_LMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JSC_LMS.Domain.Entities
{
    public class Subject : AuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public string SubjectName { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Teacher> Teacher { get; set; }
        public virtual ICollection<Academics> Academics { get; set; }

    }
}
