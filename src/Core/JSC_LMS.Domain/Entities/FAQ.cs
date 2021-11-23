using JSC_LMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JSC_LMS.Domain.Entities
{
  public  class FAQ : AuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string FAQTitle { get; set; }
        public string Question { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; }
        public Category Category { get; set; }
    }
}
