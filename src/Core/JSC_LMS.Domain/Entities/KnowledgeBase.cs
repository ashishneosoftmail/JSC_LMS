using JSC_LMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Domain.Entities
{

    public class KnowledgeBase : AuditableEntity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string DocTitle { get; set; }
        public string SubTitle { get; set; }
        public string SlugUrl { get; set; }
        public string AddContent { get; set; }
        public bool IsActive { get; set; }
        public Category Category { get; set; }
    }
}
