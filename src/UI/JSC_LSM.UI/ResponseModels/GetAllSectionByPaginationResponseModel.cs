using JSC_LMS.Application.Features.Section.Queries.GetSectionByPagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAllSectionByPaginationResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public GetSectionListByPaginationResponse data { get; set; }
    }
}
