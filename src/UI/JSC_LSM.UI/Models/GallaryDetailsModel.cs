using JSC_LMS.Application.Features.Gallary.Commands.UploadImage;
using JSC_LMS.Application.Features.Gallary.Queries.GetAllGallaryList;
using JSC_LMS.Application.Features.Gallary.Queries.GetGallaryById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class GallaryDetailsModel
    {
        public AddGallary AddGallary { get; set; }
     
        public List<GetGallaryList> GetGallaryList { get; set; }
        public List<GetGallaryListById> GetGallaryListById { get; set; }
        public List<SelectListItem> Schools { get; set; }
        public List<SelectListItem> Events { get; set; }
        public List<SelectListItem> FileType { get; set; }
        public string ImageName { get; set; }
        public string ImageFileName { get; set; }
    }
    public class AddGallary : UploadImageDto
    {
        public IFormFile imageUpload { get; set; }
    }
    public class GetGallaryList : GetAllGallaryListDto
    {
        public IFormFile imageUpload { get; set; }
        public string EventTitle { get; set; }
    }
    public class GetGallaryListById : GetGallaryByIdDto
    {
  
    }

}
