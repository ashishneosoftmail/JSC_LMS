using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class SuperadminChartDetails
    {
        public int InstituteCount { get; set; }
        public int KnowledgebaseCount { get; set; }
        public int FAQCount { get; set; }
       
    }
    public class UserData
    {
        public int UserCount { get; set; }
        public string Month { get; set; }
    }
    class UserDataSortByMonth : IComparer<UserData>
    {
        public int Compare(UserData x, UserData y)
        {
            return Convert.ToInt32(x.Month).CompareTo(Convert.ToInt32(y.Month));
        }
    }
}
