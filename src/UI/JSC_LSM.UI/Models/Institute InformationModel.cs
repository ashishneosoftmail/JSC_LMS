using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class Institute_InformationModel
    {
        public string InstituteName { get; set; }
        public int LicensePeriod { get; set; }
        public DateTime LicenseExpiryDate { get; set; }
        public int NoOfSchools { get; set; }
    }
}
