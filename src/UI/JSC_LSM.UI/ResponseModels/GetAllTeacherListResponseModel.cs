using JSC_LMS.Application.Features.Teachers.Queries.GetTeacherList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAllTeacherListResponseModel
    {
        public bool Succeeded { get; set; }
        public bool isSuccess { get; set; }

        public string message { get; set; }
        public string statusCode { get; set; }
        public IEnumerable<GetTeacherListDto> data { get; set; }
    }

}
