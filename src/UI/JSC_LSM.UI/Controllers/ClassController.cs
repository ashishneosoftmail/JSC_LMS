using JSC_LMS.Application.Contracts.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Controllers
{
    public class ClassController : BaseController
    {
        private readonly JSC_LSM.UI.Common.Common _common;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IClassRepository _classRepository;

        public ClassController(JSC_LSM.UI.Common.Common common, ISchoolRepository schoolRepository, IClassRepository classRepository)
        {
            _common = common;
            _schoolRepository = schoolRepository;
            _classRepository = classRepository;
        }



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