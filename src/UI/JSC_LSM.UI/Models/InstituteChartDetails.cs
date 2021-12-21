using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class InstituteChartDetails
    {
        public int PrincipalCount { get; set; }
        public int SchoolCount { get; set; }
        public int EventsCount { get; set; }
       
    }
    public class EventsData
    {
        public int EventsChartCount { get; set; }
        public string Month { get; set; }
    }
    class EventsDataSortByMonth : IComparer<EventsData>
    {
        public int Compare(EventsData x, EventsData y)
        {
            return Convert.ToInt32(x.Month).CompareTo(Convert.ToInt32(y.Month));
        }
    }

}
