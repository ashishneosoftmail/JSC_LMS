using JSC_LMS.Application.Features.Class.Queries.GetClassByFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAllClassByFiltersResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public IEnumerable<GetClassByFilterDto> data { get; set; }
    }
}
