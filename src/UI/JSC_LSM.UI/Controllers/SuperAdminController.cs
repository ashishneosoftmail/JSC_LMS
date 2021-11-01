using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Controllers
{
    public class SuperAdminController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
       
    }
}
