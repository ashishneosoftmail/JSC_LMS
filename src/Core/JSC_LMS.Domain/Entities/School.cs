using JSC_LMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JSC_LMS.Domain.Entities
{
    public class School : AuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string SchoolName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }        
        public int? CityId { get; set; }
        public int? StateId { get; set; }
        public int? ZipId { get; set; }
        public string Mobile { get; set; }
        public bool IsActive { get; set; }
        public Institute Institute { get; set; }
        public State State { get; set; }
        public Zip Zip { get; set; }
        public ICollection<Class> Class { get; set; }
        public City City { get; set; }
        public virtual ICollection<Section> Section { get; set; }
        public virtual ICollection<Subject> Subject { get; set; }
        public virtual ICollection<Teacher> Teacher { get; set; }
        public virtual ICollection<Academic> Academic { get; set; }
        public virtual ICollection<Circular> Circular { get; set; }
        public Principal Principal { get; set; }       
        public string Name { get; set; }
        public virtual ICollection<Announcement> Announcement { get; set; }
        public ICollection<Students> Students { get; set; }
        public ICollection<Parents> Parents { get; set; }
        public virtual ICollection<EventsTable> EventsTable { get; set; }

        public virtual ICollection<Gallary> Gallary { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
    }
}
