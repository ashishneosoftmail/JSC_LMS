using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.ParentsFeature.Queries.GetParentByUserId
{
    public class GetParentByUserIdDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public int SchoolId { get; set; }
        public int Classid { get; set; }
        public int Sectionid { get; set; }
    }
}
