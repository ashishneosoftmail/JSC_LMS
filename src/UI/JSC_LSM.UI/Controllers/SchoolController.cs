using ClosedXML.Excel;
using JSC_LMS.Application.Features.School.Commands.CreateSchool;
using JSC_LMS.Application.Features.School.Commands.UpdateSchool;
using JSC_LSM.UI.Models;

using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.ResponseModels.SchoolResponseModels;
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
    public class SchoolController : BaseController
    {

        private readonly JSC_LSM.UI.Common.Common _common;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IInstituteRepository _instituteRepository;

        public SchoolController(JSC_LSM.UI.Common.Common common, ISchoolRepository schoolRepository, IInstituteRepository instituteRepository)
        {
            _common = common;
            _schoolRepository = schoolRepository;
            _instituteRepository = instituteRepository;
        }

        [HttpGet]
        public async Task<List<SelectListItem>> GetAllState()
        {
            var states = await _common.GetAllStates();
            return states;
        }


        [HttpGet]

        public async Task<IActionResult> AddSchool()
        {
          SchoolModel schoolModel = new SchoolModel();
            schoolModel.States = await _common.GetAllStates();
            schoolModel.Institutes = await _common.GetInstitute();
            return View(schoolModel);
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
        public async Task<IActionResult> AddSchool(SchoolModel schoolModel)
        {
            ViewBag.AddSchoolSuccess = null;
            ViewBag.AddSchoolError = null;
            schoolModel.States = await _common.GetAllStates();
            schoolModel.Institutes = await _common.GetInstitute();
            CreateSchoolDto createNewSchool = new CreateSchoolDto();
       
            if (ModelState.IsValid)
            {

                createNewSchool.InstituteId = schoolModel.InstituteId;
                createNewSchool.Address1 = schoolModel.Address1;
                createNewSchool.Address2 = schoolModel.Address2;
                createNewSchool.SchoolName = schoolModel.SchoolName;
                createNewSchool.Email = schoolModel.Email;
                createNewSchool.Mobile = schoolModel.Mobile;
                createNewSchool.ContactPerson = schoolModel.ContactPerson;
                createNewSchool.CityId = schoolModel.CityId;
                createNewSchool.StateId = schoolModel.StateId;
                createNewSchool.ZipId = schoolModel.ZipId;
                createNewSchool.IsActive = schoolModel.IsActive;
               


               SchoolResponseModel schoolResponseModel = null;
                ViewBag.AddSchoolSuccess = null;
                ViewBag.AddSchoolError = null;
                ResponseModel responseModel = new ResponseModel();

                schoolResponseModel = await _schoolRepository.AddNewSchool(createNewSchool);


                if (schoolResponseModel.Succeeded)
                {
                    if (schoolResponseModel == null && schoolResponseModel.data == null)
                    {
                        responseModel.ResponseMessage = schoolResponseModel.message;
                        responseModel.IsSuccess = schoolResponseModel.Succeeded;
                    }
                    if (schoolResponseModel != null)
                    {
                        if (schoolResponseModel.data != null)
                        {
                            responseModel.ResponseMessage = schoolResponseModel.message;
                            responseModel.IsSuccess = schoolResponseModel.Succeeded;
                            ViewBag.AddSchoolSuccess = "Details Added Successfully";
                            ModelState.Clear();
                            var newPrincipalModel = new SchoolModel();
                            newPrincipalModel.States = await _common.GetAllStates();
                            newPrincipalModel.Institutes = await _common.GetInstitute();
                            var page = 1;
                            var size = 5;
                            int recsCount = (await _schoolRepository.GetAllSchool()).data.Count();
                            if (page < 1)
                                page = 1;
                            ViewBag.GetSchoolById = TempData["GetSchoolById"] as string;
                            var pager = new Pager(recsCount, page, size);
                            ViewBag.Pager = pager;
                            return RedirectToAction("SchoolDetails", pager);
                        }
                        else
                        {
                            responseModel.ResponseMessage = schoolResponseModel.message;
                            responseModel.IsSuccess = schoolResponseModel.Succeeded;
                            ViewBag.AddSchoolError = schoolResponseModel.message;
                            return View(schoolModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = schoolResponseModel.message;
                    responseModel.IsSuccess = schoolResponseModel.Succeeded;
                    ViewBag.AddSchoolError = schoolResponseModel.message;
                }
            }
            return View(schoolModel);

        }

        [HttpGet]
        public async Task<IActionResult> SchoolDetails()
        {
            var page = 1;
            var size = 5;
            int recsCount = (await _schoolRepository.GetAllSchool()).data.Count();
            if (page < 1)
                page = 1;
            ViewBag.GetSchoolById = TempData["GetSchoolById"] as string;
            var pager = new Pager(recsCount, page, size);
            ViewBag.Pager = pager;
            return View(pager);
        }

       
        public async Task<IEnumerable<SchoolViewModel>> GetSchoolByFilter(string schoolName, string instituteName, string city,string state,bool isActive ,DateTime createdDate)
        {
            var data = new List<SchoolViewModel>();
            var dataList = await _schoolRepository.GetSchoolByFilter(schoolName, instituteName,city,state, isActive, createdDate);
            if (dataList.data != null)
            {
                foreach (var school in dataList.data)
                {
                    data.Add(new SchoolViewModel()
                    {
                        Id = school.Id,
                        Name = school.SchoolName,
                        AddressLine1 = school.AddressLine1,
                        AddressLine2 = school.AddressLine2,
                        CityName = school.City.CityName,
                        StateName = school.State.StateName,
                        CreatedDate = school.CreatedDate,
                        Email = school.Email,
                        IsActive = school.IsActive,
                        Mobile = school.Mobile,
                        InstituteName = school.Institute.InstituteName,
                        ZipCode = school.Zip.Zipcode,
                    });
                }
            }
            return data;
        }

       

        [HttpGet]
        public async Task<IEnumerable<SchoolViewModel>> GetAllSchoolDetailsByPagination(int page = 1, int size = 5)
        {
            int recsCount = (await _schoolRepository.GetAllSchool()).data.Count();
            if (page < 1)
                page = 1;
            var pager = new Pager(recsCount, page, size);

            ViewBag.Pager = pager;
            var data = new List<SchoolViewModel>();

            var dataList = await _schoolRepository.GetSchoolByPagination(page, size);

            foreach (var school in dataList.data.GetSchoolByPaginationDto)
            {
                data.Add(new SchoolViewModel()
                {
                    Id = school.Id,
                    Name = school.SchoolName,
                    AddressLine1 = school.AddressLine1,
                    AddressLine2 = school.AddressLine2,
                    CityName = school.City.CityName,
                    StateName = school.State.StateName,
                    CreatedDate = school.CreatedDate,
                    Email = school.Email,
                    IsActive = school.IsActive,
                    Mobile = school.Mobile,
                    InstituteName = school.Institute.InstituteName,
                      ContactPerson=school.ContactPerson,
                    ZipCode = school.Zip.Zipcode
                });
            }
            return data;
        }

        [HttpGet]
        public async Task<GetSchoolByIdResponseModel> GetSchoolById(int Id)
        {

            var school = await _schoolRepository.GetSchoolById(Id);
            return school;
        }

        [HttpGet]
        public async Task<List<SelectListItem>> GetAllInstitute()
        {
            var institutes = await _common.GetInstitute();
            return institutes;
        }

        [HttpGet]
        public async Task<List<SelectListItem>> GetSchoolName()
        {
            var data = await _schoolRepository.GetAllSchool();
            List<SelectListItem> school = new List<SelectListItem>();
            foreach (var item in data.data)
            {
                school.Add(new SelectListItem
                {
                    Text = item.SchoolName,
                    Value = Convert.ToString(item.Id)
                });
            }
            return school;
        }

        [HttpGet]
        public async Task<IActionResult> EditSchool(int id)
        {
            var school = await _schoolRepository.GetSchoolById(id);
            if (school.data == null)
            {
                TempData["GetSchoolById"] = school.message;
                return RedirectToAction("SchoolDetails", "School");
            }
            var schoolData = new UpdateSchoolViewModel()
            {
                Id = school.data.Id,
                SchoolName = school.data.SchoolName,
                Address1 = school.data.AddressLine1,
          
                Address2 = school.data.AddressLine2,
                CityId = school.data.City.Id,
                StateId = school.data.State.Id,
                Email = school.data.Email,
                IsActive = school.data.IsActive,
                Mobile = school.data.Mobile,
               InstituteId= school.data.Institute.Id,
               ContactPerson=school.data.ContactPerson,
              
                ZipId = school.data.Zip.Id
            };
            schoolData.Institutes = await _common.GetInstitute();

            schoolData.States = await _common.GetAllStates();
            schoolData.Cities = await _common.GetAllCityByStateId(school.data.State.Id);
            schoolData.ZipCode = await _common.GetAllZipByCityId(school.data.Zip.Id);
            return View(schoolData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSchool(UpdateSchoolViewModel updateSchoolViewModel)
        {
            ViewBag.UpdateSchoolSuccess = null;
            ViewBag.UpdateSchoolError = null;
            updateSchoolViewModel.Institutes = await _common.GetInstitute();
            updateSchoolViewModel.States = await _common.GetAllStates();
            updateSchoolViewModel.Cities = await _common.GetAllCityByStateId(updateSchoolViewModel.StateId);
            updateSchoolViewModel.ZipCode = await _common.GetAllZipByCityId(updateSchoolViewModel.CityId);
            UpdateSchoolDto updatePrincipal = new UpdateSchoolDto();
            if (ModelState.IsValid)
            {
                updatePrincipal.Id = updateSchoolViewModel.Id;
               
                updatePrincipal.InstituteId = updateSchoolViewModel.InstituteId;
                updatePrincipal.Address1 = updateSchoolViewModel.Address1;
                updatePrincipal.Address2 = updateSchoolViewModel.Address2;
                updatePrincipal.SchoolName = updateSchoolViewModel.SchoolName;
                updatePrincipal.Email = updateSchoolViewModel.Email;
                updatePrincipal.Mobile = updateSchoolViewModel.Mobile;
                updatePrincipal.ContactPerson = updateSchoolViewModel.ContactPerson;
                updatePrincipal.CityId = updateSchoolViewModel.CityId;
                updatePrincipal.StateId = updateSchoolViewModel.StateId;
                updatePrincipal.ZipId = updateSchoolViewModel.ZipId;
                updatePrincipal.IsActive = updateSchoolViewModel.IsActive;


                UpdateSchoolResponseModel updateSchoolResponseModel = null;
                ViewBag.UpdateSchoolSuccess = null;
                ViewBag.UpdateSchoolError = null;
                ResponseModel responseModel = new ResponseModel();

                updateSchoolResponseModel = await _schoolRepository.UpdateSchool(updatePrincipal);


                if (updateSchoolResponseModel.Succeeded)
                {
                    if (updateSchoolResponseModel == null && updateSchoolResponseModel?.data == null)
                    {
                        responseModel.ResponseMessage = updateSchoolResponseModel.message;
                        responseModel.IsSuccess = updateSchoolResponseModel.Succeeded;
                    }
                    if (updateSchoolResponseModel != null)
                    {
                        if (updateSchoolResponseModel?.data != null)
                        {
                            responseModel.ResponseMessage = updateSchoolResponseModel.message;
                            responseModel.IsSuccess = updateSchoolResponseModel.Succeeded;
                            ViewBag.UpdateSchoolSuccess = "Details Updated Successfully";

                            var page = 1;
                            var size = 5;
                            int recsCount = (await _schoolRepository.GetAllSchool()).data.Count();
                            if (page < 1)
                                page = 1;
                            ViewBag.GetSchoolById = TempData["GetSchoolById"] as string;
                            var pager = new Pager(recsCount, page, size);
                            ViewBag.Pager = pager;
                            return RedirectToAction("SchoolDetails", pager);
                        }
                        else
                        {
                            responseModel.ResponseMessage = updateSchoolResponseModel.message;
                            responseModel.IsSuccess = updateSchoolResponseModel.Succeeded;
                            ViewBag.UpdateSchoolError = updateSchoolResponseModel.message;
                            return View(updateSchoolResponseModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = updateSchoolResponseModel.message;
                    responseModel.IsSuccess = updateSchoolResponseModel.Succeeded;
                    ViewBag.UpdatePrincipalError = updateSchoolResponseModel.message;
                }
            }
            return View(updateSchoolViewModel);
        }



     

        }



    }



