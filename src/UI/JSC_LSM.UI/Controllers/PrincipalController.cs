using ClosedXML.Excel;
using JSC_LMS.Application.Features.Circulars.Commands.CreateCircular;
using JSC_LMS.Application.Features.Circulars.Commands.UpdateCircular;
using JSC_LMS.Application.Features.Principal.Commands.CreatePrincipal;
using JSC_LMS.Application.Features.Principal.Commands.UpdatePrincipal;
using JSC_LSM.UI.Models;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;



namespace JSC_LSM.UI.Controllers
{
    #region -Developed By Harsh Chheda
    public class PrincipalController : BaseController
    {
        private readonly JSC_LSM.UI.Common.Common _common;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IPrincipalRepository _principalRepository;

        private readonly ITeacherRepository _teacherRepository;
        private readonly IAnnouncementRepository _announcementRepository;

        private readonly ICircularRepository _circularRepository;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IInstituteRepository _instituteRepository;

        /// <summary>
        /// Constructor For the PrincipalController - Developed By Harsh Chheda
        /// </summary>
        /// <param name="common"></param>
        /// <param name="schoolRepository"></param>
        /// <param name="principalRepository"></param>


        public PrincipalController(JSC_LSM.UI.Common.Common common, ISchoolRepository schoolRepository, IPrincipalRepository principalRepository, ITeacherRepository teacherRepository, IAnnouncementRepository announcementRepository, ICircularRepository circularRepository, IConfiguration configuration, IWebHostEnvironment webHostEnvironment, IInstituteRepository instituteRepository)

        {
            _circularRepository = circularRepository;
            _common = common;
            _schoolRepository = schoolRepository;
            _principalRepository = principalRepository;
            _teacherRepository = teacherRepository;
            _announcementRepository = announcementRepository;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _instituteRepository = instituteRepository;
        }
        public async Task<IActionResult> Index()
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var principalUserID = await _principalRepository.GetPrincipalByUserId(userId);
            var principalData = await _principalRepository.GetPrincipalById(principalUserID.data.Id);
            var school = await _schoolRepository.GetSchoolById(principalData.data.School.Id);
            var institute = await _instituteRepository.GetInstituteById(school.data.Institute.Id);
            PrincipalInformation model = new PrincipalInformation();
            model.SchoolName = school.data.SchoolName;
            model.InstituteName = institute.data.InstituteName;
            return View(model);
        }
        [HttpGet]

        public async Task<IActionResult> AddPrincipal()
        {
            PrincipalModel principalModel = new PrincipalModel();
            principalModel.States = await _common.GetAllStates();
            principalModel.Schools = await _common.GetSchool();
            return View(principalModel);
        }
        /// <summary>
        /// Returns all the city by the state id - Developed By Harsh Chheda
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<SelectListItem>> GetCityByStateId(int id)
        {
            var cities = await _common.GetAllCityByStateId(id);
            return cities;
        }
        /// <summary>
        /// Returns all the zip by the city id - Developed By Harsh Chheda
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public async Task<List<SelectListItem>> GetZipByCityId(int cityId)
        {
            var zip = await _common.GetAllZipByCityId(cityId);
            return zip;
        }
        /// <summary>
        /// Add the principal - Developed By Harsh Chheda
        /// </summary>
        /// <param name="principalModel"></param>
        /// <returns></returns>
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

                principalResponseModel = await _principalRepository.AddNewPrinicipal(createNewPrincipal);


                if (principalResponseModel.Succeeded)
                {
                    if (principalResponseModel == null && principalResponseModel.data == null)
                    {
                        responseModel.ResponseMessage = principalResponseModel.message;
                        responseModel.IsSuccess = principalResponseModel.Succeeded;
                    }
                    if (principalResponseModel != null)
                    {
                        if (principalResponseModel.data != null)
                        {
                            responseModel.ResponseMessage = principalResponseModel.message;
                            responseModel.IsSuccess = principalResponseModel.Succeeded;
                            ViewBag.AddPrincipalSuccess = "Details Added Successfully";
                            ModelState.Clear();
                            var newPrincipalModel = new PrincipalModel();
                            newPrincipalModel.States = await _common.GetAllStates();
                            newPrincipalModel.Schools = await _common.GetSchool();
                            /* return RedirectToAction("PrincipalDetails", "Principal");*/
                            var page = 1;
                            var size = 5;
                            int recsCount = (await _principalRepository.GetAllPrincipalDetails()).data.Count();
                            if (page < 1)
                                page = 1;
                            ViewBag.GetPrincipalById = TempData["GetPrincipalById"] as string;
                            var pager = new Pager(recsCount, page, size);
                            ViewBag.Pager = pager;
                            return View("PrincipalDetails", pager);
                        }
                        else
                        {
                            responseModel.ResponseMessage = principalResponseModel.message;
                            responseModel.IsSuccess = principalResponseModel.Succeeded;
                            ViewBag.AddPrincipalError = "Something Went Wrong";
                            return View(principalModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = principalResponseModel.message;
                    responseModel.IsSuccess = principalResponseModel.Succeeded;
                    ViewBag.AddPrincipalError = "Something Went Wrong";
                }
            }
            return View(principalModel);

        }
        /// <summary>
        /// Returns the Principal Details Page - Developed By Harsh Chheda
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> PrincipalDetails()
        {
            var page = 1;
            var size = 5;
            int recsCount = (await _principalRepository.GetAllPrincipalDetails()).data.Count();
            if (page < 1)
                page = 1;
            ViewBag.GetPrincipalById = TempData["GetPrincipalById"] as string;
            var pager = new Pager(recsCount, page, size);
            ViewBag.Pager = pager;
            return View(pager);
        }
        /// <summary>
        /// Returns all the principal list based on the serach parameters - Developed By Harsh Chheda
        /// </summary>
        /// <param name="principalName"></param>
        /// <param name="schoolName"></param>
        /// <param name="createdDate"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<PrincipalDetailsViewModel>> GetPrincipalByFilters(string principalName, string schoolName, DateTime createdDate, bool isActive)
        {
            var data = new List<PrincipalDetailsViewModel>();
            var dataList = await _principalRepository.GetPrincipalByFilters(schoolName, principalName, createdDate, isActive);
            if (dataList.data != null)
            {
                foreach (var principal in dataList.data)
                {
                    data.Add(new PrincipalDetailsViewModel()
                    {
                        Id = principal.Id,
                        Name = principal.Name,
                        AddressLine1 = principal.AddressLine1,
                        AddressLine2 = principal.AddressLine2,
                        CityName = principal.City.CityName,
                        StateName = principal.State.StateName,
                        CreatedDate = principal.CreatedDate,
                        Email = principal.Email,
                        IsActive = principal.IsActive,
                        Mobile = principal.Mobile,
                        SchoolName = principal.School.SchoolName,
                        Username = principal.Username,
                        ZipCode = principal.Zip.Zipcode
                    });
                }
            }
            return data;
        }
        /// <summary>
        /// Returns the list of the principal data - Developed By Harsh Chheda
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<PrincipalDetailsViewModel>> GetAllPrincipalDetails()
        {
            var data = new List<PrincipalDetailsViewModel>();

            var dataList = await _principalRepository.GetAllPrincipalDetails();
            foreach (var principal in dataList.data)
            {
                data.Add(new PrincipalDetailsViewModel()
                {
                    Id = principal.Id,
                    Name = principal.Name,
                    AddressLine1 = principal.AddressLine1,
                    AddressLine2 = principal.AddressLine2,
                    CityName = principal.City.CityName,
                    StateName = principal.State.StateName,
                    CreatedDate = principal.CreatedDate,
                    Email = principal.Email,
                    IsActive = principal.IsActive,
                    Mobile = principal.Mobile,
                    SchoolName = principal.School.SchoolName,
                    Username = principal.Username,
                    ZipCode = principal.Zip.Zipcode
                });
            }
            return data;
        }
        /// <summary>
        /// returns the principal data based on the pagination - Developed By Harsh Chheda
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<PrincipalDetailsViewModel>> GetAllPrincipalDetailsByPagination(int page = 1, int size = 5)
        {
            int recsCount = (await _principalRepository.GetAllPrincipalDetails()).data.Count();
            if (page < 1)
                page = 1;
            var pager = new Pager(recsCount, page, size);

            ViewBag.Pager = pager;
            var data = new List<PrincipalDetailsViewModel>();

            var dataList = await _principalRepository.GetPrincipalByPagination(page, size);

            foreach (var principal in dataList.data.GetPrincipalListPaginationDto)
            {
                data.Add(new PrincipalDetailsViewModel()
                {
                    Id = principal.Id,
                    Name = principal.Name,
                    AddressLine1 = principal.AddressLine1,
                    AddressLine2 = principal.AddressLine2,
                    CityName = principal.City.CityName,
                    StateName = principal.State.StateName,
                    CreatedDate = principal.CreatedDate,
                    Email = principal.Email,
                    IsActive = principal.IsActive,
                    Mobile = principal.Mobile,
                    SchoolName = principal.School.SchoolName,
                    Username = principal.Username,
                    ZipCode = principal.Zip.Zipcode
                });
            }
            return data;
        }
        /// <summary>
        /// returns the principal data based on the id - Developed By Harsh Chheda
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<GetPrincipalByIdResponseModel> GetPrincipalById(int Id)
        {

            var principal = await _principalRepository.GetPrincipalById(Id);
            return principal;
        }
        /// <summary>
        /// returns all the school list -Developed By Harsh Chheda
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<SelectListItem>> GetAllSchool()
        {
            var schools = await _common.GetSchool();
            return schools;
        }
        /// <summary>
        /// returns the principal data for the drop down - Developed By Harsh Chheda
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<SelectListItem>> GetPrincipalName()
        {
            var data = await _principalRepository.GetAllPrincipalDetails();
            List<SelectListItem> principal = new List<SelectListItem>();
            foreach (var item in data.data)
            {
                principal.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = Convert.ToString(item.Id)
                });
            }
            return principal;
        }
        /// <summary>
        /// Returns the ui and the data for the principal for Edit the data - Developed By Harsh Chheda
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> EditPrincipal(int id)
        {
            var principal = await _principalRepository.GetPrincipalById(id);
            if (principal.data == null)
            {
                TempData["GetPrincipalById"] = principal.message;
                return RedirectToAction("PrincipalDetails", "Principal");
            }
            var principalData = new UpdatePrincipalViewModel()
            {
                Id = principal.data.Id,
                Name = principal.data.Name,
                AddressLine1 = principal.data.AddressLine1,
                UserId = principal.data.UserId,
                AddressLine2 = principal.data.AddressLine2,
                CityId = principal.data.City.Id,
                StateId = principal.data.State.Id,
                Email = principal.data.Email,
                IsActive = principal.data.IsActive,
                Mobile = principal.data.Mobile,
                SchoolId = principal.data.School.Id,
                Username = principal.data.Username,
                ZipId = principal.data.Zip.Id
            };
            principalData.Schools = await _common.GetSchool();
            TempData["UserId"] = principalData.UserId;
            principalData.States = await _common.GetAllStates();
            principalData.Cities = await _common.GetAllCityByStateId(principal.data.State.Id);
            principalData.ZipCode = await _common.GetAllZipByCityId(principal.data.Zip.Id);
            return View(principalData);
        }
        /// <summary>
        /// Update the Principal Data -Developed By Harsh Chheda
        /// </summary>
        /// <param name="updatePrincipalViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPrincipal(UpdatePrincipalViewModel updatePrincipalViewModel)
        {
            ViewBag.UpdatePrincipalSuccess = null;
            ViewBag.UpdatePrincipalError = null;
            updatePrincipalViewModel.Schools = await _common.GetSchool();
            updatePrincipalViewModel.States = await _common.GetAllStates();
            updatePrincipalViewModel.Cities = await _common.GetAllCityByStateId(updatePrincipalViewModel.StateId);
            updatePrincipalViewModel.ZipCode = await _common.GetAllZipByCityId(updatePrincipalViewModel.CityId);
            UpdatePrincipalDto updatePrincipal = new UpdatePrincipalDto();
            if (ModelState.IsValid)
            {
                updatePrincipal.Id = updatePrincipalViewModel.Id;
                updatePrincipal.UserId = TempData["UserId"].ToString();
                updatePrincipal.SchoolId = updatePrincipalViewModel.SchoolId;
                updatePrincipal.AddressLine1 = updatePrincipalViewModel.AddressLine1;
                updatePrincipal.AddressLine2 = updatePrincipalViewModel.AddressLine2;
                updatePrincipal.Name = updatePrincipalViewModel.Name;
                updatePrincipal.Email = updatePrincipalViewModel.Email;
                updatePrincipal.Mobile = updatePrincipalViewModel.Mobile;
                updatePrincipal.Username = updatePrincipalViewModel.Username;
                updatePrincipal.CityId = updatePrincipalViewModel.CityId;
                updatePrincipal.StateId = updatePrincipalViewModel.StateId;
                updatePrincipal.ZipId = updatePrincipalViewModel.ZipId;
                updatePrincipal.IsActive = updatePrincipalViewModel.IsActive;


                UpdatePrincipalResponseModel updatePrincipalResponseModel = null;
                ViewBag.UpdatePrincipalSuccess = null;
                ViewBag.UpdatePrincipalError = null;
                ResponseModel responseModel = new ResponseModel();

                updatePrincipalResponseModel = await _principalRepository.UpdatePrincipal(updatePrincipal);


                if (updatePrincipalResponseModel.Succeeded)
                {
                    if (updatePrincipalResponseModel == null && updatePrincipalResponseModel?.data == null)
                    {
                        responseModel.ResponseMessage = updatePrincipalResponseModel.message;
                        responseModel.IsSuccess = updatePrincipalResponseModel.Succeeded;
                    }
                    if (updatePrincipalResponseModel != null)
                    {
                        if (updatePrincipalResponseModel?.data != null)
                        {
                            responseModel.ResponseMessage = updatePrincipalResponseModel.message;
                            responseModel.IsSuccess = updatePrincipalResponseModel.Succeeded;
                            ViewBag.UpdatePrincipalSuccess = "Details Updated Successfully";

                            /*return RedirectToAction("PrincipalDetails", "Principal");*/
                            var page = 1;
                            var size = 5;
                            int recsCount = (await _principalRepository.GetAllPrincipalDetails()).data.Count();
                            if (page < 1)
                                page = 1;
                            ViewBag.GetPrincipalById = TempData["GetPrincipalById"] as string;
                            var pager = new Pager(recsCount, page, size);
                            ViewBag.Pager = pager;
                            return View("PrincipalDetails", pager);
                        }
                        else
                        {
                            responseModel.ResponseMessage = updatePrincipalResponseModel.message;
                            responseModel.IsSuccess = updatePrincipalResponseModel.Succeeded;
                            ViewBag.UpdatePrincipalError = "Something Went Wrong";
                            return View(updatePrincipalResponseModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = updatePrincipalResponseModel.message;
                    responseModel.IsSuccess = updatePrincipalResponseModel.Succeeded;
                    ViewBag.UpdatePrincipalError = "Something Went Wrong";
                }
            }
            return View(updatePrincipalViewModel);
        }
        /// <summary>
        /// Download the Excel file for the principal details -Developed By Harsh Chheda
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DownloadExcel()
        {
            var data = new List<PrincipalDetailsViewModel>();

            var dataList = await _principalRepository.GetAllPrincipalDetails();
            //Creating DataTable  
            DataTable dt = new DataTable();
            //Setiing Table Name  
            dt.TableName = "PrincipalData";
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
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
            dt.Columns.Add("School_Id", typeof(int));
            dt.Columns.Add("School_Name", typeof(string));
            dt.Columns.Add("CreatedDate", typeof(DateTime));
            foreach (var _principal in dataList.data)
            {
                dt.Rows.Add(_principal.Id, _principal.Name, _principal.AddressLine1, _principal.AddressLine2, _principal.Mobile, _principal.Username, _principal.Email, _principal.IsActive ? "Active" : "Inactive", _principal.City.Id, _principal.City.CityName, _principal.State.Id, _principal.State.StateName, _principal.Zip.Id, _principal.Zip.Zipcode, _principal.School.Id, _principal.School.SchoolName, _principal.CreatedDate?.ToShortDateString());
            }
            string fileName = "PrincipalData_" + DateTime.Now.ToShortDateString() + ".xlsx";

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
            var principal = await _principalRepository.GetPrincipalByUserId(userId);
            var principalvm = new ManageProfile()
            {
                ProfileInformation = new ProfileInformation()
                {
                    Mobile = principal.data.Mobile,
                    Name = principal.data.Name,
                    Id = principal.data.Id,
                    RoleName = Convert.ToString(Request.Cookies["RoleName"])
                }
            };
            TempData["CommonId"] = principal.data.Id;
            HttpContext.Session.SetString("ProfilrInformationId", principal.data.Id.ToString());
            return View(principalvm);


        }

        /* public async Task<IActionResult> ManageCircular()
         {
             return View();
         }*/

        [HttpGet]
        public async Task<IActionResult> ManageCircular(int page = 1, int size = 5)
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var principal = await _principalRepository.GetPrincipalByUserId(userId);
            int recsCount = (await _circularRepository.GetAllCircularBySchoolList(principal.data.schoolid)).data.Count();
            if (page < 1)
                page = 1;
            var pager = new Pager(recsCount, page, size);
            ViewBag.Pager = pager;
            ManageCircularModel model = new ManageCircularModel();
            model.Pager = pager;
            model.Schools = await _common.GetSchool();
            model.AddCircular = new AddCircular() { SchoolId = principal.data.schoolid };
            model.EditCircular = new EditCircular() { SchoolId = principal.data.schoolid };
            //model.AddCircular.SchoolId = principal.data.schoolid;
            var paginationData = await _circularRepository.GetCircularListBySchoolPagination(page, size, principal.data.schoolid);
            List<CircularPagination> pagedData = new List<CircularPagination>();
            foreach (var data in paginationData.data)
            {
                pagedData.Add(new CircularPagination()
                {
                    Id = data.Id,
                    CircularTitle = data.CircularTitle,
                    Description = data.Description,
                    SchoolData = new JSC_LMS.Application.Features.Circulars.Queries.GetCircularListByPagination.SchoolDto() { Id = data.SchoolData.Id, SchoolName = data.SchoolData.SchoolName },
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
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var principal = await _principalRepository.GetPrincipalByUserId(userId);
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
                            int recsCount = (await _circularRepository.GetAllCircularBySchoolList(principal.data.schoolid)).data.Count();
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
                            model.AddCircular = new AddCircular() { SchoolId = principal.data.schoolid };
                            model.EditCircular = new EditCircular() { SchoolId = principal.data.schoolid };

                            return View("ManageCircular", model);
                        }
                        else
                        {
                            responseModel.ResponseMessage = addCircularResponseModel.message;
                            responseModel.IsSuccess = addCircularResponseModel.Succeeded;


                            ViewBag.AddCircularError = "Something Went Wrong";
                            return View(manageCircularModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = addCircularResponseModel.message;
                    responseModel.IsSuccess = addCircularResponseModel.Succeeded;
                    ViewBag.AddCircularError = "Something Went Wrong";
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
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var principal = await _principalRepository.GetPrincipalByUserId(userId);
            List<CircularPagination> data = new List<CircularPagination>();
            var dataList = await _circularRepository.GetAllCircularListByFilterAndSchool(circularTitle, description, status, principal.data.schoolid);
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

            model.AddCircular = new AddCircular() { SchoolId = principal.data.schoolid };
            model.EditCircular = new EditCircular() { SchoolId = principal.data.schoolid };

            ViewBag.Pager = model.Pager;
            model.Schools = await _common.GetSchool();
            return View("ManageCircular", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                            var userId = Convert.ToString(Request.Cookies["Id"]);
                            var principal = await _principalRepository.GetPrincipalByUserId(userId);
                            int recsCount = (await _circularRepository.GetAllCircularBySchoolList(principal.data.schoolid)).data.Count();
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

                            model.AddCircular = new AddCircular() { SchoolId = principal.data.schoolid };
                            model.EditCircular = new EditCircular() { SchoolId = principal.data.schoolid };

                            model.CircularListPagination = pagedData;
                            return View("ManageCircular", model);
                        }
                        else
                        {
                            responseModel.ResponseMessage = updateCircularResponseModel.message;
                            responseModel.IsSuccess = updateCircularResponseModel.Succeeded;
                            ViewBag.UpdateCircularError = "Something Went Wrong";
                            return View(manageCircularModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = updateCircularResponseModel.message;
                    responseModel.IsSuccess = updateCircularResponseModel.Succeeded;
                    ViewBag.UpdateCircularError = "Something Went Wrong";
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
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var principal = await _principalRepository.GetPrincipalByUserId(userId);
            int recsCount = (await _announcementRepository.GetAllAnnouncementBySchoolList(principal.data.schoolid)).data.Count();
            if (page < 1)
                page = 1;
            var pager = new Pager(recsCount, page, size);
            ViewBag.Pager = pager;
            ManageAnnouncementModel model = new ManageAnnouncementModel();
            model.Pager = pager;
            model.Classes = await _common.GetClass();
            model.Sections = await _common.GetSection();
            model.Subjects = await _common.GetSubject();
            model.Teachers = await GetTeacherName();
            var paginationData = await _announcementRepository.GetAnnouncementListBySchoolPagination(page, size, principal.data.schoolid);
            List<AnnouncementPagination> pagedData = new List<AnnouncementPagination>();
            foreach (var data in paginationData.data)
            {
                pagedData.Add(new AnnouncementPagination()
                {
                    Id = data.Id,
                    AnnouncementTitle = data.AnnouncementTitle,
                    AnnouncementContent = data.AnnouncementContent,
                    Class = new JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination.ClassDto() { Id = data.Class.Id, ClassName = data.Class.ClassName },
                    Section = new JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination.SectionDto() { Id = data.Section.Id, SectionName = data.Section.SectionName },
                    Subject = new JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination.SubjectDto() { Id = data.Subject.Id, SubjectName = data.Subject.SubjectName },
                    Teacher = new JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination.TeacherDto() { Id = data.Teacher.Id, TeacherName = data.Teacher.TeacherName },
                    CreatedDate = data.CreatedDate,


                });
            }
            model.AnnouncementPagination = pagedData;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SearchAnnouncement(int ClassId, int SectionId, int SubjectId, string TeacherName, DateTime CreatedDate)
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var principal = await _principalRepository.GetPrincipalByUserId(userId);
            if (TeacherName == null)
            {
                TeacherName = "Select Teacher";
            }
            List<AnnouncementPagination> data = new List<AnnouncementPagination>();
            var dataList = await _announcementRepository.GetAnnouncementByFilters(principal.data.schoolid, ClassId, SectionId, SubjectId, TeacherName, "Select Type", null, null, CreatedDate);
            if (dataList.data != null)
            {
                foreach (var d in dataList.data)
                {
                    data.Add(new AnnouncementPagination()
                    {
                        Id = d.Id,
                        AnnouncementTitle = d.AnnouncementTitle,
                        AnnouncementContent = d.AnnouncementContent,
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
    }
    #endregion
}
