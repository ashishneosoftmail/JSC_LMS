using JSC_LMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Domain.Entities
{
    public class EventsTable : AuditableEntity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int SchoolId { get; set; }
        public DateTime EventDateTime { get; set; }
        public string EventCoordinator { get; set; }
        public string CoordinatorNumber { get; set; }
        public string EventTitle { get; set; }
        public string Venue { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public string File { get; set; }
        public string Image { get; set; }
        public School School { get; set; }

        public virtual ICollection<Gallary> Gallary { get; set; }
    }
}
