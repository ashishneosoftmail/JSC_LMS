using JSC_LMS.Application.Features.Principal.Queries.GetPrincipalByPagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAllPrincipalByPaginationResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public GetPrincipalListByPaginationResponse data { get; set; }
    }
}
