using ClosedXML.Excel;
using JSC_LMS.Application.Features.Principal.Commands.CreatePrincipal;
using JSC_LMS.Application.Features.Principal.Commands.UpdatePrincipal;
using JSC_LSM.UI.Models;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        /// <summary>
        /// Constructor For the PrincipalController - Developed By Harsh Chheda
        /// </summary>
        /// <param name="common"></param>
        /// <param name="schoolRepository"></param>
        /// <param name="principalRepository"></param>
        public PrincipalController(JSC_LSM.UI.Common.Common common, ISchoolRepository schoolRepository, IPrincipalRepository principalRepository)
        {
            _common = common;
            _schoolRepository = schoolRepository;
            _principalRepository = principalRepository;
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
                            return RedirectToAction("PrincipalDetails", "Principal");
                        }
                        else
                        {
                            responseModel.ResponseMessage = principalResponseModel.message;
                            responseModel.IsSuccess = principalResponseModel.Succeeded;
                            ViewBag.AddPrincipalError = principalResponseModel.message;
                            return View(principalModel);
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
                updatePrincipal.Password = updatePrincipalViewModel.Password;
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

                            return RedirectToAction("PrincipalDetails", "Principal");
                        }
                        else
                        {
                            responseModel.ResponseMessage = updatePrincipalResponseModel.message;
                            responseModel.IsSuccess = updatePrincipalResponseModel.Succeeded;
                            ViewBag.UpdatePrincipalError = updatePrincipalResponseModel.message;
                            return View(updatePrincipalResponseModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = updatePrincipalResponseModel.message;
                    responseModel.IsSuccess = updatePrincipalResponseModel.Succeeded;
                    ViewBag.UpdatePrincipalError = updatePrincipalResponseModel.message;
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
    }
    #endregion
}
