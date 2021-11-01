using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Controllers
{
    public class School : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddSchool()
        {
            return View();
        }
    }
}
