using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.Circulars.Commands.UpdateCircular
{
    public class UpdateCircularDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "School is mandatory")]
        public int SchoolId { get; set; }
        [Required(ErrorMessage = "Circular Title is mandatory")]
        [MaxLength(150, ErrorMessage = "Circular Title should be less than 150 characters")]
        public string CircularTitle { get; set; }
        [Required(ErrorMessage = "Description is mandatory")]
        [MaxLength(200, ErrorMessage = "Description should be less than 200 characters")]

        public string Description { get; set; }
        public string File { get; set; }
        public bool Status { get; set; }
        public bool IsActive { get; set; }
    }
}
