using JSC_LSM.UI.Helpers;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.ResponseModels.EventsResponseModel;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IInstituteRepository _instituteRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IParentsRepository _parentsRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IFeedbackTitleRepository _feedbackTitleRepository;

        private readonly IEventsDetailsRepository _eventRepository;
 
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;

        public Common(ICategoryRepository categoryRepository, IStateRepository stateRepository, ICityRepository cityRepository, ISectionRepository sectionRepository, IClassRepository classRepository, IZipRepository zipRepository, ISchoolRepository schoolRepository, ISubjectRepository subjectRepository, ITeacherRepository teacherRepository, IInstituteRepository instituteRepository, IEventsDetailsRepository eventRepository, IStudentRepository studentRepository, IParentsRepository parentsRepository,IWebHostEnvironment webHostEnvironment, IConfiguration configuration,IFeedbackTitleRepository feedbackTitleRepository, IOptions<ApiBaseUrl> apiBaseUrl)
        {
            _categoryRepository = categoryRepository;
            _stateRepository = stateRepository;
            _cityRepository = cityRepository;
            _zipRepository = zipRepository;
         
            _schoolRepository = schoolRepository;
            _instituteRepository = instituteRepository;
            _classRepository = classRepository;
            _sectionRepository = sectionRepository;
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
            _eventRepository = eventRepository;
            _studentRepository = studentRepository;
            _parentsRepository = parentsRepository;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _apiBaseUrl = apiBaseUrl;
            _feedbackTitleRepository = feedbackTitleRepository;
        }
        [NonAction]
        public string ProcessUploadFile(IFormFile formFile, string path)
        {
            string uniqueFileName = null;
            if (formFile != null)
            {
                if (!Directory.Exists(_webHostEnvironment.WebRootPath + path))
                {
                    Directory.CreateDirectory(_webHostEnvironment.WebRootPath + path);
                }

                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath + path);
                uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        [NonAction]
        public async Task<List<SelectListItem>> GetAllCategory()
        {
            List<SelectListItem> category = new List<SelectListItem>();
            GetAllCategoryResponseModel getAllCategoryResponseModel = null;
            ResponseModel responseModel = new ResponseModel();
            getAllCategoryResponseModel = await _categoryRepository.GetAllCategory(_apiBaseUrl.Value.LmsApiBaseUrl);

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
            getAllStateResponseModel = await _stateRepository.GetAllStates(_apiBaseUrl.Value.LmsApiBaseUrl);

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
            getAllCititesResponseModel = await _cityRepository.GetAllCities(_apiBaseUrl.Value.LmsApiBaseUrl,id);

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
            getAllZipResponse = await _zipRepository.GetAllZipcodes(_apiBaseUrl.Value.LmsApiBaseUrl,cityId);

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
            getAllClassResponseModel = await _classRepository.GetAllClass(_apiBaseUrl.Value.LmsApiBaseUrl);

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
            getAllSchoolResponseModel = await _schoolRepository.GetAllSchool(_apiBaseUrl.Value.LmsApiBaseUrl);

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
            getAllSectionResponseModel = await _sectionRepository.GetAllSection(_apiBaseUrl.Value.LmsApiBaseUrl);

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
            getAllSubjectResponseModel = await _subjectRepository.GetAllSubjectDetails(_apiBaseUrl.Value.LmsApiBaseUrl);

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
            getAllTeacherResponseModel = await _teacherRepository.GetAllTeacherDetails(_apiBaseUrl.Value.LmsApiBaseUrl);

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

        public async Task<List<SelectListItem>> GetInstitute()
        {
            List<SelectListItem> subject = new List<SelectListItem>();
            GetAllInstituteListResponseModel getAllInstituteResponseModel = null;
            ResponseModel responseModel = new ResponseModel();
            getAllInstituteResponseModel = await _instituteRepository.GetAllInstituteDetails(_apiBaseUrl.Value.LmsApiBaseUrl);

            if (getAllInstituteResponseModel.Succeeded)
            {
                if (getAllInstituteResponseModel == null && getAllInstituteResponseModel.data == null)
                {
                    responseModel.ResponseMessage = getAllInstituteResponseModel.message;
                    responseModel.IsSuccess = getAllInstituteResponseModel.isSuccess;
                }
                if (getAllInstituteResponseModel != null)
                {
                    //User user = authenticationResponseModel.userDetail;
                    responseModel.ResponseMessage = getAllInstituteResponseModel.message;
                    responseModel.IsSuccess = getAllInstituteResponseModel.isSuccess;
                    foreach (var item in getAllInstituteResponseModel.data)
                    {
                        subject.Add(new SelectListItem
                        {
                            Text = item.InstituteName,
                            Value = Convert.ToString(item.Id)
                        });
                    }
                    return subject;
                }
            }
            else
            {
                responseModel.ResponseMessage = getAllInstituteResponseModel.message;
                responseModel.IsSuccess = getAllInstituteResponseModel.isSuccess;
            }
            return null;
        }


        public async Task<List<SelectListItem>> GetEvent()
        {
            List<SelectListItem> events = new List<SelectListItem>();
            GetEventsListResponseModel getEventsListResponseModel = null;
            ResponseModel responseModel = new ResponseModel();
            getEventsListResponseModel = await _eventRepository.GetEventsList(_apiBaseUrl.Value.LmsApiBaseUrl);

            if (getEventsListResponseModel.Succeeded)
            {
                if (getEventsListResponseModel == null && getEventsListResponseModel.data == null)
                {
                    responseModel.ResponseMessage = getEventsListResponseModel.message;
                    responseModel.IsSuccess = getEventsListResponseModel.isSuccess;
                }
                if (getEventsListResponseModel != null)
                {
                    //User user = authenticationResponseModel.userDetail;
                    responseModel.ResponseMessage = getEventsListResponseModel.message;
                    responseModel.IsSuccess = getEventsListResponseModel.isSuccess;
                    foreach (var item in getEventsListResponseModel.data)
                    {
                        events.Add(new SelectListItem
                        {
                            Text = item.EventTitle,
                            Value = Convert.ToString(item.Id)
                        });
                    }
                    return events;
                }
            }
            else
            {
                responseModel.ResponseMessage = getEventsListResponseModel.message;
                responseModel.IsSuccess = getEventsListResponseModel.isSuccess;
            }
            return null;
        }


        public async Task<List<SelectListItem>> GetAllsStudent()
        {
            List<SelectListItem> students = new List<SelectListItem>();
            GetAllStudentListResponseModel getAllStudentResponseModel = null;
            ResponseModel responseModel = new ResponseModel();
            getAllStudentResponseModel = await _studentRepository.GetAllStudentDetails(_apiBaseUrl.Value.LmsApiBaseUrl);

            if (getAllStudentResponseModel.Succeeded)
            {
                if (getAllStudentResponseModel == null && getAllStudentResponseModel.data == null)
                {
                    responseModel.ResponseMessage = getAllStudentResponseModel.message;
                    responseModel.IsSuccess = getAllStudentResponseModel.isSuccess;
                }
                if (getAllStudentResponseModel != null)
                {
                    //User user = authenticationResponseModel.userDetail;
                    responseModel.ResponseMessage = getAllStudentResponseModel.message;
                    responseModel.IsSuccess = getAllStudentResponseModel.isSuccess;
                    foreach (var item in getAllStudentResponseModel.data)
                    {
                        students.Add(new SelectListItem
                        {
                            Text = item.StudentName,
                            Value = Convert.ToString(item.Id)
                        });
                    }
                    return students;
                }
            }
            else
            {
                responseModel.ResponseMessage = getAllStudentResponseModel.message;
                responseModel.IsSuccess = getAllStudentResponseModel.isSuccess;
            }
            return null;
        }


        public async Task<List<SelectListItem>> GetAllParents()
        {
            List<SelectListItem> parents = new List<SelectListItem>();
            GetAllParentsListResponseModel getAllParentsResponseModel = null;
            ResponseModel responseModel = new ResponseModel();
            getAllParentsResponseModel = await _parentsRepository.GetAllParentsDetails(_apiBaseUrl.Value.LmsApiBaseUrl);

            if (getAllParentsResponseModel.Succeeded)
            {
                if (getAllParentsResponseModel == null && getAllParentsResponseModel.data == null)
                {
                    responseModel.ResponseMessage = getAllParentsResponseModel.message;
                    responseModel.IsSuccess = getAllParentsResponseModel.isSuccess;
                }
                if (getAllParentsResponseModel != null)
                {
                    //User user = authenticationResponseModel.userDetail;
                    responseModel.ResponseMessage = getAllParentsResponseModel.message;
                    responseModel.IsSuccess = getAllParentsResponseModel.isSuccess;
                    foreach (var item in getAllParentsResponseModel.data)
                    {
                        parents.Add(new SelectListItem
                        {
                            Text = item.ParentName,
                            Value = Convert.ToString(item.Id)
                        });
                    }
                    return parents;
                }
            }
            else
            {
                responseModel.ResponseMessage = getAllParentsResponseModel.message;
                responseModel.IsSuccess = getAllParentsResponseModel.isSuccess;
            }
            return null;
        }


        public async Task<List<SelectListItem>> GetFeedbackTitle()
        {
            List<SelectListItem> feedbacktitle = new List<SelectListItem>();
            GetFeedbackTitleListResponseModel getFeedbackTitleListResponseModel = null;
            ResponseModel responseModel = new ResponseModel();
            getFeedbackTitleListResponseModel = await _feedbackTitleRepository.GetFeedbackTitleDetails(_apiBaseUrl.Value.LmsApiBaseUrl);

            if (getFeedbackTitleListResponseModel.Succeeded)
            {
                if (getFeedbackTitleListResponseModel == null && getFeedbackTitleListResponseModel.data == null)
                {
                    responseModel.ResponseMessage = getFeedbackTitleListResponseModel.message;
                    responseModel.IsSuccess = getFeedbackTitleListResponseModel.isSuccess;
                }
                if (getFeedbackTitleListResponseModel != null)
                {
                    //User user = authenticationResponseModel.userDetail;
                    responseModel.ResponseMessage = getFeedbackTitleListResponseModel.message;
                    responseModel.IsSuccess = getFeedbackTitleListResponseModel.isSuccess;
                    foreach (var item in getFeedbackTitleListResponseModel.data)
                    {
                        feedbacktitle.Add(new SelectListItem
                        {
                            Text = item.FeedbackTitle,
                            Value = Convert.ToString(item.Id)
                        });
                    }
                    return feedbacktitle;
                }
            }
            else
            {
                responseModel.ResponseMessage = getFeedbackTitleListResponseModel.message;
                responseModel.IsSuccess = getFeedbackTitleListResponseModel.isSuccess;
            }
            return null;
        }
    }
}
