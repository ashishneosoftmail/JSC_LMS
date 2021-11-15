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
        public virtual KnowledgeBase KnowledgeBase { get; set; }

    }
}
