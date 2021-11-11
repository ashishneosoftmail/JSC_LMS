using JSC_LMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Domain.Entities
{
    public class Superadmin : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MobileSupport { get; set; }
        public string EmailSupport { get; set; }
        public string Logo { get; set; }
        public string LoginImage { get; set; }
        public string UserId { get; set; }
        public bool IsActive { get; set; }
    }
}
