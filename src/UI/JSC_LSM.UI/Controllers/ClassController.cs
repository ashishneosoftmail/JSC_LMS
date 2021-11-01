using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Controllers
{
    public class ClassController : BaseController
    {
        public IActionResult AddClass()
        {
            return View();
        }

        public IActionResult ManageClass()
        {
            return View();
        }

    }
}
