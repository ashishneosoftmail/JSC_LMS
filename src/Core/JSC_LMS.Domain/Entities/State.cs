using JSC_LMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JSC_LMS.Domain.Entities
{
    public class State : AuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string StateName { get; set; }
        public bool IsActive { get; set; }
        public ICollection<City> City { get; set; }
        public virtual ICollection<Institute> Institute { get; set; }
    }
}
