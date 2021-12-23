using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class PrincipalChartDetails
    {
        public int ClassCount { get; set; }
        public int SectionCount { get; set; }
        public int SubjectCount { get; set; }
        public int TeacherCount { get; set; }
        public int StudentCount { get; set; }
        public int ParentCount { get; set; }
        public int AcedemicCount { get; set; }
        public int AnnouncementCount { get; set; }
        public int EventsCount { get; set; }
    }
    public class EventsPrincipalData
    {
        public int EventsChartCount { get; set; }
        public string Month { get; set; }
    }
    public class ClassStudentsData
    {
        public int StudentsPieCount { get; set; }
        public string ClassName { get; set; }
    }
    class EventsPrincipalDataSortByMonth : IComparer<EventsPrincipalData>
    {
        public int Compare(EventsPrincipalData x, EventsPrincipalData y)
        {
            return Convert.ToInt32(x.Month).CompareTo(Convert.ToInt32(y.Month));
        }
    }
}
