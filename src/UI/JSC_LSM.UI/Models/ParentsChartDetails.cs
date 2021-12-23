using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class ParentsChartDetails
    {
        public int AnnouncementCount { get; set; }
        public int EventsCount { get; set; }
        public int CircularCount { get; set; }
    }
    public class EventsParentsData
    {
        public int EventsParentsCount { get; set; }
        public string Month { get; set; }
    }
    class EventsParentsDataSortByMonth : IComparer<EventsParentsData>
    {
        public int Compare(EventsParentsData x, EventsParentsData y)
        {
            return Convert.ToInt32(x.Month).CompareTo(Convert.ToInt32(y.Month));
        }
    }
}
