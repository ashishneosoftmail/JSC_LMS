using JSC_LMS.Application.Features.KnowledgeBase.Queries.GetKnowledgeBaseByPagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAllKnowledgeBasePaginationResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public IEnumerable<GetKnowledgeBaseByPaginationDto> data { get; set; }
    }
}
