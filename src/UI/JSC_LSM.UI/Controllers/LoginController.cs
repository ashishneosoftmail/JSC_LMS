using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Controllers
{
    public class LoginController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

       
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
