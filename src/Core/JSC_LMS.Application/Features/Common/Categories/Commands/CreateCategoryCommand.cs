using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Common.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<Response<int>>
    {
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
    }
}
