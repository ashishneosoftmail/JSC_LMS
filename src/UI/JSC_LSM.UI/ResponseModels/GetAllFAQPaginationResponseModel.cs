using JSC_LMS.Application.Features.FAQ.Queries.GetFAQByPagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAllFAQPaginationResponseModel
    {

        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public IEnumerable<GetFAQByPaginationDto> data { get; set; }
    }
}
