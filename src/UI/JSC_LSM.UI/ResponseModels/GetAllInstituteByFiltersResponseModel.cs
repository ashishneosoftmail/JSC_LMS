using JSC_LMS.Application.Features.Institutes.Queries.GetInstituteFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAllInstituteByFiltersResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public IEnumerable<GetInstituteFilterVm> data { get; set; }
    }
}
