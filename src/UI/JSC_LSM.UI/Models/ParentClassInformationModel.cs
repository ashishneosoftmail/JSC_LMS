using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class ParentClassInformationModel
    {
        public string StudentName { get; set; }
        public string SchoolName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public List<SubjectTeacher> SubjectName { get; set; }
        public string PrincipalName { get; set; }
    }
    public class SubjectTeacher
    {
        public string SubjectName { get; set; }
        public string TeacherSubjectName { get; set; }
    }
}
