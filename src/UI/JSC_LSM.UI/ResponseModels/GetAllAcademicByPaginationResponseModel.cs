using JSC_LMS.Application.Features.Academics.Queries.GetAcademicByPagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAllAcademicByPaginationResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public GetAcademicListByPaginationResponse data { get; set; }
    }
}
