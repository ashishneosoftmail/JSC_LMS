using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class AcademicViewModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal CutOff { get; set; }
    
        public DateTime? CreatedDate { get; set; }

        public bool IsActive { get; set; }
        public string SchoolName { get; set; }

        public string SubjectName { get; set; }

        public string TeacherName { get; set; }
        public string SectionName { get; set; }
        public string ClassName { get;  set; }
    }
}
