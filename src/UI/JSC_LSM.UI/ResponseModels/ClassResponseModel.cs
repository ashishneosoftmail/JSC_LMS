using JSC_LMS.Application.Features.Class.Commands.CreateClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class ClassResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public CreateClassDto data { get; set; }
    }
}
