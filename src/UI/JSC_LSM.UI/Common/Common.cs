using JSC_LSM.UI.ResponseModels;
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
        private readonly ICategoryRepository _categoryRepository;
        private readonly IStateRepository _stateRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IZipRepository _zipRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;

        public Common(ICategoryRepository categoryRepository, IStateRepository stateRepository, ICityRepository cityRepository, ISectionRepository sectionRepository, IClassRepository classRepository, IZipRepository zipRepository, ISchoolRepository schoolRepository, ISubjectRepository subjectRepository, ITeacherRepository teacherRepository)
        {
            _categoryRepository = categoryRepository;
            _stateRepository = stateRepository;
            _cityRepository = cityRepository;
            _zipRepository = zipRepository;
            _schoolRepository = schoolRepository;
            _classRepository = classRepository;
            _sectionRepository = sectionRepository;
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
        }
        [NonAction]
        public async Task<List<SelectListItem>> GetAllCategory()
        {
            List<SelectListItem> category = new List<SelectListItem>();
            GetAllCategoryResponseModel getAllCategoryResponseModel = null;
            ResponseModel responseModel = new ResponseModel();
            getAllCategoryResponseModel = await _categoryRepository.GetAllCategory();

            if (getAllCategoryResponseModel.isSuccess)
            {
                if (getAllCategoryResponseModel == null && getAllCategoryResponseModel.data == null)
                {
                    responseModel.ResponseMessage = getAllCategoryResponseModel.message;
                    responseModel.IsSuccess = getAllCategoryResponseModel.isSuccess;
                }
                if (getAllCategoryResponseModel != null)
                {
                    //User user = authenticationResponseModel.userDetail;
                    responseModel.ResponseMessage = getAllCategoryResponseModel.message;
                    responseModel.IsSuccess = getAllCategoryResponseModel.isSuccess;
                    foreach (var item in getAllCategoryResponseModel.data)
                    {
                        category.Add(new SelectListItem
                        {
                            Text = item.CategoryName,
                            Value = Convert.ToString(item.Id)
                        });
                    }
                    return category;
                }
            }
            else
            {
                responseModel.ResponseMessage = getAllCategoryResponseModel.message;
                responseModel.IsSuccess = getAllCategoryResponseModel.isSuccess;
            }
            return null;
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

        public async Task<List<SelectListItem>> GetClass()
        {
            List<SelectListItem> classes = new List<SelectListItem>();
            GetAllClassResponseModel getAllClassResponseModel = null;
            ResponseModel responseModel = new ResponseModel();
            getAllClassResponseModel = await _classRepository.GetAllClass();

            if (getAllClassResponseModel.isSuccess)
            {
                if (getAllClassResponseModel == null && getAllClassResponseModel.data == null)
                {
                    responseModel.ResponseMessage = getAllClassResponseModel.message;
                    responseModel.IsSuccess = getAllClassResponseModel.isSuccess;
                }
                if (getAllClassResponseModel != null)
                {
                    //User user = authenticationResponseModel.userDetail;
                    responseModel.ResponseMessage = getAllClassResponseModel.message;
                    responseModel.IsSuccess = getAllClassResponseModel.isSuccess;
                    foreach (var item in getAllClassResponseModel.data)
                    {
                        classes.Add(new SelectListItem
                        {
                            Text = item.ClassName,
                            Value = Convert.ToString(item.Id)
                        });
                    }
                    return classes;
                }
            }
            else
            {
                responseModel.ResponseMessage = getAllClassResponseModel.message;
                responseModel.IsSuccess = getAllClassResponseModel.isSuccess;
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

        public async Task<List<SelectListItem>> GetSection()
        {
            List<SelectListItem> section = new List<SelectListItem>();
            GetAllSectionResponseModel getAllSectionResponseModel = null;
            ResponseModel responseModel = new ResponseModel();
            getAllSectionResponseModel = await _sectionRepository.GetAllSection();

            if (getAllSectionResponseModel.isSuccess)
            {
                if (getAllSectionResponseModel == null && getAllSectionResponseModel.data == null)
                {
                    responseModel.ResponseMessage = getAllSectionResponseModel.message;
                    responseModel.IsSuccess = getAllSectionResponseModel.isSuccess;
                }
                if (getAllSectionResponseModel != null)
                {
                    //User user = authenticationResponseModel.userDetail;
                    responseModel.ResponseMessage = getAllSectionResponseModel.message;
                    responseModel.IsSuccess = getAllSectionResponseModel.isSuccess;
                    foreach (var item in getAllSectionResponseModel.data)
                    {
                        section.Add(new SelectListItem
                        {
                            Text = item.SectionName,
                            Value = Convert.ToString(item.Id)
                        });
                    }
                    return section;
                }
            }
            else
            {
                responseModel.ResponseMessage = getAllSectionResponseModel.message;
                responseModel.IsSuccess = getAllSectionResponseModel.isSuccess;
            }
            return null;
        }
        public async Task<List<SelectListItem>> GetSubject()
        {
            List<SelectListItem> subject = new List<SelectListItem>();
            GetAllSubjectListResponseModel getAllSubjectResponseModel = null;
            ResponseModel responseModel = new ResponseModel();
            getAllSubjectResponseModel = await _subjectRepository.GetAllSubjectDetails();

            if (getAllSubjectResponseModel.Succeeded)
            {
                if (getAllSubjectResponseModel == null && getAllSubjectResponseModel.data == null)
                {
                    responseModel.ResponseMessage = getAllSubjectResponseModel.message;
                    responseModel.IsSuccess = getAllSubjectResponseModel.isSuccess;
                }
                if (getAllSubjectResponseModel != null)
                {
                    //User user = authenticationResponseModel.userDetail;
                    responseModel.ResponseMessage = getAllSubjectResponseModel.message;
                    responseModel.IsSuccess = getAllSubjectResponseModel.isSuccess;
                    foreach (var item in getAllSubjectResponseModel.data)
                    {
                        subject.Add(new SelectListItem
                        {
                            Text = item.SubjectName,
                            Value = Convert.ToString(item.Id)
                        });
                    }
                    return subject;
                }
            }
            else
            {
                responseModel.ResponseMessage = getAllSubjectResponseModel.message;
                responseModel.IsSuccess = getAllSubjectResponseModel.isSuccess;
            }
            return null;
        }
        public async Task<List<SelectListItem>> GetTeacher()
        {
            List<SelectListItem> teacher = new List<SelectListItem>();
            GetAllTeacherListResponseModel getAllTeacherResponseModel = null;
            ResponseModel responseModel = new ResponseModel();
            getAllTeacherResponseModel = await _teacherRepository.GetAllTeacherDetails();

            if (getAllTeacherResponseModel.Succeeded)
            {
                if (getAllTeacherResponseModel == null && getAllTeacherResponseModel.data == null)
                {
                    responseModel.ResponseMessage = getAllTeacherResponseModel.message;
                    responseModel.IsSuccess = getAllTeacherResponseModel.isSuccess;
                }
                if (getAllTeacherResponseModel != null)
                {
                    //User user = authenticationResponseModel.userDetail;
                    responseModel.ResponseMessage = getAllTeacherResponseModel.message;
                    responseModel.IsSuccess = getAllTeacherResponseModel.isSuccess;
                    foreach (var item in getAllTeacherResponseModel.data)
                    {
                        teacher.Add(new SelectListItem
                        {
                            Text = item.TeacherName,
                            Value = Convert.ToString(item.Id)
                        });
                    }
                    return teacher;
                }
            }
            else
            {
                responseModel.ResponseMessage = getAllTeacherResponseModel.message;
                responseModel.IsSuccess = getAllTeacherResponseModel.isSuccess;
            }
            return null;
        }








    }
}
