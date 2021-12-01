using JSC_LMS.Application.Features.Students.Queries.GetStudentListByPaginationBySchool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAllStudentListBySchoolPaginationResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public IEnumerable<GetStudentListByPaginationBySchoolDto> data { get; set; }
    }
}
