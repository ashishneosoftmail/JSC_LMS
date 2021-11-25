using JSC_LMS.Application.Features.Circulars.Queries.GetAllCircularByFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAllCircularByFilterInstituteAdminResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public IEnumerable<GetAllCircularByFiltersDto> data { get; set; }
    }
}
