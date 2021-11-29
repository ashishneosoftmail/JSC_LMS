using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Students.Queries.GetStudentByUserId
{
    public class GetStudentByUserIdDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public int Schoolid { get; set; }
        public int Classid { get; set; }
        public int Sectionid { get; set; }
    }
}
