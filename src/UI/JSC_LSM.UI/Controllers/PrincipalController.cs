using JSC_LMS.Application.Features.Principal.Commands.CreatePrincipal;
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
    public class PrincipalController : BaseController
    {
        private readonly JSC_LSM.UI.Common.Common _common;
        private readonly ISchoolRepository _schoolRepository;
        public PrincipalController(JSC_LSM.UI.Common.Common common, ISchoolRepository schoolRepository)
        {
            _common = common;
            _schoolRepository = schoolRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddPrincipal()
        {
            ViewBag.AddPrincipalSuccess = null;
            ViewBag.AddPrincipalError = null;
            PrincipalModel principalModel = new PrincipalModel();
            principalModel.States = await _common.GetAllStates();
            principalModel.Schools = await _common.GetSchool();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPrincipal(PrincipalModel principalModel)
        {
            ViewBag.AddPrincipalSuccess = null;
            ViewBag.AddPrincipalError = null;
            principalModel.States = await _common.GetAllStates();
            principalModel.Schools = await _common.GetSchool();
            CreatePrincipalDto createNewPrincipal = new CreatePrincipalDto();
            principalModel.RoleName = "Principal";
            if (ModelState.IsValid)
            {

                createNewPrincipal.SchoolId = principalModel.SchoolId;
                createNewPrincipal.AddressLine1 = principalModel.AddressLine1;
                createNewPrincipal.AddressLine2 = principalModel.AddressLine2;
                createNewPrincipal.Name = principalModel.Name;
                createNewPrincipal.Email = principalModel.Email;
                createNewPrincipal.Mobile = principalModel.Mobile;
                createNewPrincipal.Password = principalModel.Password;
                createNewPrincipal.Username = principalModel.Username;
                createNewPrincipal.CityId = principalModel.CityId;
                createNewPrincipal.StateId = principalModel.StateId;
                createNewPrincipal.ZipId = principalModel.ZipId;
                createNewPrincipal.IsActive = principalModel.IsActive;
                createNewPrincipal.RoleName = principalModel.RoleName;


                PrincipalResponseModel principalResponseModel = null;
                ViewBag.AddPrincipalSuccess = null;
                ViewBag.AddPrincipalError = null;
                ResponseModel responseModel = new ResponseModel();

                principalResponseModel = await _schoolRepository.AddNewPrinicipal(createNewPrincipal);


                if (principalResponseModel.Succeeded)
                {
                    if (principalResponseModel == null && principalResponseModel.createPrincipalDto == null)
                    {
                        responseModel.ResponseMessage = principalResponseModel.message;
                        responseModel.IsSuccess = principalResponseModel.Succeeded;
                    }
                    if (principalResponseModel != null)
                    {
                        if (principalResponseModel.createPrincipalDto != null)
                        {
                            responseModel.ResponseMessage = principalResponseModel.message;
                            responseModel.IsSuccess = principalResponseModel.Succeeded;
                            ViewBag.AddPrincipalSuccess = "Details added successfully";
                            return RedirectToAction("AddPrincipal", "Principal");

                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = principalResponseModel.message;
                    responseModel.IsSuccess = principalResponseModel.Succeeded;
                    ViewBag.AddPrincipalError = principalResponseModel.message;
                }
            }
            return View(principalModel);

        }
    }
}
