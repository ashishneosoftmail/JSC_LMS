using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Circulars.Queries.GetAllCircularByFilterAndSchool
{

    public class GetAllCircularByFilterAndSchoolDto
    {

        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string CircularTitle { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string File { get; set; }
        public bool Status { get; set; }
        public bool IsActive { get; set; }
        public SchoolDto SchoolData { get; set; }
    }
    public class SchoolDto
    {
        public int Id { get; set; }
        public string SchoolName { get; set; }
    }
}
