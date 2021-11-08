using JSC_LMS.Application.Features.Institutes.Queries.GetInstituteByPagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAllInstituteByPaginationResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public GetInstituteListByPaginationResponse data { get; set; }
    }
}
