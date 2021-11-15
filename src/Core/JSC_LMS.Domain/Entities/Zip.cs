using JSC_LMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Domain.Entities
{
    public class Zip : AuditableEntity
    {
        public int Id { get; set; }
        public string Zipcode { get; set; }
        public int CityId { get; set; }
        public bool IsActive { get; set; }
        public City City { get; set; }
        public ICollection<Institute> Institute { get; set; }
        public virtual ICollection<School> School { get; set; }
        public virtual ICollection<Teacher> Teacher { get; set; }
        public ICollection<Principal> Principal { get; set; }
        public ICollection<Students> student { get; set; }

    }
}
