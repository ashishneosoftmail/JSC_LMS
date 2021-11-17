using JSC_LMS.Application.Features.KnowledgeBase.Queries.GetKnowledgeBaseList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAllKnowledgeBaseListResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public IEnumerable<GetKnowledgeBaseListDto> data { get; set; }
    }
}
