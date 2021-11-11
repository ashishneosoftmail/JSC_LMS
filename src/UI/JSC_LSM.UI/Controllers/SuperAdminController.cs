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
using JSC_LMS.Application.Features.Superadmin.Commands.UpdateSuperadminPassword;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace JSC_LSM.UI.Controllers
{
    public class SuperAdminController : BaseController
    {
        private readonly ISuperadminRepository _superadminRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        public SuperAdminController(ISuperadminRepository superadminRepository, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _superadminRepository = superadminRepository;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
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
            var user = User;
            Console.WriteLine(user);
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var superadmin = await _superadminRepository.GetSuperadminByUserId(userId);
            var LoginImagePath = _configuration["LoginImages"];
            var LogoImagePath = _configuration["Logos"];
            var superadminvm = new UpdateSuperadminProfileInformationModel()
            {
                Id = superadmin.data.Id,
                EmailSupport = superadmin.data.EmailSupport,
                MobileSupport = superadmin.data.MobileSupport,
                Name = superadmin.data.Name,
                LogoFileName = LogoImagePath + superadmin.data.Logo,
                LoginImageFileName = LoginImagePath + superadmin.data.LoginImage
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
        public async Task<IActionResult> UploadImageLogo(IFormFile Logo, IFormFile LoginImage)
        {


            var LoginImagePath = _configuration["LoginImages"];
            var LogoImagePath = _configuration["Logos"];

            var LogoImageFileName = ProcessUploadFile(Logo, LogoImagePath);
            var LoginImageFileName = ProcessUploadFile(LoginImage, LoginImagePath);

            ViewBag.UpdateSuperadminChangePasswordSuccess = null;
            ViewBag.UpdateSuperadminChangePasswordError = null;
            var Id = Convert.ToInt32(TempData["SuperAdminId"].ToString());

            UpdateSuperadminImageResponseModel updateSuperadminImageResponseModel = null;
            ViewBag.UpdateSuperadminChangePasswordSuccess = null;
            ViewBag.UpdateSuperadminChangePasswordError = null;
            ResponseModel responseModel = new ResponseModel();

            updateSuperadminImageResponseModel = await _superadminRepository.UpdateSuperadminImage(Id, LogoImageFileName, LoginImageFileName);


            if (updateSuperadminImageResponseModel.Succeeded)
            {
                if (updateSuperadminImageResponseModel == null && updateSuperadminImageResponseModel?.data == null)
                {
                    responseModel.ResponseMessage = updateSuperadminImageResponseModel.message;
                    responseModel.IsSuccess = updateSuperadminImageResponseModel.Succeeded;
                }
                if (updateSuperadminImageResponseModel != null)
                {
                    if (updateSuperadminImageResponseModel?.data != null)
                    {
                        responseModel.ResponseMessage = updateSuperadminImageResponseModel.message;
                        responseModel.IsSuccess = updateSuperadminImageResponseModel.Succeeded;

                        ViewBag.UpdateSuperadminChangePasswordSuccess = updateSuperadminImageResponseModel.message;
                        return RedirectToAction("Profile", "SuperAdmin");
                    }
                    else
                    {
                        responseModel.ResponseMessage = updateSuperadminImageResponseModel.message;
                        responseModel.IsSuccess = updateSuperadminImageResponseModel.Succeeded;
                        ViewBag.UpdateSuperadminChangePasswordError = updateSuperadminImageResponseModel.message;
                        return RedirectToAction("Profile", "SuperAdmin");
                    }
                }
            }
            else
            {
                responseModel.ResponseMessage = updateSuperadminImageResponseModel.message;
                responseModel.IsSuccess = updateSuperadminImageResponseModel.Succeeded;
                ViewBag.UpdateSuperadminChangePasswordError = updateSuperadminImageResponseModel.message;
            }
            return RedirectToAction("Profile", "SuperAdmin");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuperAdminChangePassword(SuperadminChangePasswordModel superadminChangePasswordModel)
        {
            ViewBag.UpdateSuperadminChangePasswordSuccess = null;
            ViewBag.UpdateSuperadminChangePasswordError = null;
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var user = User;
            UpdateSuperadminChangePasswordDto updateSuperadminChangePasswordDto = new UpdateSuperadminChangePasswordDto();
            if (ModelState.IsValid)
            {
                updateSuperadminChangePasswordDto.UserId = userId;
                updateSuperadminChangePasswordDto.CurrentPassword = superadminChangePasswordModel.CurrentPassword;
                updateSuperadminChangePasswordDto.NewPassword = superadminChangePasswordModel.NewPassword;

                SuperadminChangePasswordResponseModel superadminChangePasswordResponseModel = null;
                ViewBag.UpdateSuperadminChangePasswordSuccess = null;
                ViewBag.UpdateSuperadminChangePasswordError = null;
                ResponseModel responseModel = new ResponseModel();

                superadminChangePasswordResponseModel = await _superadminRepository.SuperAdminChangePassword(updateSuperadminChangePasswordDto);


                if (superadminChangePasswordResponseModel.Succeeded)
                {
                    if (superadminChangePasswordResponseModel == null && superadminChangePasswordResponseModel?.data == null)
                    {
                        responseModel.ResponseMessage = superadminChangePasswordResponseModel.message;
                        responseModel.IsSuccess = superadminChangePasswordResponseModel.Succeeded;
                    }
                    if (superadminChangePasswordResponseModel != null)
                    {
                        if (superadminChangePasswordResponseModel?.data != null)
                        {
                            responseModel.ResponseMessage = superadminChangePasswordResponseModel.message;
                            responseModel.IsSuccess = superadminChangePasswordResponseModel.Succeeded;

                            ViewBag.UpdateSuperadminChangePasswordSuccess = superadminChangePasswordResponseModel.message;
                            return RedirectToAction("Profile", "SuperAdmin");

                        }
                        else
                        {
                            responseModel.ResponseMessage = superadminChangePasswordResponseModel.message;
                            responseModel.IsSuccess = superadminChangePasswordResponseModel.Succeeded;
                            ViewBag.UpdateSuperadminChangePasswordError = superadminChangePasswordResponseModel.message;
                            return RedirectToAction("Profile", "SuperAdmin");

                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = superadminChangePasswordResponseModel.message;
                    responseModel.IsSuccess = superadminChangePasswordResponseModel.Succeeded;
                    ViewBag.UpdateSuperadminChangePasswordError = superadminChangePasswordResponseModel.message;
                }
            }
            return RedirectToAction("Profile", "SuperAdmin");

        }
        [NonAction]
        private string ProcessUploadFile(IFormFile formFile, string path)
        {
            string uniqueFileName = null;
            if (formFile != null)
            {
                if (!Directory.Exists(_webHostEnvironment.WebRootPath + path))
                {
                    Directory.CreateDirectory(_webHostEnvironment.WebRootPath + path);
                }

                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath + path);
                uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
