using JSC_LMS.Application.Models.Authentication;
using JSC_LSM.UI.Helpers;
using JSC_LSM.UI.Models;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly IRoleRepository _roleRepository;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;

        public AccountController(IUserRepository userRepository, IRoleRepository roleRepository, IOptions<ApiBaseUrl> apiBaseUrl)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _apiBaseUrl = apiBaseUrl;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            ViewBag.LoginError = null;
            Login login = new Login();
            login.Roles = await GetAllRoles();
           // Console.WriteLine(login.Roles);
            return View(login);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Login login)
        {
            ViewBag.LoginError = null;
            login.Roles = await GetAllRoles();
            AuthenticationRequest authenticationRequest = new AuthenticationRequest();
            if (ModelState.IsValid)
            {
                authenticationRequest.Email = login.Email;
                authenticationRequest.Password = login.Password;
                authenticationRequest.Role = login.Role;
                AuthenticationResponseModel authenticationResponseModel = null;
                ResponseModel responseModel = new ResponseModel();


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
                    if (authenticationResponseModel == null && authenticationResponseModel.userDetails == null)
                    {
                        responseModel.ResponseMessage = authenticationResponseModel.message;
                        responseModel.IsSuccess = authenticationResponseModel.isSuccess;
                    }
                    if (authenticationResponseModel != null)
                    {
                        if (authenticationResponseModel.isAuthenticated)
                        {
                            responseModel.ResponseMessage = authenticationResponseModel.message;
                            responseModel.IsSuccess = authenticationResponseModel.isSuccess;

                            // Add cookies 
                            CookieOptions option = new CookieOptions();
                            option.Expires = DateTime.Now.AddDays(1); //DateTime.Now.AddMinutes(30);
                            Response.Cookies.Append("Token", authenticationResponseModel.token, option);
                            Response.Cookies.Append("Id", Convert.ToString(authenticationResponseModel.userDetails.Id), option);
                            Response.Cookies.Append("Email", authenticationResponseModel.userDetails.Email, option);
                            Response.Cookies.Append("FirstName", authenticationResponseModel.userDetails.FirstName, option);
                            Response.Cookies.Append("Name", authenticationResponseModel.userDetails.FirstName + " " + authenticationResponseModel.userDetails.LastName, option);
                            Response.Cookies.Append("RoleId", Convert.ToString(authenticationResponseModel.userDetails.Role.RoleId), option);
                            Response.Cookies.Append("RoleName", Convert.ToString(authenticationResponseModel.userDetails.Role.RoleName), option);

                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = authenticationResponseModel.message;
                    responseModel.IsSuccess = authenticationResponseModel.isSuccess;
                    ViewBag.LoginError = responseModel.ResponseMessage;
                }
            }
            return View(login);
        }

        public IActionResult Logout()
        {
            //Response.Cookies.Delete("Email");
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            return RedirectToAction("Login", "Account");
        }

        [NonAction]
        private async Task<List<SelectListItem>> GetAllRoles()
        {
            List<SelectListItem> role = new List<SelectListItem>();
            GetAllRolesResponseModel getAllRolesResponseModel = null;
            ResponseModel responseModel = new ResponseModel();
            getAllRolesResponseModel = await _roleRepository.GetAllRoles();

            if (getAllRolesResponseModel.isSuccess)
            {
                if (getAllRolesResponseModel == null && getAllRolesResponseModel.data == null)
                {
                    responseModel.ResponseMessage = getAllRolesResponseModel.message;
                    responseModel.IsSuccess = getAllRolesResponseModel.isSuccess;
                }
                if (getAllRolesResponseModel != null)
                {
                    //User user = authenticationResponseModel.userDetail;
                    responseModel.ResponseMessage = getAllRolesResponseModel.message;
                    responseModel.IsSuccess = getAllRolesResponseModel.isSuccess;
                    foreach (var item in getAllRolesResponseModel.data)
                    {
                        role.Add(new SelectListItem
                        {
                            Text = item.RoleName,
                            Value = Convert.ToString(item.Id)
                        });
                    }
                    return role;
                }
            }
            else
            {
                responseModel.ResponseMessage = getAllRolesResponseModel.message;
                responseModel.IsSuccess = getAllRolesResponseModel.isSuccess;
            }
            return null;
        }
    }
}
