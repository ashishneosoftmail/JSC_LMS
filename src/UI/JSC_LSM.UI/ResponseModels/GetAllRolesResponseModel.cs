﻿using JSC_LMS.Application.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAllRolesResponseModel
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public string token { get; set; }
        public string statusCode { get; set; }
        public IEnumerable<RolesResponse> data { get; set; }
    }
}
