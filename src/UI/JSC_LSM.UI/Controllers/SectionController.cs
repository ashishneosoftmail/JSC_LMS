﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Controllers
{
    public class SectionController : BaseController
    {
        public IActionResult AddSection()
        {
            return View();
        }
        public IActionResult ManageSection()
        {
            return View();
        }
    }
}
