using JSC_LMS.Application.Features.Institutes.Commands.CreateInstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class InstituteResponseModel
    {
        public bool isSuccess { get; set; }       
        public string message { get; set; }       
        public string statusCode { get; set; }
        public CreateInstituteDto createInstituteDto { get; set; }

    }
}
