using JSC_LMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
#region-class to generate the attributes of the Institute table: by Shivani Goswami
namespace JSC_LMS.Domain.Entities
{  
    public class Institute : AuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string InstituteName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ContactPerson { get; set; }
        public string Mobile { get; set; }
        public string UserId { get; set; }
        public int? CityId { get; set; }
        public int? StateId { get; set; }
        public int? ZipId { get; set; }
        public DateTime LicenseExpiry { get; set; }
        public string InstituteURL { get; set; }
        public bool IsActive { get; set; }

        public City City { get; set; }
        public State State { get; set; }
        public Zip Zip { get; set; }
        public virtual ICollection<School> School { get; set; }
    }
}
#endregion