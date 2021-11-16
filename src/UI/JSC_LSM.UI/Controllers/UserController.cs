using JSC_LMS.Application.Features.Students.Commands.CreateStudent;
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
        private readonly IStudentRepository _studentRepository;
        private readonly JSC_LSM.UI.Common.Common _common;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly ISchoolRepository _schoolRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IClassRepository _classRepository;
        public UserController(IStateRepository stateRepository, IStudentRepository studentRepository, ISectionRepository sectionRepository, IClassRepository classRepository,JSC_LSM.UI.Common.Common common, IOptions<ApiBaseUrl> apiBaseUrl, ITeacherRepository teacherRepository)
        {
            _stateRepository = stateRepository;
            _studentRepository = studentRepository;
            _common = common;
            _apiBaseUrl = apiBaseUrl;
            _classRepository=classRepository;
            _sectionRepository = sectionRepository;

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
        public async Task<List<SelectListItem>> GetAllClass()
        {
            var classes = await _common.GetClass();
            return classes;
        }

        [HttpGet]
        public async Task<List<SelectListItem>> GetAllSection()
        {
            var sections = await _common.GetSection();
            return sections;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddUser()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddStudent()
        {
            ViewBag.AddStudentSuccess = null;
            ViewBag.AddStudentError = null;
            StudentModel student = new StudentModel();
            student.States = await _common.GetAllStates();
           // student.Schools = await _common.GetSchool();
            student.Classes = await _common.GetClass();
            student.Sections = await _common.GetSection();
            return View(student);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStudent(StudentModel studentModel)
        {
            ViewBag.AddStudentSuccess = null;
            ViewBag.AddStudentError = null;
            studentModel.States = await _common.GetAllStates();
            studentModel.Classes = await _common.GetClass();
            studentModel.Sections = await _common.GetSection();
            CreateStudentDto createNewStudent = new CreateStudentDto();
            //studentModel.RoleName = "Student";
            if (ModelState.IsValid)
            {

                createNewStudent.ClassId = studentModel.ClassId;
                createNewStudent.SectionId = studentModel.SectionId;
                createNewStudent.AddressLine1 = studentModel.AddressLine1;
                createNewStudent.AddressLine2 = studentModel.AddressLine2;
                createNewStudent.StudentName = studentModel.StudentName;
                createNewStudent.Email = studentModel.Email;
                createNewStudent.Mobile = studentModel.Mobile;
                createNewStudent.Password = studentModel.Password;
                createNewStudent.Username = studentModel.Username;
                createNewStudent.CityId = studentModel.CityId;
                createNewStudent.StateId = studentModel.StateId;
                createNewStudent.ZipId = studentModel.ZipId;
                createNewStudent.IsActive = studentModel.IsActive;
                createNewStudent.UserType = studentModel.UserType;


                StudentResponseModel studentResponseModel = null;
                ViewBag.AddStudentSuccess = null;
                ViewBag.AddStudentError = null;
                ResponseModel responseModel = new ResponseModel();

                studentResponseModel = await _studentRepository.AddNewStudent(createNewStudent);


                if (studentResponseModel.Succeeded)
                {
                    if (studentResponseModel == null && studentResponseModel.data == null)
                    {
                        responseModel.ResponseMessage = studentResponseModel.message;
                        responseModel.IsSuccess = studentResponseModel.Succeeded;
                    }
                    if (studentResponseModel != null)
                    {
                        if (studentResponseModel.data != null)
                        {
                            responseModel.ResponseMessage = studentResponseModel.message;
                            responseModel.IsSuccess = studentResponseModel.Succeeded;
                            ViewBag.AddStudentSuccess = "Details Added Successfully";
                            ModelState.Clear();
                            var newStudentModel = new StudentModel();
                            newStudentModel.States = await _common.GetAllStates();
                            newStudentModel.Classes = await _common.GetClass();
                            newStudentModel.Sections = await _common.GetSection();
                            return View("AddStudent");
                        }
                        else
                        {
                            responseModel.ResponseMessage = studentResponseModel.message;
                            responseModel.IsSuccess = studentResponseModel.Succeeded;
                            ViewBag.AddPrincipalError = studentResponseModel.message;
                            return View(studentModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = studentResponseModel.message;
                    responseModel.IsSuccess = studentResponseModel.Succeeded;
                    ViewBag.AddPrincipalError = studentResponseModel.message;
                }
            }
            return View(studentModel);

        }

        public IActionResult AddParents()
        {
            return View();

        }
      
       


    }
}
