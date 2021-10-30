using JSC_LMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JSC_LMS.Domain.Entities
{
    public class City : AuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CityName { get; set; }

        public bool IsActive { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
        public virtual ICollection<Zip> Zip { get; set; }
        public ICollection<Institute> Institute { get; set; }
        public virtual ICollection<School> School { get; set; }
        public virtual ICollection<Teacher> Teacher { get; set; }
        public ICollection<Principal> Principal { get; set; }
    }
}
