using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.Common.Categories.Commands
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "Category Name is mandatory")]
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
    }
}
