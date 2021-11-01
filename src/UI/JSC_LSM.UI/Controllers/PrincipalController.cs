using JSC_LSM.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Controllers
{
    public class PrincipalController : BaseController
    {
        private readonly JSC_LSM.UI.Common.Common _common;
        public PrincipalController(JSC_LSM.UI.Common.Common common)
        {
            _common = common;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddPrincipal()
        {
            PrincipalModel principalModel = new PrincipalModel();
            principalModel.States = await _common.GetAllStates();
            return View(principalModel);
        }
        public async Task<List<SelectListItem>> GetCityByStateId(int id)
        {
            var cities = await _common.GetAllCityByStateId(id);
            return cities;
        }
        public async Task<List<SelectListItem>> GetZipByCityId(int cityId)
        {
            var zip = await _common.GetAllZipByCityId(cityId);
            return zip;
        }
    }
}
