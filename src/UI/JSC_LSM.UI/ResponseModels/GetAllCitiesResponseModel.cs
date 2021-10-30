﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JSC_LMS.Application.Models.Common;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAllCitiesResponseModel
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public string token { get; set; }
        public string statusCode { get; set; }
       public IEnumerable<CityResponse> data { get; set; }
    }
}
