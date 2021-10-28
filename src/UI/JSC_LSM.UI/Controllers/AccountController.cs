using JSC_LMS.Application.Models.Authentication;
using JSC_LSM.UI.Helpers;
using JSC_LSM.UI.Models;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        
        public AccountController(IUserRepository userRepository, IOptions<ApiBaseUrl> apiBaseUrl)
        {
            _userRepository = userRepository;
            _apiBaseUrl = apiBaseUrl;

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
        public async Task<ActionResult> Login(Login login)
        {
            AuthenticationRequest authenticationRequest = new AuthenticationRequest();

            authenticationRequest.Email = login.Email;
            authenticationRequest.Password = login.Password;

            AuthenticationResponseModel authenticationResponseModel = null;
            ResponseModel responseModel = new ResponseModel();
            if (ModelState.IsValid)
            {

                //using (HttpClient client = new HttpClient())
                //{
                //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
                //    var jsonData = JsonConvert.SerializeObject(authenticationRequest);
                //    var dataEncode = new StringContent(jsonData, Encoding.UTF8, "application/json");

                //    HttpResponseMessage httpResponseMessage = client.PostAsync(_apiBaseUrl.Value.LmsApiBaseUrl + UrlHelper.UserAuthenticate, dataEncode).Result;

                //    if (httpResponseMessage.IsSuccessStatusCode)
                //    {
                //        var loginInfo = httpResponseMessage.Content.ReadAsStringAsync().Result;
                //        var Pack = JsonConvert.DeserializeObject(loginInfo);
                //        authenticationResponseModel = JsonConvert.DeserializeObject<AuthenticationResponseModel>(Convert.ToString(Pack));
                //        if (authenticationResponseModel.isSuccess)
                //        {

                //        }
                //    }
                //}

                authenticationResponseModel = await _userRepository.UserAuthenticate(authenticationRequest);

                if (authenticationResponseModel.isSuccess)
                {
                    if (authenticationResponseModel == null && authenticationResponseModel.userDetail == null)
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
