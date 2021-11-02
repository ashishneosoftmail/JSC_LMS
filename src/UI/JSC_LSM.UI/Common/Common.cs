﻿using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Common
{
    public class Common
    {
        private readonly IStateRepository _stateRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IZipRepository _zipRepository;
        private readonly ISchoolRepository _schoolRepository;
        public Common(IStateRepository stateRepository, ICityRepository cityRepository, IZipRepository zipRepository, ISchoolRepository schoolRepository)
        {
            _stateRepository = stateRepository;
            _cityRepository = cityRepository;
            _zipRepository = zipRepository;
            _schoolRepository = schoolRepository;
        }

        [NonAction]
        public async Task<List<SelectListItem>> GetAllStates()
        {
            List<SelectListItem> state = new List<SelectListItem>();
            GetAllStatesResponseModel getAllStateResponseModel = null;
            ResponseModel responseModel = new ResponseModel();
            getAllStateResponseModel = await _stateRepository.GetAllStates();

            if (getAllStateResponseModel.isSuccess)
            {
                if (getAllStateResponseModel == null && getAllStateResponseModel.data == null)
                {
                    responseModel.ResponseMessage = getAllStateResponseModel.message;
                    responseModel.IsSuccess = getAllStateResponseModel.isSuccess;
                }
                if (getAllStateResponseModel != null)
                {
                    //User user = authenticationResponseModel.userDetail;
                    responseModel.ResponseMessage = getAllStateResponseModel.message;
                    responseModel.IsSuccess = getAllStateResponseModel.isSuccess;
                    foreach (var item in getAllStateResponseModel.data)
                    {
                        state.Add(new SelectListItem
                        {
                            Text = item.StateName,
                            Value = Convert.ToString(item.Id)
                        });
                    }
                    return state;
                }
            }
            else
            {
                responseModel.ResponseMessage = getAllStateResponseModel.message;
                responseModel.IsSuccess = getAllStateResponseModel.isSuccess;
            }
            return null;
        }
        [NonAction]
        public async Task<List<SelectListItem>> GetAllCityByStateId(int id)
        {
            List<SelectListItem> cities = new List<SelectListItem>();
            GetAllCitiesResponseModel getAllCititesResponseModel = null;
            ResponseModel responseModel = new ResponseModel();
            getAllCititesResponseModel = await _cityRepository.GetAllCities(id);

            if (getAllCititesResponseModel.isSuccess)
            {
                if (getAllCititesResponseModel == null && getAllCititesResponseModel.data == null)
                {
                    responseModel.ResponseMessage = getAllCititesResponseModel.message;
                    responseModel.IsSuccess = getAllCititesResponseModel.isSuccess;
                }
                if (getAllCititesResponseModel != null)
                {
                    //User user = authenticationResponseModel.userDetail;
                    responseModel.ResponseMessage = getAllCititesResponseModel.message;
                    responseModel.IsSuccess = getAllCititesResponseModel.isSuccess;
                    foreach (var item in getAllCititesResponseModel.data)
                    {
                        cities.Add(new SelectListItem
                        {
                            Text = item.CityName,
                            Value = Convert.ToString(item.Id)
                        });
                    }
                    return cities;
                }
            }
            else
            {
                responseModel.ResponseMessage = getAllCititesResponseModel.message;
                responseModel.IsSuccess = getAllCititesResponseModel.isSuccess;
            }
            return null;
        }
        [NonAction]
        public async Task<List<SelectListItem>> GetAllZipByCityId(int cityId)
        {
            List<SelectListItem> zip = new List<SelectListItem>();
            GetAllZipResponseModel getAllZipResponse = null;
            ResponseModel responseModel = new ResponseModel();
            getAllZipResponse = await _zipRepository.GetAllZipcodes(cityId);

            if (getAllZipResponse.isSuccess)
            {
                if (getAllZipResponse == null && getAllZipResponse.data == null)
                {
                    responseModel.ResponseMessage = getAllZipResponse.message;
                    responseModel.IsSuccess = getAllZipResponse.isSuccess;
                }
                if (getAllZipResponse != null)
                {
                    //User user = authenticationResponseModel.userDetail;
                    responseModel.ResponseMessage = getAllZipResponse.message;
                    responseModel.IsSuccess = getAllZipResponse.isSuccess;
                    foreach (var item in getAllZipResponse.data)
                    {
                        zip.Add(new SelectListItem
                        {
                            Text = item.ZipCode,
                            Value = Convert.ToString(item.Id)
                        });
                    }
                    return zip;
                }
            }
            else
            {
                responseModel.ResponseMessage = getAllZipResponse.message;
                responseModel.IsSuccess = getAllZipResponse.isSuccess;
            }
            return null;
        }

        public async Task<List<SelectListItem>> GetSchool()
        {
            List<SelectListItem> school = new List<SelectListItem>();
            GetAllSchoolResponseModel getAllSchoolResponseModel = null;
            ResponseModel responseModel = new ResponseModel();
            getAllSchoolResponseModel = await _schoolRepository.GetAllSchool();

            if (getAllSchoolResponseModel.isSuccess)
            {
                if (getAllSchoolResponseModel == null && getAllSchoolResponseModel.data == null)
                {
                    responseModel.ResponseMessage = getAllSchoolResponseModel.message;
                    responseModel.IsSuccess = getAllSchoolResponseModel.isSuccess;
                }
                if (getAllSchoolResponseModel != null)
                {
                    //User user = authenticationResponseModel.userDetail;
                    responseModel.ResponseMessage = getAllSchoolResponseModel.message;
                    responseModel.IsSuccess = getAllSchoolResponseModel.isSuccess;
                    foreach (var item in getAllSchoolResponseModel.data)
                    {
                        school.Add(new SelectListItem
                        {
                            Text = item.SchoolName,
                            Value = Convert.ToString(item.Id)
                        });
                    }
                    return school;
                }
            }
            else
            {
                responseModel.ResponseMessage = getAllSchoolResponseModel.message;
                responseModel.IsSuccess = getAllSchoolResponseModel.isSuccess;
            }
            return null;
        }
    }
}