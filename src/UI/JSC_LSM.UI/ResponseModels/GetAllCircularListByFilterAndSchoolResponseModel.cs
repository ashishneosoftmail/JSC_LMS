using JSC_LMS.Application.Features.Circulars.Queries.GetAllCircularByFilterAndSchool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAllCircularListByFilterAndSchoolResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public IEnumerable<GetAllCircularByFilterAndSchoolDto> data { get; set; }
    }
}
