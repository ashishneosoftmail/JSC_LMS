using JSC_LMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Domain.Entities
{
    public class Students : AuditableEntity
    {
        public int Id { get; set; }
        public string UserType { get; set; }
        public string StudentName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public string Mobile { get; set; }
        public string UserId { get; set; }
        public int? CityId { get; set; }
        public int? StateId { get; set; }
        public int? ZipId { get; set; }
        public bool IsActive { get; set; }
        public Class Class { get; set; }
        public Section Section { get; set; }
        public City City { get; set; }
        public State State { get; set; }
        public Zip Zip { get; set; }
        public int SchoolId { get; set; }
        public School SchoolData { get; set; }

        public virtual ICollection<Feedback> Feedback { get; set; }
    }
}
