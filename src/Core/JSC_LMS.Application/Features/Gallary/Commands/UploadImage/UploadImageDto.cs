using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Gallary.Commands.UploadImage
{
   public class UploadImageDto
    {
      
       public int Id { get; set; }
        public int EventsTableId { get; set; }

        public string image { get; set; }

        public string FileName { get; set; }
        public string FileType { get; set; }
        public bool IsActive { get; set; }

    }
}
