using JSC_LMS.Application.CommonDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.FAQ.Queries.GetFAQList
{
    public class GetFAQListDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string FAQTitle { get; set; }
        public string Question { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; }
        public CategoriesDto Category { get; set; }
    }
}
