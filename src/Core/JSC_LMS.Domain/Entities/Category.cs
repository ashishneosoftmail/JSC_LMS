using JSC_LMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JSC_LMS.Domain.Entities
{
    public class Category : AuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<KnowledgeBase> KnowledgeBase { get; set; }
        public virtual ICollection<FAQ> FAQ { get; set; }

    }
}
