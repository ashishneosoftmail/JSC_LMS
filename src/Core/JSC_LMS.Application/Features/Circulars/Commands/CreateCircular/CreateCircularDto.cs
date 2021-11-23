using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.Circulars.Commands.CreateCircular
{
    public class CreateCircularDto
    {
        [Required(ErrorMessage = "School is Required")]
        public int SchoolId { get; set; }
        [Required(ErrorMessage = "Circular Title is Required")]
        [MaxLength(150, ErrorMessage = "Circular Title should be less than 150 characters")]
        public string CircularTitle { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        [MaxLength(200, ErrorMessage = "Description should be less than 200 characters")]

        public string Description { get; set; }

        public string File { get; set; }
        public bool Status { get; set; }
        public bool IsActive { get; set; }
    }
}
