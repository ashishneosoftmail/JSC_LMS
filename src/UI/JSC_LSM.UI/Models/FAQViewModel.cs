using JSC_LMS.Application.Features.Categories.Commands.CreateCateogry;
using JSC_LMS.Application.Features.FAQ.Commands.CreateFAQ;
using JSC_LMS.Application.Features.FAQ.Commands.UpdateFAQ;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class FAQViewModel
    {
        public AddCategory AddCategory { get; set; }
        public AddFAQ AddFAQ { get; set; }
        public UpdateFAQ UpdateFAQ { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
 
    public class AddFAQ : CreateFAQDto
    {
     
    }
    public class UpdateFAQ : UpdateFAQDto
    {

    }
    public class FAQList
    {
        public int Id { get; set; }
        public string FAQTitle { get; set; }
        public string Question { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; }
        public string CategoryName { get; set; }
    }
}
