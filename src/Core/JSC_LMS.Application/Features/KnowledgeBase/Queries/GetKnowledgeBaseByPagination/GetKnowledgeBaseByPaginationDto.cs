using JSC_LMS.Application.CommonDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.KnowledgeBase.Queries.GetKnowledgeBaseByPagination
{
    public class GetKnowledgeBaseByPaginationDto
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
