using JSC_LMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAllCategoryResponseModel
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public string token { get; set; }
        public string statusCode { get; set; }
        public IEnumerable<Category> data { get; set; }
    }
}
