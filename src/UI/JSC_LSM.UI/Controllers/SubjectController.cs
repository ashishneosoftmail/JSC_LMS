using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Controllers
{
    public class SubjectController : BaseController
    {
        public IActionResult AddSubject()
        {
            return View();
        }
        public IActionResult ManageSubject()
        {
            return View();
        }
    }
}
