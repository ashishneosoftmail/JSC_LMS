using JSC_LMS.Application.Features.KnowledgeBase.Queries.GetKnowledgeBaseByFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAllKnowledgeBaseFilterResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public IEnumerable<GetKnowledgeBaseByFilterDto> data { get; set; }
    }
}
