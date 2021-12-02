using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.EventsFeature.Queries.GetAllEventsListByPagination
{
    public class GetAllEventsListByPaginationDto
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public DateTime EventDateTime { get; set; }
        public string EventCoordinator { get; set; }
        public string CoordinatorNumber { get; set; }
        public string EventTitle { get; set; }
        public string Venue { get; set; }
        public bool Status { get; set; }
        public SchoolDto School { get; set; }

    }
    public class SchoolDto
    {
        public int Id { get; set; }
        public string SchoolName { get; set; }
    }
}
