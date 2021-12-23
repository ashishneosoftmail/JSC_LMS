using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.Gallary.Commands.UploadImage
{
   public class UploadImageDto
    {

        [Required(ErrorMessage = "Event Name is mandatory")]
        public int EventsTableId { get; set; }
        [Required(ErrorMessage = "School Name is mandatory")]
        public int SchoolId { get; set; }

      
        public string image { get; set; }

  
        public string FileName { get; set; }
        [Required(ErrorMessage = "FileType  is mandatory")]
        public string FileType { get; set; }
        public bool IsActive { get; set; }

    }
}
