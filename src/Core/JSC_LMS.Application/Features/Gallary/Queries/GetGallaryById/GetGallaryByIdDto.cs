using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Gallary.Queries.GetGallaryById
{
  public  class GetGallaryByIdDto
    {
        public int Id { get; set; }
        public int EventsTableId { get; set; }

        public int SchoolId { get; set; }

        public string image { get; set; }

        public string FileName { get; set; }
        public string FileType { get; set; }
        public bool IsActive { get; set; }
        public SchoolDto SchoolData { get; set; }
        public EventsTableDto EventsData { get; set; }
    }

    public class SchoolDto
    {
        public int Id { get; set; }
        public string SchoolName { get; set; }
    }

    public class EventsTableDto
    {
        public int Id { get; set; }
        public string EventTitle { get; set; }
    }

}
