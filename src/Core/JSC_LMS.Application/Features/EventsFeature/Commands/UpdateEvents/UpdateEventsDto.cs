using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.EventsFeature.Commands.UpdateEvents
{
    public class UpdateEventsDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "School is required")]
        public int SchoolId { get; set; }
        [Required(ErrorMessage = "Date and time of event is required")]
        public DateTime EventDateTime { get; set; }
        [Required(ErrorMessage = "Event Coordinator's Name is required")]
        [MaxLength(150, ErrorMessage = "Event Coordinator's Name should be less than 100 characters")]
        public string EventCoordinator { get; set; }
        [Required(ErrorMessage = "Event Coordinator's phone number is required")]
        [RegularExpression(@"[6-9]\d{9}", ErrorMessage = "Please enter correct mobile number")]
        public string CoordinatorNumber { get; set; }
        [Required(ErrorMessage = "Event's Title is Required")]
        [MaxLength(150, ErrorMessage = "Event's Title should be less than 150 characters")]
        public string EventTitle { get; set; }
        [Required(ErrorMessage = "Venue of the event is Required")]
        [MaxLength(150, ErrorMessage = "Venue of the event should be less than 100 characters")]
        public string Venue { get; set; }
        public bool Status { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        [MaxLength(200, ErrorMessage = "Description should be less than 200 characters")]
        public string Description { get; set; }
        public string File { get; set; }
        public string Image { get; set; }

    }
}
