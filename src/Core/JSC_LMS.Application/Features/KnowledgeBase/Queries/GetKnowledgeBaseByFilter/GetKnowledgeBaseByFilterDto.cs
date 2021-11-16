using JSC_LMS.Application.CommonDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.KnowledgeBase.Queries.GetKnowledgeBaseByFilter
{
    public class GetKnowledgeBaseByFilterDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string DocTitle { get; set; }
        public string SubTitle { get; set; }
        public string SlugUrl { get; set; }
        public string AddContent { get; set; }
        public bool IsActive { get; set; }
        public CategoriesDto Category { get; set; }
    }
}
