using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Controllers
{
    public class UserController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddUser()
        {
            return View();
        }
        public IActionResult AddStudent()
        {
            return View();
        }
        public IActionResult AddParents()
        {
            return View();

        }
        public IActionResult ManageTeacherUsers()
        {
            return View();

        }
        public IActionResult AddTeacher()
        {
            return View();

        }
        public IActionResult EditTeacher()
        {
            return View();

        }

    }
}
