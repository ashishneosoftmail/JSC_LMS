using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Common.Categories.Commands
{
    public class CreateCategoryDto
    {
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
    }
}
