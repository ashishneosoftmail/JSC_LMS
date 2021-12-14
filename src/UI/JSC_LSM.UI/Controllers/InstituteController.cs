using ClosedXML.Excel;
using JSC_LMS.Application.Features.Circulars.Commands.CreateCircular;
using JSC_LMS.Application.Features.Circulars.Commands.UpdateCircular;
using JSC_LMS.Application.Features.EventsFeature.Commands.CreateEvents;
using JSC_LMS.Application.Features.EventsFeature.Commands.UpdateEvents;
using JSC_LMS.Application.Features.Institutes.Commands.CreateInstitute;
using JSC_LMS.Application.Features.Institutes.Commands.UpdateInstitute;
using JSC_LMS.Application.Features.Institutes.Commands.UpdateInstituteAdminChangePassword;
using JSC_LMS.Application.Features.Institutes.Commands.UpdateInstituteAdminProfileInformation;
using JSC_LSM.UI.Helpers;
using JSC_LSM.UI.Models;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.ResponseModels.EventsResponseModel;
using JSC_LSM.UI.ResponseModels.GallaryResponseModel;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region - Controller for Institiute module:by Shivani Goswami
namespace JSC_LSM.UI.Controllers
{
    public class InstituteController : BaseController
    {

        private readonly IStateRepository _stateRepository;
        private readonly ICircularRepository _circularRepository;
        private readonly IInstituteRepository _instituteRepository;
        private readonly JSC_LSM.UI.Common.Common _common;
        private readonly IConfiguration _configuration;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IEventsDetailsRepository _eventsRepository;
        private readonly IGallaryRepository _gallaryRepository;
        private readonly IUserRepository _usersRepository;
        /// <summary>
        /// constructor for institute controller
        /// </summary>
        /// <param name="stateRepository"></param>
        /// <param name="common"></param>
        /// <param name="apiBaseUrl"></param>
        /// <param name="instituteRepository"></param>
        public InstituteController(IStateRepository stateRepository, JSC_LSM.UI.Common.Common common, IOptions<ApiBaseUrl> apiBaseUrl, IInstituteRepository instituteRepository, ICircularRepository circularRepository, IConfiguration configuration, IAnnouncementRepository announcementRepository, ITeacherRepository teacherRepository, IWebHostEnvironment webHostEnvironment, ISchoolRepository schoolRepository , IEventsDetailsRepository eventsRepository , IUserRepository usersRepository)
        {
            _stateRepository = stateRepository;
            _circularRepository = circularRepository;
            _instituteRepository = instituteRepository;
            _common = common;
            _apiBaseUrl = apiBaseUrl;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _announcementRepository = announcementRepository;
            _teacherRepository = teacherRepository;
            _schoolRepository = schoolRepository;
            _eventsRepository = eventsRepository;
            _usersRepository = usersRepository;
        }
        public async Task<IActionResult> Index()
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var institutebyUserId = await _instituteRepository.GetInstituteAdminByUserId(userId);
            var instituteinformation = await _instituteRepository.GetInstituteById(institutebyUserId.data.Id);
            Institute_InformationModel model = new Institute_InformationModel();
            model.InstituteName = instituteinformation.data.InstituteName;
            model.LicensePeriod = Convert.ToInt32(instituteinformation.data.LicenseExpiry.Subtract(DateTime.Today).TotalDays);
            model.LicenseExpiryDate = instituteinformation.data.LicenseExpiry;
            var schoolList = (await _schoolRepository.GetAllSchool()).data.Count(x => x.Institute.Id == instituteinformation.data.Id);
            model.NoOfSchools = schoolList;
            return View(model);
        }

        /// <summary>
        /// Gives all the details in form of a list:by Shivani Goswami
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> InstituteDetails()
        {
            var page = 1;
            var size = 5;
            int recsCount = (await _instituteRepository.GetAllInstituteDetails()).data.Count();
            if (page < 1)
                page = 1;
            ViewBag.GetInstituteById = TempData["GetInstituteById"] as string;
            var pager = new Pager(recsCount, page, size);
            ViewBag.Pager = pager;
            return View(pager);
        }
        /// <summary>
        /// To get the state data in list form:by Shivani Goswami
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<SelectListItem>> GetAllState()
        {
            var states = await _common.GetAllStates();
            return states;
        }
        /// <summary>
        /// To get city list using state Id :by Shivani Goswami
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<SelectListItem>> GetCityByStateId(int id)
        {
            var cities = await _common.GetAllCityByStateId(id);
            return cities;
        }
        /// <summary>
        /// To get Zipcode list using city Id:by Shivani Goswami
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public async Task<List<SelectListItem>> GetZipByCityId(int cityId)
        {
            var cities = await _common.GetAllZipByCityId(cityId);
            return cities;
        }

        /// <summary>
        /// Get method To Add a new institute :by Shivani Goswami
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> AddInstitute()
        {
            ViewBag.AddInstituteSuccess = null;
            ViewBag.AddInstituteError = null;
            Institute institute = new Institute();
            institute.States = await _common.GetAllStates();
            return View(institute);
        }

        [AcceptVerbs("Get", "Post")]

        public async Task<ActionResult> CheckEmailExists(string EmailId)
        {
            List<Institute> RegisterUsers = new List<Institute>();
            GetAllUsersResponseModel getAllUsersResponseModel = null;
            ResponseModel responseModel = new ResponseModel();
            getAllUsersResponseModel = await _usersRepository.GetAllUser();

            foreach (var item in getAllUsersResponseModel.data)
            {
                RegisterUsers.Add(new Institute
                {
                    EmailId = item.Email,
                    Username = item.UserName
                });
            }

            var RegEmailId = (from u in RegisterUsers
                              where u.EmailId.ToUpper() == EmailId.ToUpper()
                              select new { EmailId }).FirstOrDefault();


            if (RegEmailId == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email already exists.");
            }


        }

        [AcceptVerbs("Get", "Post")]

        public async Task<ActionResult> CheckUserNameExists(string UserNAME)
        {
            List<Institute> RegisterUsers = new List<Institute>();
            GetAllUsersResponseModel getAllUsersResponseModel = null;
            ResponseModel responseModel = new ResponseModel();
            getAllUsersResponseModel = await _usersRepository.GetAllUser();

            foreach (var item in getAllUsersResponseModel.data)
            {
                RegisterUsers.Add(new Institute
                {
                    EmailId = item.Email,
                    UserNAME = item.UserName
                });
            }

            var RegEmailId = (from u in RegisterUsers
                              where u.UserNAME == UserNAME
                              select new { UserNAME }).FirstOrDefault();


            if (RegEmailId == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Username already exists.");
            }


        }

        [AcceptVerbs("Get", "Post")]

        public async Task<ActionResult> CheckEmailExistsForUpdate(string EmailId)
        {
            var email = HttpContext.Session.GetString("UpdateInstituteEmail");
            var j = Json(true);
            if (EmailId == email)
            {
                j= Json(true);
            }
            else  if (EmailId != email)
                {
                List<Institute> RegisterUsers = new List<Institute>();
                GetAllUsersResponseModel getAllUsersResponseModel = null;
                ResponseModel responseModel = new ResponseModel();
                getAllUsersResponseModel = await _usersRepository.GetAllUser();

                foreach (var item in getAllUsersResponseModel.data)
                {
                    RegisterUsers.Add(new Institute
                    {
                        EmailId = item.Email,
                        Username = item.UserName
                    });
                }

                var RegEmailId = (from u in RegisterUsers
                                  where u.EmailId.ToUpper() == EmailId.ToUpper()
                                  select new { EmailId }).FirstOrDefault();


                if (RegEmailId == null)
                {
                    j= Json(true);
                }
                else
                {
                    j= Json($"Email already exists.");
                }
            }

            return j;
        }
        [AcceptVerbs("Get", "Post")]
        public async Task<ActionResult> CheckUsernameExistsForUpdate(string UserNAME)
        {

           var username = HttpContext.Session.GetString("UpdateInstituteUsername");
           
            var j = Json(true);
            if (UserNAME == username)
            {
                j = Json(true);
            }
            else if (UserNAME != username)
            {
                List<Institute> RegisterUsers = new List<Institute>();
                GetAllUsersResponseModel getAllUsersResponseModel = null;
                ResponseModel responseModel = new ResponseModel();
                getAllUsersResponseModel = await _usersRepository.GetAllUser();

                foreach (var item in getAllUsersResponseModel.data)
                {
                    RegisterUsers.Add(new Institute
                    {
                        EmailId = item.Email,
                        UserNAME = item.UserName
                    });
                }

                var RegEmailId = (from u in RegisterUsers
                                  where u.UserNAME == UserNAME
                                  select new { UserNAME }).FirstOrDefault();


                if (RegEmailId == null)
                {
                    j = Json(true);
                }
                else
                {
                    j = Json($"Username already exists.");
                }
            }

            return j;
        }
        /// <summary>
        /// Post method to add a new institute :by Shivani Goswami
        /// </summary>
        /// <param name="institute"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddInstitute(Institute institute)
        {
            ViewBag.AddInstituteSuccess = null;
            ViewBag.AddInstituteError = null;
            institute.States = await _common.GetAllStates();
            CreateInstituteDto createNewInstitute = new CreateInstituteDto();

            if (ModelState.IsValid)
            {
                createNewInstitute.InstituteName = institute.InstituteName;
                createNewInstitute.AddressLine1 = institute.AddressLine1;
                createNewInstitute.AddressLine2 = institute.AddressLine2;
                createNewInstitute.ContactPerson = institute.ContactPerson;
                createNewInstitute.Email = institute.EmailId;
                createNewInstitute.Mobile = institute.Mobile;
                createNewInstitute.Password = institute.Password;
                createNewInstitute.Username = institute.UserNAME;
                createNewInstitute.CityId = institute.CityId;
                createNewInstitute.StateId = institute.StateId;
                createNewInstitute.ZipId = institute.ZipId;
                createNewInstitute.LicenseExpiry = institute.LicenseExpiry;
                createNewInstitute.InstituteURL = institute.InstituteURL;
                createNewInstitute.IsActive = institute.IsActive;
                createNewInstitute.RoleName = "Institute Admin";


                InstituteResponseModel instituteResponseModel = null;
                ViewBag.AddInstituteError = null;
                ResponseModel responseModel = new ResponseModel();

                instituteResponseModel = await _instituteRepository.CreateInstitute(createNewInstitute);


                if (instituteResponseModel.Succeeded)
                {
                    if (instituteResponseModel == null && instituteResponseModel.data == null)
                    {
                        responseModel.ResponseMessage = instituteResponseModel.message;
                        responseModel.IsSuccess = instituteResponseModel.Succeeded;
                    }
                    if (instituteResponseModel != null)
                    {
                        if (instituteResponseModel.data != null)
                        {
                            responseModel.ResponseMessage = instituteResponseModel.message;
                            responseModel.IsSuccess = instituteResponseModel.Succeeded;

                            ViewBag.AddInstituteSuccess = "Details Added Successfully";
                            var page = 1;
                            var size = 5;
                            int recsCount = (await _instituteRepository.GetAllInstituteDetails()).data.Count();
                            if (page < 1)
                                page = 1;
                            ViewBag.GetInstituteById = TempData["GetInstituteById"] as string;
                            var pager = new Pager(recsCount, page, size);
                            ViewBag.Pager = pager;

                            return View("InstituteDetails", pager);

                        }
                        else
                        {
                            responseModel.ResponseMessage = instituteResponseModel.message;
                            responseModel.IsSuccess = instituteResponseModel.Succeeded;
                            ViewBag.AddInstituteError = instituteResponseModel.message;
                            return View(institute);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = instituteResponseModel.message;
                    responseModel.IsSuccess = instituteResponseModel.Succeeded;
                    ViewBag.AddInstituteError = instituteResponseModel.message;

                }
            }
            return View(institute);

        }

        /// <summary>
        /// gives institute details using Id :by Shivani Goswami
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<GetInstituteByIdResponseModel> GetInstituteById(int Id)
        {

            var institute = await _instituteRepository.GetInstituteById(Id);
            return institute;
        }
        /// <summary>
        /// gives the institute details which matches the searched parameters:by Shivani Goswami
        /// </summary>
        /// <param name="instituteName"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="isActive"></param>
        /// <param name="licenseExpiry"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<InstituteDetailsViewModel>> GetInstituteByFilters(string instituteName, string city, string state, bool isActive, DateTime licenseExpiry)
        {
            var data = new List<InstituteDetailsViewModel>();

            var dataList = await _instituteRepository.GetInstituteByFilters(instituteName, city, state, licenseExpiry, isActive);
            if (dataList.data != null)
            {
                foreach (var institute in dataList.data)
                {
                    data.Add(new InstituteDetailsViewModel()
                    {
                        Id = institute.Id,
                        InstituteName = institute.InstituteName,
                        AddressLine1 = institute.AddressLine1,
                        AddressLine2 = institute.AddressLine2,
                        CityName = institute.City.CityName,
                        StateName = institute.State.StateName,
                        CreatedDate = institute.CreatedDate,
                        Email = institute.Email,
                        IsActive = institute.IsActive,
                        Mobile = institute.Mobile,
                        InstituteURL = institute.InstituteURL,
                        Username = institute.Username,
                        ContactPerson = institute.ContactPerson,
                        ZipCode = institute.Zip.Zipcode,
                        LicenseExpiry = institute.LicenseExpiry
                    });
                }
            }
            return data;
        }


        /// <summary>
        /// gives all the institute details:by Shivani Goswami
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<InstituteDetailsViewModel>> GetAllInstituteDetails()
        {
            var data = new List<InstituteDetailsViewModel>();

            var dataList = await _instituteRepository.GetAllInstituteDetails();
            foreach (var institute in dataList.data)
            {
                data.Add(new InstituteDetailsViewModel()
                {
                    Id = institute.Id,
                    InstituteName = institute.InstituteName,
                    AddressLine1 = institute.AddressLine1,
                    AddressLine2 = institute.AddressLine2,
                    CityName = institute.City.CityName,
                    StateName = institute.State.StateName,
                    CreatedDate = institute.CreatedDate,
                    Email = institute.Email,
                    IsActive = institute.IsActive,
                    Mobile = institute.Mobile,
                    InstituteURL = institute.InstituteURL,
                    Username = institute.Username,
                    ContactPerson = institute.ContactPerson,
                    ZipCode = institute.Zip.Zipcode,
                    LicenseExpiry = institute.LicenseExpiry
                });
            }
            return data;
        }

        /// <summary>
        /// Custom Pagination for institute module:by Shivani Goswami
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<InstituteDetailsViewModel>> GetAllInstituteDetailsByPagination(int page = 1, int size = 5)
        {
            int recsCount = (await _instituteRepository.GetAllInstituteDetails()).data.Count();
            if (page < 1)
                page = 1;
            var pager = new Pager(recsCount, page, size);

            ViewBag.Pager = pager;
            var data = new List<InstituteDetailsViewModel>();

            //int recSkip = (page - 1) * size;
            var dataList = await _instituteRepository.GetInstituteByPagination(page, size);

            foreach (var institute in dataList.data.GetInstituteListPaginationDto)
            {
                data.Add(new InstituteDetailsViewModel()
                {
                    Id = institute.Id,
                    InstituteName = institute.InstituteName,
                    AddressLine1 = institute.AddressLine1,
                    AddressLine2 = institute.AddressLine2,
                    CityName = institute.City.CityName,
                    StateName = institute.State.StateName,
                    CreatedDate = institute.CreatedDate,
                    Email = institute.Email,
                    IsActive = institute.IsActive,
                    Mobile = institute.Mobile,
                    InstituteURL = institute.InstituteURL,
                    Username = institute.Username,
                    ContactPerson = institute.ContactPerson,
                    ZipCode = institute.Zip.Zipcode,
                    LicenseExpiry = institute.LicenseExpiry
                });
            }
            return data;
        }



        /// <summary>
        /// Updates the Institute details of the given Id:by Shivani Goswami
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> EditInstitute(int id)
        {
            var institute = await _instituteRepository.GetInstituteById(id);
            if (institute.data == null)
            {
                TempData["GetInstituteById"] = institute.message;
                return RedirectToAction("InstituteDetails", "Institute");
            }
            HttpContext.Session.SetString("UpdateInstituteEmail", institute.data.Email);
            HttpContext.Session.SetString("UpdateInstituteUsername", institute.data.Username);

            var instituteData = new UpdateInstituteViewModel()
            {
                Id = institute.data.Id,
                InstituteName = institute.data.InstituteName,
                InstituteURL = institute.data.InstituteURL,
                UserId = institute.data.UserId,
                ContactPerson = institute.data.ContactPerson,
                AddressLine1 = institute.data.AddressLine1,
                AddressLine2 = institute.data.AddressLine2,
                CityId = institute.data.City.Id,
                StateId = institute.data.State.Id,
                EmailId = institute.data.Email,
                IsActive = institute.data.IsActive,
                Mobile = institute.data.Mobile,
                LicenseExpiry = institute.data.LicenseExpiry,
                UserNAME = institute.data.Username,
                ZipId = institute.data.Zip.Id
            };
            TempData["UserId"] = instituteData.UserId;
            instituteData.States = await _common.GetAllStates();
            instituteData.Cities = await _common.GetAllCityByStateId(institute.data.State.Id);
            instituteData.ZipCode = await _common.GetAllZipByCityId(institute.data.Zip.Id);
            return View(instituteData);
        }


        /// <summary>
        /// Post method to update the institute details :by Shivani Goswami
        /// </summary>
        /// <param name="updateInstituteViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInstitute(UpdateInstituteViewModel updateInstituteViewModel)
        {
            ViewBag.UpdateInstituteSuccess = null;
            ViewBag.UpdateInstituteError = null;

            updateInstituteViewModel.States = await _common.GetAllStates();
            updateInstituteViewModel.Cities = await _common.GetAllCityByStateId(updateInstituteViewModel.StateId);
            updateInstituteViewModel.ZipCode = await _common.GetAllZipByCityId(updateInstituteViewModel.CityId);
            UpdateInstituteDto updateInstitute = new UpdateInstituteDto();
            if (ModelState.IsValid)
            {
                updateInstitute.Id = updateInstituteViewModel.Id;
                updateInstitute.UserId = TempData["UserId"].ToString();
                updateInstitute.AddressLine1 = updateInstituteViewModel.AddressLine1;
                updateInstitute.AddressLine2 = updateInstituteViewModel.AddressLine2;
                updateInstitute.ContactPerson = updateInstituteViewModel.ContactPerson;
                updateInstitute.Email = updateInstituteViewModel.EmailId;
                updateInstitute.Mobile = updateInstituteViewModel.Mobile;
                updateInstitute.Username = updateInstituteViewModel.UserNAME;
                updateInstitute.CityId = updateInstituteViewModel.CityId;
                updateInstitute.StateId = updateInstituteViewModel.StateId;
                updateInstitute.ZipId = updateInstituteViewModel.ZipId;
                updateInstitute.IsActive = updateInstituteViewModel.IsActive;
                updateInstitute.LicenseExpiry = updateInstituteViewModel.LicenseExpiry;
                updateInstitute.InstituteURL = updateInstituteViewModel.InstituteURL;
                updateInstitute.InstituteName = updateInstituteViewModel.InstituteName;



                UpdateInstituteResponseModel updateInstituteResponseModel = null;
                ViewBag.UpdateInstituteSuccess = null;
                ViewBag.UpdateInstituteError = null;
                ResponseModel responseModel = new ResponseModel();

                updateInstituteResponseModel = await _instituteRepository.UpdateInstitute(updateInstitute);


                if (updateInstituteResponseModel.Succeeded)
                {
                    if (updateInstituteResponseModel == null && updateInstituteResponseModel?.data == null)
                    {
                        responseModel.ResponseMessage = updateInstituteResponseModel.message;
                        responseModel.IsSuccess = updateInstituteResponseModel.Succeeded;
                    }
                    if (updateInstituteResponseModel != null)
                    {
                        if (updateInstituteResponseModel?.data != null)
                        {
                            responseModel.ResponseMessage = updateInstituteResponseModel.message;
                            responseModel.IsSuccess = updateInstituteResponseModel.Succeeded;
                            ViewBag.UpdateInstituteSuccess = "Details Updated Successfully";
                            var page = 1;
                            var size = 5;
                            int recsCount = (await _instituteRepository.GetAllInstituteDetails()).data.Count();
                            if (page < 1)
                                page = 1;
                            ViewBag.GetInstituteById = TempData["GetInstituteById"] as string;
                            var pager = new Pager(recsCount, page, size);
                            ViewBag.Pager = pager;

                            return View("InstituteDetails", pager);
                        }
                        else
                        {
                            responseModel.ResponseMessage = updateInstituteResponseModel.message;
                            responseModel.IsSuccess = updateInstituteResponseModel.Succeeded;
                            ViewBag.UpdateInstituteError = updateInstituteResponseModel.message;
                            return View(updateInstituteResponseModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = updateInstituteResponseModel.message;
                    responseModel.IsSuccess = updateInstituteResponseModel.Succeeded;
                    ViewBag.UpdateInstituteError = updateInstituteResponseModel.message;
                }
            }
            return View(updateInstituteViewModel);
        }

        /// <summary>
        /// Exports the Institute details in excel format:by Shivani Goswami
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> DownloadExcel()
        {
            var data = new List<InstituteDetailsViewModel>();

            var dataList = await _instituteRepository.GetAllInstituteDetails();
            //Creating DataTable  
            DataTable dt = new DataTable();
            //Setiing Table Name  
            dt.TableName = "InstituteData";
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Institute_Name", typeof(string));
            dt.Columns.Add("Contact_Name", typeof(string));
            dt.Columns.Add("AddressLine1", typeof(string));
            dt.Columns.Add("AddressLine2", typeof(string));
            dt.Columns.Add("Mobile", typeof(string));
            dt.Columns.Add("Username", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("IsActive", typeof(string));
            dt.Columns.Add("City_Id", typeof(int));
            dt.Columns.Add("City_Name", typeof(string));
            dt.Columns.Add("State_Id", typeof(int));
            dt.Columns.Add("State_Name", typeof(string));
            dt.Columns.Add("Zip_Id", typeof(int));
            dt.Columns.Add("ZipCode", typeof(string));
            dt.Columns.Add("CreatedDate", typeof(DateTime));

            dt.Columns.Add("LicenseExpiry", typeof(DateTime));
            dt.Columns.Add("InstituteURL", typeof(string));

            foreach (var _institute in dataList.data)
            {
                dt.Rows.Add(_institute.Id, _institute.InstituteName, _institute.ContactPerson, _institute.AddressLine1, _institute.AddressLine2, _institute.Mobile, _institute.Username, _institute.Email, _institute.IsActive ? "Active" : "Inactive", _institute.City.Id, _institute.City.CityName, _institute.State.Id, _institute.State.StateName, _institute.Zip.Id, _institute.Zip.Zipcode, _institute.CreatedDate?.ToShortDateString(), _institute.LicenseExpiry.ToShortDateString(), _institute.InstituteURL);
            }
            string fileName = "InstituteData_" + DateTime.Now.ToShortDateString() + ".xlsx";

            using (XLWorkbook wb = new XLWorkbook())
            {
                //Add DataTable in worksheet  
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    //Return xlsx Excel File  
                    /* return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);*/
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }

            }
        }

        [HttpGet]
        public async Task<IActionResult> ManageProfile()
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var instituteAdmin = await _instituteRepository.GetInstituteAdminByUserId(userId);
            var instituteadminvm = new ManageProfile()
            {
                ProfileInformation = new ProfileInformation() { Mobile = instituteAdmin.data.Mobile, Name = instituteAdmin.data.Name, Id = instituteAdmin.data.Id, RoleName = Convert.ToString(Request.Cookies["RoleName"]) }
            };
            TempData["CommonId"] = instituteAdmin.data.Id;
            HttpContext.Session.SetString("ProfilrInformationId", instituteAdmin.data.Id.ToString());
            return View(instituteadminvm);

        }


        [HttpGet]
        public async Task<IActionResult> ManageCircular(int page = 1, int size = 5)
        {

            int recsCount = (await _circularRepository.GetAllCircularList()).data.Count();
            if (page < 1)
                page = 1;
            var pager = new Pager(recsCount, page, size);
            ViewBag.Pager = pager;
            ManageCircularModel model = new ManageCircularModel();
            model.Pager = pager;
            model.Schools = await _common.GetSchool();
            var paginationData = await _circularRepository.GetAllCircularListByPagination(page, size);
            List<CircularPagination> pagedData = new List<CircularPagination>();
            foreach (var data in paginationData.data)
            {
                pagedData.Add(new CircularPagination()
                {
                    Id = data.Id,
                    CircularTitle = data.CircularTitle,
                    Description = data.Description,
                    SchoolData = data.SchoolData,
                    Status = data.Status,
                    CreatedDate = data.CreatedDate,

                });
            }
            model.CircularListPagination = pagedData;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> SendCircular(string SendCircular, ManageCircularModel manageCircularModel)
        {
            ViewBag.AddCircularSuccess = null;
            ViewBag.AddCircularError = null;
            manageCircularModel.Schools = await _common.GetSchool();
            CreateCircularDto createCircularDto = new CreateCircularDto();

            if (ModelState.IsValid)
            {
                var CircularsPath = _configuration["Circulars"];
                string filename = null;
                if (manageCircularModel.AddCircular.fileUpload != null)
                {
                    filename = _common.ProcessUploadFile(manageCircularModel.AddCircular.fileUpload, CircularsPath);
                }

                createCircularDto.SchoolId = manageCircularModel.AddCircular.SchoolId;
                createCircularDto.CircularTitle = manageCircularModel.AddCircular.CircularTitle;
                createCircularDto.Description = manageCircularModel.AddCircular.Description;
                createCircularDto.File = filename;
                switch (SendCircular)
                {
                    case "Save":
                        createCircularDto.Status = false;
                        break;
                    case "Send":
                        createCircularDto.Status = true;
                        break;
                    default:
                        createCircularDto.Status = false;
                        break;
                }

                createCircularDto.IsActive = true;
                AddCircularResponseModel addCircularResponseModel = null;
                ViewBag.AddCircularSuccess = null;
                ViewBag.AddCircularError = null;
                ResponseModel responseModel = new ResponseModel();

                addCircularResponseModel = await _circularRepository.AddCircular(createCircularDto);


                if (addCircularResponseModel.Succeeded)
                {
                    if (addCircularResponseModel == null && addCircularResponseModel.data == null)
                    {
                        responseModel.ResponseMessage = addCircularResponseModel.message;
                        responseModel.IsSuccess = addCircularResponseModel.Succeeded;
                    }
                    if (addCircularResponseModel != null)
                    {
                        if (addCircularResponseModel.data != null)
                        {
                            responseModel.ResponseMessage = addCircularResponseModel.message;
                            responseModel.IsSuccess = addCircularResponseModel.Succeeded;
                            ViewBag.AddCircularSuccess = "Details Added Successfully";
                            ModelState.Clear();
                            ManageCircularModel model = new ManageCircularModel();
                            model.Schools = await _common.GetSchool();
                            int recsCount = (await _circularRepository.GetAllCircularList()).data.Count();
                            var page = 1;
                            var size = 5;
                            if (page < 1)
                                page = 1;
                            var pager = new Pager(recsCount, page, size);
                            ViewBag.Pager = pager;
                            model.Pager = pager;
                            var paginationData = await _circularRepository.GetAllCircularListByPagination(page, size);
                            List<CircularPagination> pagedData = new List<CircularPagination>();
                            foreach (var data in paginationData.data)
                            {
                                pagedData.Add(new CircularPagination()
                                {
                                    CircularTitle = data.CircularTitle,
                                    Description = data.Description,
                                    SchoolData = data.SchoolData,
                                    Status = data.Status,
                                    CreatedDate = data.CreatedDate,

                                });
                            }
                            model.CircularListPagination = pagedData;
                            return View("ManageCircular", model);
                        }
                        else
                        {
                            responseModel.ResponseMessage = addCircularResponseModel.message;
                            responseModel.IsSuccess = addCircularResponseModel.Succeeded;


                            ViewBag.AddCircularError = addCircularResponseModel.message;
                            return View(manageCircularModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = addCircularResponseModel.message;
                    responseModel.IsSuccess = addCircularResponseModel.Succeeded;
                    ViewBag.AddCircularError = addCircularResponseModel.message;
                }
            }
            return View(manageCircularModel);
        }

        [HttpGet]
        public async Task<GetCircularByIdResponseModel> ViewCircular(int Id)
        {
            var circular = await _circularRepository.GetCircularById(Id);
            return circular;
        }
        [HttpGet]
        public IActionResult ViewFileData(string fileName)
        {
            ViewCircularFileModel model = new ViewCircularFileModel();
            model.FileName = fileName;

            var FilePath = _configuration["Circulars"];
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath + FilePath);
            model.FilePath = uploadsFolder + fileName;
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteCircular(int id)
        {
            await _circularRepository.DeleteCircular(id);
            ViewBag.DeleteCircularSuccess = "Circular Deleted Successfully";
            return RedirectToAction("ManageCircular");
        }

        [HttpGet]
        public async Task<IActionResult> SearchCircular(string circularTitle, string description, bool status)
        {
            List<CircularPagination> data = new List<CircularPagination>();
            var dataList = await _circularRepository.GetAllCircularByFilterInstituteAdmin(circularTitle, description, status);
            if (dataList.data != null)
            {
                foreach (var d in dataList.data)
                {
                    data.Add(new CircularPagination()
                    {
                        Id = d.Id,
                        CircularTitle = d.CircularTitle,
                        Description = d.Description,
                        SchoolData = new JSC_LMS.Application.Features.Circulars.Queries.GetCircularListByPagination.SchoolDto() { Id = d.SchoolData.Id, SchoolName = d.SchoolData.SchoolName },
                        Status = d.Status,
                        CreatedDate = d.CreatedDate,

                    });
                }
            }
            ManageCircularModel model = new ManageCircularModel();
            model.CircularListPagination = data;
            if (dataList.data.Count() == 0)
            {
                model.Pager = new Pager(1, 1, 1);
            }
            else
            {

                model.Pager = new Pager(dataList.data.Count(), 1, dataList.data.Count());
            }
            ViewBag.Pager = model.Pager;
            model.Schools = await _common.GetSchool();
            return View("ManageCircular", model);
        }

        public async Task<IActionResult> UpdateCircular(ManageCircularModel manageCircularModel, string UpdateCircular)
        {
            ViewBag.UpdateCircularSuccess = null;
            ViewBag.UpdateCircularError = null;
            manageCircularModel.Schools = await _common.GetSchool();
            UpdateCircularDto updateCircularDto = new UpdateCircularDto();
            if (ModelState.IsValid)
            {
                updateCircularDto.Id = manageCircularModel.EditCircular.Id;
                updateCircularDto.SchoolId = manageCircularModel.EditCircular.SchoolId;
                updateCircularDto.CircularTitle = manageCircularModel.EditCircular.CircularTitle;
                updateCircularDto.Description = manageCircularModel.EditCircular.Description;
                switch (UpdateCircular)
                {
                    case "Save":
                        updateCircularDto.Status = false;
                        break;
                    case "Send":
                        updateCircularDto.Status = true;
                        break;
                    default:
                        updateCircularDto.Status = false;
                        break;
                }
                updateCircularDto.IsActive = true;

                if (manageCircularModel.EditCircular.File == null)
                {
                    updateCircularDto.File = null;
                }
                if (manageCircularModel.EditCircular.File != null)
                {
                    updateCircularDto.File = manageCircularModel.EditCircular.File;
                }
                if (manageCircularModel.EditCircular.fileUpload != null)
                {
                    var CircularsPath = _configuration["Circulars"];

                    updateCircularDto.File = _common.ProcessUploadFile(manageCircularModel.EditCircular.fileUpload, CircularsPath);
                }
                UpdateCircularResponseModel updateCircularResponseModel = null;
                ViewBag.UpdateCircularSuccess = null;
                ViewBag.UpdateCircularError = null;
                ResponseModel responseModel = new ResponseModel();

                updateCircularResponseModel = await _circularRepository.EditCircular(updateCircularDto);


                if (updateCircularResponseModel.Succeeded)
                {
                    if (updateCircularResponseModel == null && updateCircularResponseModel?.data == null)
                    {
                        responseModel.ResponseMessage = updateCircularResponseModel.message;
                        responseModel.IsSuccess = updateCircularResponseModel.Succeeded;
                    }
                    if (updateCircularResponseModel != null)
                    {
                        if (updateCircularResponseModel?.data != null)
                        {
                            responseModel.ResponseMessage = updateCircularResponseModel.message;
                            responseModel.IsSuccess = updateCircularResponseModel.Succeeded;
                            ViewBag.UpdateCircularSuccess = "Details Updated Successfully";

                            ModelState.Clear();
                            ManageCircularModel model = new ManageCircularModel();
                            model.Schools = await _common.GetSchool();
                            int recsCount = (await _circularRepository.GetAllCircularList()).data.Count();
                            var page = 1;
                            var size = 5;
                            if (page < 1)
                                page = 1;
                            var pager = new Pager(recsCount, page, size);
                            ViewBag.Pager = pager;
                            model.Pager = pager;
                            var paginationData = await _circularRepository.GetAllCircularListByPagination(page, size);
                            List<CircularPagination> pagedData = new List<CircularPagination>();
                            foreach (var data in paginationData.data)
                            {
                                pagedData.Add(new CircularPagination()
                                {
                                    CircularTitle = data.CircularTitle,
                                    Description = data.Description,
                                    SchoolData = data.SchoolData,
                                    Status = data.Status,
                                    CreatedDate = data.CreatedDate,

                                });
                            }
                            model.CircularListPagination = pagedData;
                            return View("ManageCircular", model);
                        }
                        else
                        {
                            responseModel.ResponseMessage = updateCircularResponseModel.message;
                            responseModel.IsSuccess = updateCircularResponseModel.Succeeded;
                            ViewBag.UpdateCircularError = updateCircularResponseModel.message;
                            return View(manageCircularModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = updateCircularResponseModel.message;
                    responseModel.IsSuccess = updateCircularResponseModel.Succeeded;
                    ViewBag.UpdateCircularError = updateCircularResponseModel.message;
                }
            }
            return View(manageCircularModel);
        }

        [HttpGet]
        public async Task<List<SelectListItem>> GetTeacherName()
        {
            var data = await _teacherRepository.GetAllTeacherDetails();
            List<SelectListItem> teachers = new List<SelectListItem>();
            foreach (var item in data.data)
            {
                teachers.Add(new SelectListItem
                {
                    Text = item.TeacherName,
                    Value = Convert.ToString(item.TeacherName)
                });
            }
            return teachers;
        }


        [HttpGet]
        public async Task<IActionResult> ManageAnnouncement(int page = 1, int size = 20)
        {

            int recsCount = (await _announcementRepository.GetAnnouncementList()).data.Count();
            if (page < 1)
                page = 1;
            var pager = new Pager(recsCount, page, size);
            ViewBag.Pager = pager;
            ManageAnnouncementModel model = new ManageAnnouncementModel();
            model.Pager = pager;
            model.Schools = await _common.GetSchool();
            model.Classes = await _common.GetClass();
            model.Sections = await _common.GetSection();
            model.Subjects = await _common.GetSubject();
            model.Teachers = await GetTeacherName();
            var paginationData = await _announcementRepository.GetAnnouncementListByPagination(page, size);
            List<AnnouncementPagination> pagedData = new List<AnnouncementPagination>();
            foreach (var data in paginationData.data)
            {
                pagedData.Add(new AnnouncementPagination()
                {
                    Id = data.Id,
                    AnnouncementTitle = data.AnnouncementTitle,
                    AnnouncementContent = data.AnnouncementContent,
                    School = data.School,
                    Class = data.Class,
                    Section = data.Section,
                    Subject = data.Subject,
                    Teacher = data.Teacher,
                    CreatedDate = data.CreatedDate,

                });
            }
            model.AnnouncementPagination = pagedData;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SearchAnnouncement(int SchoolId, int ClassId, int SectionId, int SubjectId, string TeacherName, DateTime CreatedDate)
        {
            if (TeacherName == null)
            {
                TeacherName = "Select Teacher";
            }
            List<AnnouncementPagination> data = new List<AnnouncementPagination>();
            var dataList = await _announcementRepository.GetAnnouncementByFilters(SchoolId, ClassId, SectionId, SubjectId, TeacherName, "Select Type", null, null, CreatedDate);
            if (dataList.data != null)
            {
                foreach (var d in dataList.data)
                {
                    data.Add(new AnnouncementPagination()
                    {
                        Id = d.Id,
                        AnnouncementTitle = d.AnnouncementTitle,
                        AnnouncementContent = d.AnnouncementContent,
                        School = new JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination.SchoolDto()
                        {
                            Id = d.School.Id,
                            SchoolName = d.School.SchoolName
                        },
                        Class = new JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination.ClassDto()
                        {
                            Id = d.Class.Id,
                            ClassName = d.Class.ClassName
                        },
                        Section = new JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination.SectionDto()
                        {
                            Id = d.Section.Id,
                            SectionName = d.Section.SectionName
                        },
                        Subject = new JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination.SubjectDto()
                        {
                            Id = d.Subject.Id,
                            SubjectName = d.Subject.SubjectName
                        },
                        Teacher = new JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination.TeacherDto()
                        {
                            Id = d.Teacher.Id,
                            TeacherName = d.Teacher.TeacherName
                        },
                        CreatedDate = d.CreatedDate

                    });
                }
            }
            ManageAnnouncementModel model = new ManageAnnouncementModel();
            model.AnnouncementPagination = data;
            if (dataList.data.Count() == 0)
            {
                model.Pager = new Pager(1, 1, 1);
            }
            else
            {

                model.Pager = new Pager(dataList.data.Count(), 1, dataList.data.Count());
            }
            ViewBag.Pager = model.Pager;
            model.Schools = await _common.GetSchool();
            model.Classes = await _common.GetClass();
            model.Sections = await _common.GetSection();
            model.Subjects = await _common.GetSubject();
            model.Teachers = await GetTeacherName();
            return View("ManageAnnouncement", model);
        }

        [HttpGet]
        public async Task<GetAnnouncementByIdResponseModel> ViewAnnouncement(int Id)
        {
            var announcement = await _announcementRepository.GetAnnouncementById(Id);
            return announcement;
        }

        [HttpGet]
        public async Task<IActionResult> ManageAllEvents()
        {
            var data = new List<GetEventsList>();
            EventsDetailsModel model = new EventsDetailsModel();
            model.Schools = await _common.GetSchool();
            var dataList = await _eventsRepository.GetEventsList();
            var tempstatus="";
            foreach (var eventsdata in dataList.data)
            {
                if (eventsdata.Status)
                {
                    tempstatus = "Sent";
                }
                else
                {
                    tempstatus = "Draft";
                }
                data.Add(new GetEventsList()
                {
                   
                Id = eventsdata.Id,
                    EventTitle = eventsdata.EventTitle,
                    EventCoordinator = eventsdata.EventCoordinator,
                    EventDateTime = eventsdata.EventDateTime,
                    CoordinatorNumber = eventsdata.CoordinatorNumber,
                    SchoolId = eventsdata.SchoolId,
                    statusName=tempstatus,
                    Venue = eventsdata.Venue,
                    SchoolName = eventsdata.School.SchoolName,
                    CreatedDate =eventsdata.CreatedDate,
                    Status = eventsdata.Status

                }) ;
            }
            model.GetEventsList = data;
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> SendEventsData(string SendEventsData, EventsDetailsModel eventsDetailsModel)
        {
            ViewBag.AddEventsSuccess = null;
            ViewBag.AddEventsError = null;
            eventsDetailsModel.Schools = await _common.GetSchool();
            CreateEventsDto createEventsDto = new CreateEventsDto();

            if (ModelState.IsValid)
            {
                var EventsFilePath = _configuration["EventsFile"];
                string filename = null;
                var EventsImagePath = _configuration["EventsImage"];
                string imagename = null;
                if (eventsDetailsModel.AddNewEvents.fileUpload != null)
                {
                    filename = _common.ProcessUploadFile(eventsDetailsModel.AddNewEvents.fileUpload, EventsFilePath);
                }
                if (eventsDetailsModel.AddNewEvents.imageUpload != null)
                {
                    imagename = _common.ProcessUploadFile(eventsDetailsModel.AddNewEvents.imageUpload, EventsImagePath);
                }

                createEventsDto.SchoolId = eventsDetailsModel.AddNewEvents.SchoolId;
                createEventsDto.EventTitle = eventsDetailsModel.AddNewEvents.EventTitle;
                createEventsDto.Description = eventsDetailsModel.AddNewEvents.Description;
                createEventsDto.EventDateTime = eventsDetailsModel.AddNewEvents.EventDateTime;
                createEventsDto.File = filename;
                createEventsDto.Image = imagename;
                createEventsDto.Venue = eventsDetailsModel.AddNewEvents.Venue;
                createEventsDto.CoordinatorNumber = eventsDetailsModel.AddNewEvents.CoordinatorNumber;
                createEventsDto.EventCoordinator = eventsDetailsModel.AddNewEvents.EventCoordinator;
                switch (SendEventsData)
                {
                    case "Save":
                        createEventsDto.Status = false;
                        break;
                    case "Send":
                        createEventsDto.Status = true;
                        break;
                    default:
                        createEventsDto.Status = false;
                        break;
                }

                createEventsDto.IsActive = true;
                AddEventsResponseModel addEventsResponseModel = null;
                ViewBag.AddEventsSuccess = null;
                ViewBag.AddEventsError = null;
                ResponseModel responseModel = new ResponseModel();

                addEventsResponseModel = await _eventsRepository.AddEventsData(createEventsDto);


                if (addEventsResponseModel.Succeeded)
                {
                    if (addEventsResponseModel == null && addEventsResponseModel.data == null)
                    {
                        responseModel.ResponseMessage = addEventsResponseModel.message;
                        responseModel.IsSuccess = addEventsResponseModel.Succeeded;
                    }
                    if (addEventsResponseModel != null)
                    {
                        if (addEventsResponseModel.data != null)
                        {
                            responseModel.ResponseMessage = addEventsResponseModel.message;
                            responseModel.IsSuccess = addEventsResponseModel.Succeeded;
                            ViewBag.AddEventsSuccess = "Details Added Successfully";
                            ModelState.Clear();
                            EventsDetailsModel model = new EventsDetailsModel();
                          
                            var data = new List<GetEventsList>();
                          
                            var dataList = await _eventsRepository.GetEventsList();
                            var tempstatus = "";
                            foreach (var eventsdata in dataList.data)
                            {
                                if (eventsdata.Status)
                                {
                                    tempstatus = "Sent";
                                }
                                else
                                {
                                    tempstatus = "Draft";
                                }
                                data.Add(new GetEventsList()
                                {

                                    Id = eventsdata.Id,
                                    EventTitle = eventsdata.EventTitle,
                                    EventCoordinator = eventsdata.EventCoordinator,
                                    EventDateTime = eventsdata.EventDateTime,
                                    CoordinatorNumber = eventsdata.CoordinatorNumber,
                                    SchoolId = eventsdata.SchoolId,
                                    statusName = tempstatus,
                                    Venue = eventsdata.Venue,
                                    SchoolName = eventsdata.School.SchoolName,
                                    CreatedDate = eventsdata.CreatedDate,
                                    Status = eventsdata.Status

                                });
                            }
                            model.GetEventsList = data;
                            return View("ManageAllEvents", model);
                        }
                        else
                        {
                            responseModel.ResponseMessage = addEventsResponseModel.message;
                            responseModel.IsSuccess = addEventsResponseModel.Succeeded;


                            ViewBag.AddCircularError = addEventsResponseModel.message;
                            return View(eventsDetailsModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = addEventsResponseModel.message;
                    responseModel.IsSuccess = addEventsResponseModel.Succeeded;
                    ViewBag.AddCircularError = addEventsResponseModel.message;
                }
            }
            return View(eventsDetailsModel);
        }

        [HttpGet]
        public async Task<GetEventsByIdResponseModel> ViewEventsData(int Id)
        {
           
            var eventdata = await _eventsRepository.GetEventsById(Id);
            return eventdata;
        }


        public async Task<IActionResult> UpdateEventsData(EventsDetailsModel eventsDetailsModel, string UpdateEventsData)
        {
            ViewBag.UpdateEventSuccess = null;
            ViewBag.UpdateEventError = null;
            eventsDetailsModel.Schools = await _common.GetSchool();
            UpdateEventsDto updateEventsDto = new UpdateEventsDto();
            if (ModelState.IsValid)
            {
                updateEventsDto.SchoolId = eventsDetailsModel.UpdateEventsById.SchoolId;
                updateEventsDto.EventTitle = eventsDetailsModel.UpdateEventsById.EventTitle;
                updateEventsDto.Description = eventsDetailsModel.UpdateEventsById.Description;
                updateEventsDto.EventDateTime = eventsDetailsModel.UpdateEventsById.EventDateTime;
                updateEventsDto.Venue = eventsDetailsModel.UpdateEventsById.Venue;
                updateEventsDto.CoordinatorNumber = eventsDetailsModel.UpdateEventsById.CoordinatorNumber;
                updateEventsDto.EventCoordinator = eventsDetailsModel.UpdateEventsById.EventCoordinator;

                updateEventsDto.Id = eventsDetailsModel.UpdateEventsById.Id;
               
                switch (UpdateEventsData)
                {
                    case "Save":
                        updateEventsDto.Status = false;
                        break;
                    case "Send":
                        updateEventsDto.Status = true;
                        break;
                    default:
                        updateEventsDto.Status = false;
                        break;
                }
                updateEventsDto.IsActive = true;

                if (eventsDetailsModel.UpdateEventsById.File == null)
                {
                    updateEventsDto.File = null;
                }
                if (eventsDetailsModel.UpdateEventsById.File != null)
                {
                    updateEventsDto.File = eventsDetailsModel.UpdateEventsById.File;
                }
                if (eventsDetailsModel.UpdateEventsById.fileUpload != null)
                {
                    var EventsPath = _configuration["EventsFile"];

                    updateEventsDto.File = _common.ProcessUploadFile(eventsDetailsModel.UpdateEventsById.fileUpload, EventsPath);
                }

                if (eventsDetailsModel.UpdateEventsById.Image == null)
                {
                    updateEventsDto.Image = null;
                }
                if (eventsDetailsModel.UpdateEventsById.Image != null)
                {
                    updateEventsDto.Image = eventsDetailsModel.UpdateEventsById.Image;
                }
                if (eventsDetailsModel.UpdateEventsById.imageUpload != null)
                {
                    var EventsPathImg = _configuration["EventsImage"];

                    updateEventsDto.Image = _common.ProcessUploadFile(eventsDetailsModel.UpdateEventsById.imageUpload, EventsPathImg);
                }
                UpdateEventsResponseModel updateEventsResponseModel = null;
                ViewBag.UpdateEventsSuccess = null;
                ViewBag.UpdateEventsError = null;
                ResponseModel responseModel = new ResponseModel();

                updateEventsResponseModel = await _eventsRepository.UpdateEventsDetails(updateEventsDto);


                if (updateEventsResponseModel.Succeeded)
                {
                    if (updateEventsResponseModel == null && updateEventsResponseModel?.data == null)
                    {
                        responseModel.ResponseMessage = updateEventsResponseModel.message;
                        responseModel.IsSuccess = updateEventsResponseModel.Succeeded;
                    }
                    if (updateEventsResponseModel != null)
                    {
                        if (updateEventsResponseModel?.data != null)
                        {
                            responseModel.ResponseMessage = updateEventsResponseModel.message;
                            responseModel.IsSuccess = updateEventsResponseModel.Succeeded;
                            ViewBag.UpdateEventsSuccess = "Details Updated Successfully";

                            ModelState.Clear();
                            EventsDetailsModel model = new EventsDetailsModel();

                            var data = new List<GetEventsList>();

                            var dataList = await _eventsRepository.GetEventsList();
                            var tempstatus = "";
                            foreach (var eventsdata in dataList.data)
                            {
                                if (eventsdata.Status)
                                {
                                    tempstatus = "Sent";
                                }
                                else
                                {
                                    tempstatus = "Draft";
                                }
                                data.Add(new GetEventsList()
                                {

                                    Id = eventsdata.Id,
                                    EventTitle = eventsdata.EventTitle,
                                    EventCoordinator = eventsdata.EventCoordinator,
                                    EventDateTime = eventsdata.EventDateTime,
                                    CoordinatorNumber = eventsdata.CoordinatorNumber,
                                    SchoolId = eventsdata.SchoolId,
                                    statusName = tempstatus,
                                    Venue = eventsdata.Venue,
                                    SchoolName = eventsdata.School.SchoolName,
                                    CreatedDate = eventsdata.CreatedDate,
                                    Status = eventsdata.Status

                                });
                            }
                            model.GetEventsList = data;
                            return View("ManageAllEvents", model);
                        }
                        else
                        {
                            responseModel.ResponseMessage = updateEventsResponseModel.message;
                            responseModel.IsSuccess = updateEventsResponseModel.Succeeded;
                            ViewBag.UpdateEventsError = updateEventsResponseModel.message;
                            return View(eventsDetailsModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = updateEventsResponseModel.message;
                    responseModel.IsSuccess = updateEventsResponseModel.Succeeded;
                    ViewBag.UpdateEventsError = updateEventsResponseModel.message;
                }
            }
            return View(eventsDetailsModel);
        }


        [HttpGet]
        public async Task<IActionResult> DeleteGallary(int id)
        {
            await _gallaryRepository.DeleteGallary(id);
            ViewBag.DeleteGallarySuccess = "Image Deleted Successfully";
            return RedirectToAction("ManageGallary");
        }

        [HttpGet]
        public async Task<GetGallaryListByIdResponseModel> ViewGallary(int Id)
        {
            var gallary = await _gallaryRepository.GetGallaryById(Id);
            return gallary;
        }

        [HttpGet]
        public async Task<IActionResult> ManageGallary()
        {

            GallaryDetailsModel model = new GallaryDetailsModel();
            model.Events = await _common.GetEvent();
            return View(model);
        }




    }

}
#endregion