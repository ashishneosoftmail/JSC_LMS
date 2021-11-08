﻿using JSC_LMS.Application.Features.Institutes.Commands.CreateInstitute;
using JSC_LSM.UI.Helpers;
using JSC_LSM.UI.Models;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Controllers
{
    public class InstituteController : BaseController
    {
        /* JSC_LSM.UI.Common.Common _common;
        public InstituteController(JSC_LSM.UI.Common.Common common)
        {
            _common = common;
        }*/
        private readonly IStateRepository _stateRepository;
        private readonly IInstituteRepository _instituteRepository;
        private readonly JSC_LSM.UI.Common.Common _common;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        public InstituteController(IStateRepository stateRepository, JSC_LSM.UI.Common.Common common, IOptions<ApiBaseUrl> apiBaseUrl, IInstituteRepository instituteRepository)
        {
            _stateRepository = stateRepository;
            _instituteRepository = instituteRepository;
            _common = common;
            _apiBaseUrl = apiBaseUrl;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> InstituteDetails()
        {
            var page = 1;
            var size = 5;
            int recsCount = (await _instituteRepository.GetAllInstituteDetails()).data.Count();
            if (page < 1)
                page = 1;

            var pager = new Pager(recsCount, page, size);
            ViewBag.Pager = pager;
            return View(pager);
        }

        [HttpGet]
        public async Task<List<SelectListItem>> GetAllState()
        {
            var states = await _common.GetAllStates();
            return states;
        }
        public async Task<List<SelectListItem>> GetCityByStateId(int id)
        {
            var cities = await _common.GetAllCityByStateId(id);
            return cities;
        }
        public async Task<List<SelectListItem>> GetZipByCityId(int cityId)
        {
            var cities = await _common.GetAllZipByCityId(cityId);
            return cities;
        }

        [HttpGet]
        public async Task<IActionResult> AddInstitute()
        {
            ViewBag.AddInstituteSuccess = null;
            ViewBag.AddInstituteError = null;
            Institute institute = new Institute();
            institute.States = await _common.GetAllStates();
            return View(institute);
        }

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
                createNewInstitute.Email = institute.Email;
                createNewInstitute.Mobile = institute.Mobile;
                createNewInstitute.Password = institute.Password;
                createNewInstitute.Username = institute.Username;
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
                            ModelState.Clear();
                            var newInstituteModel = new Institute();
                            newInstituteModel.States = await _common.GetAllStates();                          
                            return View(newInstituteModel);

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

        [HttpGet]
        public async Task<GetInstituteByIdResponseModel> GetInstituteById(int Id)
        {

            var institute = await _instituteRepository.GetInstituteById(Id);
            return institute;
        }
        [HttpGet]
        public async Task<IEnumerable<InstituteDetailsViewModel>> GetInstituteByFilters(string instituteName, string city, string state, bool isActive, DateTime licenseExpiry)
        {
            var data = new List<InstituteDetailsViewModel>();
           
            var dataList = await _instituteRepository.GetInstituteByFilters(instituteName, city,state, licenseExpiry, isActive);
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
    }
}
