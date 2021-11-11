using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using JSC_LSM.UI.Services.IRepositories;
using JSC_LSM.UI.Models;
using JSC_LMS.Application.Features.Superadmin.Commands.UpdateSuperadmin;
using JSC_LSM.UI.ResponseModels;

namespace JSC_LSM.UI.Controllers
{
    public class SuperAdminController : BaseController
    {
        private readonly ISuperadminRepository _superadminRepository;
        public SuperAdminController(ISuperadminRepository superadminRepository)
        {
            _superadminRepository = superadminRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManagesSchools()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {

            var userId = Convert.ToString(Request.Cookies["Id"]);
            var superadmin = await _superadminRepository.GetSuperadminByUserId(userId);
            var superadminvm = new UpdateSuperadminProfileInformationModel()
            {
                Id = superadmin.data.Id,
                EmailSupport = superadmin.data.EmailSupport,
                MobileSupport = superadmin.data.MobileSupport,
                Name = superadmin.data.Name,
                Logo = superadmin.data.Logo,
                LoginImage = superadmin.data.LoginImage
            };
            TempData["SuperAdminId"] = superadminvm.Id;
            return View(superadminvm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(UpdateSuperadminProfileInformationModel updateSuperadminProfileInformationModel)
        {
            ViewBag.UpdateSuperadminSuccess = null;
            ViewBag.UpdateSuperadminError = null;

            UpdateSuperadminDto updateSuperadminDto = new UpdateSuperadminDto();
            if (ModelState.IsValid)
            {

                updateSuperadminDto.Id = Convert.ToInt32(TempData["SuperAdminId"].ToString());
                updateSuperadminDto.Name = updateSuperadminProfileInformationModel.Name;
                updateSuperadminDto.EmailSupport = updateSuperadminProfileInformationModel.EmailSupport;
                updateSuperadminDto.MobileSupport = updateSuperadminProfileInformationModel.MobileSupport;

                UpdateSuperadminProfileInformationResponseModel updateSuperadminProfileInformationResponseModel = null;
                ViewBag.UpdateSuperadminSuccess = null;
                ViewBag.UpdateSuperadminError = null;
                ResponseModel responseModel = new ResponseModel();

                updateSuperadminProfileInformationResponseModel = await _superadminRepository.UpdateSuperadminPersonalInformation(updateSuperadminDto);


                if (updateSuperadminProfileInformationResponseModel.Succeeded)
                {
                    if (updateSuperadminProfileInformationResponseModel == null && updateSuperadminProfileInformationResponseModel?.data == null)
                    {
                        responseModel.ResponseMessage = updateSuperadminProfileInformationResponseModel.message;
                        responseModel.IsSuccess = updateSuperadminProfileInformationResponseModel.Succeeded;
                    }
                    if (updateSuperadminProfileInformationResponseModel != null)
                    {
                        if (updateSuperadminProfileInformationResponseModel?.data != null)
                        {
                            responseModel.ResponseMessage = updateSuperadminProfileInformationResponseModel.message;
                            responseModel.IsSuccess = updateSuperadminProfileInformationResponseModel.Succeeded;
                            ViewBag.UpdatePrincipalSuccess = "Details Updated Successfully";

                            return RedirectToAction("Profile", "SuperAdmin");
                        }
                        else
                        {
                            responseModel.ResponseMessage = updateSuperadminProfileInformationResponseModel.message;
                            responseModel.IsSuccess = updateSuperadminProfileInformationResponseModel.Succeeded;
                            ViewBag.UpdatePrincipalError = updateSuperadminProfileInformationResponseModel.message;
                            return View(updateSuperadminProfileInformationModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = updateSuperadminProfileInformationResponseModel.message;
                    responseModel.IsSuccess = updateSuperadminProfileInformationResponseModel.Succeeded;
                    ViewBag.UpdatePrincipalError = updateSuperadminProfileInformationResponseModel.message;
                }
            }
            return View(updateSuperadminProfileInformationModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadImageLogo()
        {
            return null;
        }
    }
}
