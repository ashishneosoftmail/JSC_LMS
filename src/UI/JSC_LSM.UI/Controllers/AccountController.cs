using JSC_LMS.Application.Models.Authentication;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(AuthenticationRequest authenticationRequest)
        {
            AuthenticationResponseModel authenticationResponseModel  = null;
            ResponseModel responseModel = new ResponseModel();
            if (ModelState.IsValid)
            {
                authenticationResponseModel = await _userRepository.UserAuthenticate(authenticationRequest);

                if (authenticationResponseModel.isSuccess)
                {
                    if (authenticationResponseModel == null) // && authenticationResponseModel.userDetail == null && authenticationResponseModel.userDetail.Id > 0)
                    {
                        responseModel.ResponseMessage = authenticationResponseModel.message;
                        responseModel.IsSuccess = authenticationResponseModel.isSuccess;
                    }
                    if (authenticationResponseModel != null)
                    {
                        //User user = authenticationResponseModel.userDetail;
                        responseModel.ResponseMessage = authenticationResponseModel.message;
                        responseModel.IsSuccess = authenticationResponseModel.isSuccess;
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {

                    responseModel.ResponseMessage = authenticationResponseModel.message;
                    responseModel.IsSuccess = authenticationResponseModel.isSuccess;
                }
            }

            return View();
        }
    }
}
