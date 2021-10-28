﻿using JSC_LMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JSC_LMS.Domain.Entities
{
   public class Class: AuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
   
        public int SchoolId { get; set; }
    
        public string ClassName { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }

    }
}