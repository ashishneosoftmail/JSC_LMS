using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Controllers
{
    public class InstituteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InstituteDetails()
        {
            return View();
        }
        public IActionResult AddInstitute()
        {
            return View();
        }
    }
}
