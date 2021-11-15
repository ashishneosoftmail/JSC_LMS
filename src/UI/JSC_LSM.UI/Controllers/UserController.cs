using JSC_LMS.Application.Features.Teachers.Commands.CreateTeacher;
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
    public class UserController : BaseController
    {


        private readonly IStateRepository _stateRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly JSC_LSM.UI.Common.Common _common;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly ISchoolRepository _schoolRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IClassRepository _classRepository;
        public UserController(IStateRepository stateRepository, ISchoolRepository schoolRepository, JSC_LSM.UI.Common.Common common, IOptions<ApiBaseUrl> apiBaseUrl, ITeacherRepository teacherRepository)
        {
            _stateRepository = stateRepository;
            _teacherRepository = teacherRepository;
            _common = common;
            _apiBaseUrl = apiBaseUrl;
            _schoolRepository = schoolRepository;

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
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddUser()
        {
            return View();
        }
        public IActionResult AddStudent()
        {
            return View();
        }
        public IActionResult AddParents()
        {
            return View();

        }
        public IActionResult ManageTeacherUsers()
        {
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> AddTeacher()
        {
            ViewBag.AddInstituteSuccess = null;
            ViewBag.AddInstituteError = null;
            Teacher teacher = new Teacher();
            teacher.States = await _common.GetAllStates();
            teacher.Schools = await _common.GetSchool();
            return View(teacher);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTeacher(Teacher teacher)
        {
            ViewBag.AddTeacherSuccess = null;
            ViewBag.AddTeacherError = null;
            teacher.States = await _common.GetAllStates();
            CreateTeacherDto createNewTeacher = new CreateTeacherDto();
            if (ModelState.IsValid)
            {
                createNewTeacher.TeacherName = teacher.TeacherName;
                createNewTeacher.AddressLine1 = teacher.AddressLine1;
                createNewTeacher.AddressLine2 = teacher.AddressLine2;
                createNewTeacher.SubjectId = teacher.SubjectId;
                createNewTeacher.Email = teacher.Email;
                createNewTeacher.Mobile = teacher.Mobile;
                createNewTeacher.Password = teacher.Password;
                createNewTeacher.Username = teacher.Username;
                createNewTeacher.CityId = teacher.CityId;
                createNewTeacher.StateId = teacher.StateId;
                createNewTeacher.ZipId = teacher.ZipId;
                createNewTeacher.SectionId = teacher.SectionId;
                createNewTeacher.ClassId = teacher.ClassId;
                createNewTeacher.IsActive = teacher.IsActive;
                createNewTeacher.UserType = teacher.UserType;

                TeacherResponseModel teacherResponseModel = null;
                ViewBag.AddTeacherError = null;
                ResponseModel responseModel = new ResponseModel();

                teacherResponseModel = await _teacherRepository.CreateTeacher(createNewTeacher);

                if (teacherResponseModel.Succeeded)
                {
                    if (teacherResponseModel == null && teacherResponseModel.data == null)
                    {
                        responseModel.ResponseMessage = teacherResponseModel.message;
                        responseModel.IsSuccess = teacherResponseModel.Succeeded;
                    }
                    if (teacherResponseModel != null)
                    {
                        if (teacherResponseModel.data != null)
                        {
                            responseModel.ResponseMessage = teacherResponseModel.message;
                            responseModel.IsSuccess = teacherResponseModel.Succeeded;

                            ViewBag.AddTeacherSuccess = "Details Added Successfully";
                            ModelState.Clear();
                            var newTeacherModel = new Teacher();
                            newTeacherModel.States = await _common.GetAllStates();
                            return View(newTeacherModel);

                        }
                        else
                        {
                            responseModel.ResponseMessage = teacherResponseModel.message;
                            responseModel.IsSuccess = teacherResponseModel.Succeeded;
                            ViewBag.AddTeacherError = teacherResponseModel.message;
                            return View(teacher);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = teacherResponseModel.message;
                    responseModel.IsSuccess = teacherResponseModel.Succeeded;
                    ViewBag.AddTeacherError = teacherResponseModel.message;

                }
            }
            return View(teacher);


        }


        public IActionResult EditTeacher()
        {
            return View();

        }


    }
}
