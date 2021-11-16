using JSC_LMS.Application.Features.Students.Queries.GetStudentByPagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAllStudentByPaginationResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public GetStudentListByPaginationResponse data { get; set; }
    }
}
