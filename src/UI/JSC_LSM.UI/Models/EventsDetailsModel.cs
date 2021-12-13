using JSC_LMS.Application.Features.EventsFeature.Commands.CreateEvents;
using JSC_LMS.Application.Features.EventsFeature.Commands.UpdateEvents;
using JSC_LMS.Application.Features.EventsFeature.Queries.GetAllEventsList;
using JSC_LMS.Application.Features.EventsFeature.Queries.GetAllEventsListBySchool;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class EventsDetailsModel
    {
        public AddNewEvents AddNewEvents { get; set; }
        public UpdateEventsById UpdateEventsById { get; set; }
        public List<GetEventsList> GetEventsList { get; set; }
        public List<GetEventsListBySchoolId> GetEventsListBySchoolId { get; set; }
        public List<SelectListItem> Schools { get; set; }
        public string ImageName { get; set; }
        public string ImageFileName { get; set; }

    }
    public class AddNewEvents : CreateEventsDto
    {
        public IFormFile fileUpload { get; set; }
        public IFormFile imageUpload { get; set; }
    }
    public class UpdateEventsById : UpdateEventsDto
    {
        public IFormFile fileUpload { get; set; }
        public IFormFile imageUpload { get; set; }
    }
    public class GetEventsList : GetAllEventsListDto
    {
        public string SchoolName { get; set; }
        public string statusName { get; set; }
    }
    public class GetEventsListBySchoolId : GetAllEventsListBySchoolDto
    {
        public string SchoolName { get; set; }
        public string statusName { get; set; }
    }
}
