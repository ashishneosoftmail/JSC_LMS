using JSC_LMS.Application.CommonDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.ParentsFeature.Queries.GetParentsList
{
    public class GetParentsListDto
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string ParentName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
