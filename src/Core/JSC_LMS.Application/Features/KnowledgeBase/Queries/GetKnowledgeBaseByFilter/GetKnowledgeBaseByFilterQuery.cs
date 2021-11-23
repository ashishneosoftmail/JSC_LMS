using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.KnowledgeBase.Queries.GetKnowledgeBaseByFilter
{
    public class GetKnowledgeBaseByFilterQuery : IRequest<Response<IEnumerable<GetKnowledgeBaseByFilterDto>>>
    {
        public GetKnowledgeBaseByFilterQuery(string _DocTitle, string _Subtitle, string _Category)
        {
            DocTitle = _DocTitle;
            Subtitle = _Subtitle;
            Category = _Category;
        }
        public string DocTitle { get; set; }
        public string Subtitle { get; set; }
        public string Category { get; set; }

    }
}
