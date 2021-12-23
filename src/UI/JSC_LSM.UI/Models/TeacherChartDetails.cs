using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class TeacherChartDetails
    {
        public int AnnouncementCount { get; set; }
    }
    public class AnnouncementPrincipalData
    {
        public int AnnouncementChartCount { get; set; }
        public string Month { get; set; }
    }
    class AnnouncementDataSortByMonth : IComparer<AnnouncementPrincipalData>
    {
        public int Compare(AnnouncementPrincipalData x, AnnouncementPrincipalData y)
        {
            return Convert.ToInt32(x.Month).CompareTo(Convert.ToInt32(y.Month));
        }
    }
}
