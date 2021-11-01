using JSC_LSM.UI.Models;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Controllers
{
    public class InstituteController : BaseController
    {
        /* JSC_LSM.UI.Common.Common _common;
        public InstituteController(JSC_LSM.UI.Common.Common common)
        {
            _common = common;
        }*/
        private readonly IStateRepository _stateRepository;
        private readonly JSC_LSM.UI.Common.Common _common;
        public InstituteController(IStateRepository stateRepository, JSC_LSM.UI.Common.Common common)
        {
            _stateRepository = stateRepository;
            _common = common;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InstituteDetails()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddInstitute()
        {
            Institute institute = new Institute();
            institute.States = await _common.GetAllStates();
            return View(institute);
        }
        public async Task<List<SelectListItem>> GetCityByStateId(int id)
        {
            var cities = await _common.GetAllCityByStateId(id);
            return cities;
        }
        public async Task<List<SelectListItem>> GetZipByCityId(int cityId)
        {
            var cities = await _common.GetAllZipByCityId(cityId);
            return cities;
        }
    }
}
