using JSC_LMS.Application.Features.School.Queries.GetSchoolByPagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels.SchoolResponseModels
{
    public class GetAllSchoolByPaginationResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public GetSchoolByPaginationResponse data { get; set; }
    }
}
