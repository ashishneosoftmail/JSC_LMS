using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.FAQ.Queries.GetFAQByFilter
{
   public class GetFAQByFilterQuery : IRequest<Response<IEnumerable<GetFAQByFilterDto>>>
    {
        public GetFAQByFilterQuery(string _FAQTitle, bool _IsActive, string _Category)
        {
            FAQTitle = _FAQTitle;
            IsActive = _IsActive;
            Category = _Category;
        }
        public string FAQTitle { get; set; }
        public bool IsActive { get; set; }
        public string Category { get; set; }
    }
}
