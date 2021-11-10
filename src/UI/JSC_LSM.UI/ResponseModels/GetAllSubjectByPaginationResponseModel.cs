using JSC_LMS.Application.Features.Subject.Queries.GetSubjectByPagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAllSubjectByPaginationResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public GetSubjectByPaginationResponse data { get; set; }
    }
}
