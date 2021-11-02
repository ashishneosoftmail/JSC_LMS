using JSC_LMS.Application.Features.Institutes.Queries.GetInstituteList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAllInstituteListResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public IEnumerable<GetInstituteListVm> data { get; set; }
    }
}
