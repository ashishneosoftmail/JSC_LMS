﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class SubjectDetailsViewModel
    {
        public int Id { get; set; }

        public string SubjectName { get; set; }

        public string ClassName { get; set; }

        public DateTime CreatedDate { get; set; }


        public bool IsActive { get; set; }

        public string SchoolName { get; set; }

        public string SectionName { get; set; }
    }
}
