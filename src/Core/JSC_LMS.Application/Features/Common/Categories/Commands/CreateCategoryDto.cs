using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.Common.Categories.Commands
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "Category Name is Required")]
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
    }
}
