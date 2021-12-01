using JSC_LMS.Application.Features.School.Queries.GetSchoolList;
using JSC_LMS.Application.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAllSchoolResponseModel
    {
        public bool isSuccess { get; set; }
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string token { get; set; }
        public string statusCode { get; set; }

        public IEnumerable<GetSchoolListDto> data { get; set; }
        public IEnumerable<SchoolResponse> Data{ get; set; }
    }
}
