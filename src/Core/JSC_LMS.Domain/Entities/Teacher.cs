using JSC_LMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JSC_LMS.Domain.Entities
{
    public class Teacher : AuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserTypeId { get; set; }
        public string TeacherName { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int SubjectId { get; set; }
        public int SchoolId { get; set; }
        public string Mobile { get; set; }
        public string UserId { get; set; }
        public int? CityId { get; set; }
        public int? StateId { get; set; }
        public int? ZipId { get; set; }
        public bool IsActive { get; set; }
        /*    public State State { get; set; }
            public City City { get; set; }
            public Zip Zip { get; set; }
            public Section Section { get; set; }
            public Class Class { get; set; }

            public Subject Subject { get; set; }
            public School School { get; set; }*/
    }
}
