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
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Mobile { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string CityName { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string UserType { get; set; }

    }
}
