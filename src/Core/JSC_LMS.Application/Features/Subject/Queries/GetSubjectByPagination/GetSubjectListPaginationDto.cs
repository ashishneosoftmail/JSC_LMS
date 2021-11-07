using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Subject.Queries.GetSubjectByPagination
{
    public class GetSubjectListPaginationDto
    {
        public int Id { get; set; }

        public string SubjectName { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }

        public SchoolPaginationDto School { get; set; }

        public ClassPaginationDto Class { get; set; }

        public SectionPaginationDto Section { get; set; }

    }
}
