using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class StudentChartDetails
    {
        public int AnnouncementCount { get; set; }
        public int EventsCount { get; set; }
        public int CircularCount { get; set; }
    }
    public class EventsStudentData
    {
        public int EventsStudentsCount { get; set; }
        public string Month { get; set; }
    }
    class EventsStudentsDataSortByMonth : IComparer<EventsStudentData>
    {
        public int Compare(EventsStudentData x, EventsStudentData y)
        {
            return Convert.ToInt32(x.Month).CompareTo(Convert.ToInt32(y.Month));
        }
    }
}
