using JSC_LMS.Application.Features.ParentsFeature.Commands.CreateParents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class ParentsResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public CreateParentsDto data { get; set; }
    }
}
