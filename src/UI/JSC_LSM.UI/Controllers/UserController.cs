using ClosedXML.Excel;

using JSC_LMS.Application.Features.ManageProfile.ChangePassword;
using JSC_LMS.Application.Features.ManageProfile.UpdateProfileInfo;

using JSC_LMS.Application.Features.ParentsFeature.Commands.CreateParents;

using JSC_LMS.Application.Features.ParentsFeature.Commands.UpdateParents;

using JSC_LMS.Application.Features.Students.Commands.CreateStudent;
using JSC_LMS.Application.Features.Students.Commands.UpdateStudent;
using JSC_LMS.Application.Features.Teachers.Commands.CreateTeacher;
using JSC_LSM.UI.Helpers;
using JSC_LSM.UI.Models;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.ResponseModels.EventsResponseModel;
using JSC_LSM.UI.Services.IRepositories;
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
using System.Threading.Tasks;

namespace JSC_LSM.UI.Controllers
{
    public class UserController : BaseController
    {


        private readonly IStateRepository _stateRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IParentsRepository _parentsRepository;
        private readonly IPrincipalRepository _principalRepository;
        private readonly JSC_LSM.UI.Common.Common _common;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly ISchoolRepository _schoolRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IClassRepository _classRepository;
        private readonly IConfiguration _configuration;

        private readonly IUserRepository _userRepository;
        private readonly IEventsDetailsRepository _eventsRepository;

        public UserController(IStateRepository stateRepository, IPrincipalRepository principalRepository, IStudentRepository studentRepository, ISectionRepository sectionRepository, IParentsRepository parentsRepository, IClassRepository classRepository,JSC_LSM.UI.Common.Common common, IOptions<ApiBaseUrl> apiBaseUrl, ITeacherRepository teacherRepository, IUserRepository userRepository , IEventsDetailsRepository eventsRepository , IConfiguration configuration)

        {
            _stateRepository = stateRepository;
            _principalRepository = principalRepository;
            _studentRepository = studentRepository;
            _common = common;
            _apiBaseUrl = apiBaseUrl;
            _classRepository=classRepository;
            _sectionRepository = sectionRepository;
            _userRepository = userRepository;
            _parentsRepository = parentsRepository;
            _eventsRepository = eventsRepository;
            _configuration = configuration;

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
        [AcceptVerbs("Get", "Post")]
        public async Task<ActionResult> CheckEmailExists(string EmailId)
        {
            List<Institute> RegisterUsers = new List<Institute>();
            GetAllUsersResponseModel getAllUsersResponseModel = null;
            ResponseModel responseModel = new ResponseModel();
            getAllUsersResponseModel = await _userRepository.GetAllUser(_apiBaseUrl.Value.LmsApiBaseUrl);

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
            getAllUsersResponseModel = await _userRepository.GetAllUser(_apiBaseUrl.Value.LmsApiBaseUrl);

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

        public async Task<ActionResult> CheckEmailExistsForUpdateStudent(string EmailId)
        {
            var email = HttpContext.Session.GetString("UpdateStudentEmail");
            var j = Json(true);
            if (EmailId == email)
            {
                j = Json(true);
            }
            else if (EmailId != email)
            {
                List<Institute> RegisterUsers = new List<Institute>();
                GetAllUsersResponseModel getAllUsersResponseModel = null;
                ResponseModel responseModel = new ResponseModel();
                getAllUsersResponseModel = await _userRepository.GetAllUser(_apiBaseUrl.Value.LmsApiBaseUrl);

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
                    j = Json(true);
                }
                else
                {
                    j = Json($"Email already exists.");
                }
            }

            return j;
        }
        [AcceptVerbs("Get", "Post")]
        public async Task<ActionResult> CheckUsernameExistsForUpdateStudent(string UserNAME)
        {

            var username = HttpContext.Session.GetString("UpdateStudentUsername");

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
                getAllUsersResponseModel = await _userRepository.GetAllUser(_apiBaseUrl.Value.LmsApiBaseUrl);

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
        [AcceptVerbs("Get", "Post")]

        public async Task<ActionResult> CheckEmailExistsForUpdateParents(string EmailId)
        {
            var email = HttpContext.Session.GetString("UpdateParentsEmail");
            var j = Json(true);
            if (EmailId == email)
            {
                j = Json(true);
            }
            else if (EmailId != email)
            {
                List<Institute> RegisterUsers = new List<Institute>();
                GetAllUsersResponseModel getAllUsersResponseModel = null;
                ResponseModel responseModel = new ResponseModel();
                getAllUsersResponseModel = await _userRepository.GetAllUser(_apiBaseUrl.Value.LmsApiBaseUrl);

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
                    j = Json(true);
                }
                else
                {
                    j = Json($"Email already exists.");
                }
            }

            return j;
        }
        [AcceptVerbs("Get", "Post")]
        public async Task<ActionResult> CheckUsernameExistsForUpdateParents(string UserNAME)
        {

            var username = HttpContext.Session.GetString("UpdateParentsUsername");

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
                getAllUsersResponseModel = await _userRepository.GetAllUser(_apiBaseUrl.Value.LmsApiBaseUrl);

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
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var principal = await _principalRepository.GetPrincipalByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);
            
            studentModel.States = await _common.GetAllStates();
            studentModel.Classes = await _common.GetClass();
            studentModel.Sections = await _common.GetSection();
            CreateStudentDto createNewStudent = new CreateStudentDto();
            //studentModel.RoleName = "Student";
            if (ModelState.IsValid)
            {
                createNewStudent.SchoolId = principal.data.schoolid;
                createNewStudent.ClassId = studentModel.ClassId;
                createNewStudent.SectionId = studentModel.SectionId;
                createNewStudent.AddressLine1 = studentModel.AddressLine1;
                createNewStudent.AddressLine2 = studentModel.AddressLine2;
                createNewStudent.StudentName = studentModel.StudentName;
                createNewStudent.Email = studentModel.EmailId;
                createNewStudent.Mobile = studentModel.Mobile;
                createNewStudent.Password = studentModel.Password;
                createNewStudent.Username = studentModel.UserNAME;
                createNewStudent.CityId = studentModel.CityId;
                createNewStudent.StateId = studentModel.StateId;
                createNewStudent.ZipId = studentModel.ZipId;
                createNewStudent.IsActive = studentModel.IsActive;
                createNewStudent.UserType = studentModel.UserType;


                StudentResponseModel studentResponseModel = null;
                ViewBag.AddStudentSuccess = null;
                ViewBag.AddStudentError = null;
                ResponseModel responseModel = new ResponseModel();

                studentResponseModel = await _studentRepository.AddNewStudent(_apiBaseUrl.Value.LmsApiBaseUrl,createNewStudent);


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
                            var page = 1;
                            var size = 5;

                            int recsCount = (await _studentRepository.GetAllStudentBySchoolList(_apiBaseUrl.Value.LmsApiBaseUrl,principal.data.schoolid)).data.Count();

                            if (page < 1)
                                page = 1;
                            ViewBag.GetStudentById = TempData["GetStudentById"] as string;
                            var pager = new Pager(recsCount, page, size);
                            ViewBag.Pager = pager;
                            return View("ManageStudentUsers", pager);
                        }
                        else
                        {
                            responseModel.ResponseMessage = studentResponseModel.message;
                            responseModel.IsSuccess = studentResponseModel.Succeeded;
                            ViewBag.AddStudentError = studentResponseModel.message;
                            return View(studentModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = studentResponseModel.message;
                    responseModel.IsSuccess = studentResponseModel.Succeeded;
                    ViewBag.AddStudentError = studentResponseModel.message;
                }
            }
            return View(studentModel);

        }

        [HttpGet]
        public async Task<IActionResult> EditStudent(int id)
        {
            var student = await _studentRepository.GetStudentById(_apiBaseUrl.Value.LmsApiBaseUrl,id);
            if (student.data == null)
            {
                TempData["GetStudentById"] = student.message;
                return RedirectToAction("ManageStudentUsers", "User");
            }
            HttpContext.Session.SetString("UpdateStudentEmail", student.data.Email);
            HttpContext.Session.SetString("UpdateStudentUsername", student.data.Username);
            var studentData = new UpdateStudentViewModel()
            {
                Id = student.data.Id,
                StudentName = student.data.StudentName,
                UserType= student.data.UserType,
                AddressLine1 = student.data.AddressLine1,
                UserId = student.data.UserId,
                AddressLine2 = student.data.AddressLine2,
                CityId = student.data.City.Id,
                StateId = student.data.State.Id,
                EmailId = student.data.Email,
                IsActive = student.data.IsActive,
                Mobile = student.data.Mobile,
                ClassId = student.data.Class.Id,
                SectionId = student.data.Section.Id,
                UserNAME = student.data.Username,
                ZipId = student.data.Zip.Id
            };
            studentData.Classes = await _common.GetClass();
            studentData.Sections = await _common.GetSection();
            TempData["UserId"] = studentData.UserId;
            studentData.States = await _common.GetAllStates();
            studentData.Cities = await _common.GetAllCityByStateId(student.data.State.Id);
            studentData.ZipCode = await _common.GetAllZipByCityId(student.data.Zip.Id);
            return View(studentData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStudent(UpdateStudentViewModel updateStudentViewModel)
        {
            ViewBag.UpdateStudentSuccess = null;
            ViewBag.UpdateStudentError = null;
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var principal = await _principalRepository.GetPrincipalByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);
           
            updateStudentViewModel.Classes = await _common.GetClass();
            updateStudentViewModel.Sections = await _common.GetSection();
            updateStudentViewModel.States = await _common.GetAllStates();
            updateStudentViewModel.Cities = await _common.GetAllCityByStateId(updateStudentViewModel.StateId);
            updateStudentViewModel.ZipCode = await _common.GetAllZipByCityId(updateStudentViewModel.CityId);
            UpdateStudentDto updateStudent = new UpdateStudentDto();
            if (ModelState.IsValid)
            {
                updateStudent.SchoolId = principal.data.schoolid;
                updateStudent.Id = updateStudentViewModel.Id;
                updateStudent.UserId = TempData["UserId"].ToString();
                updateStudent.ClassId = updateStudentViewModel.ClassId;
                updateStudent.SectionId = updateStudentViewModel.SectionId;
                updateStudent.AddressLine1 = updateStudentViewModel.AddressLine1;
                updateStudent.AddressLine2 = updateStudentViewModel.AddressLine2;
                updateStudent.StudentName = updateStudentViewModel.StudentName;
                updateStudent.UserType = updateStudentViewModel.UserType;
                updateStudent.Email = updateStudentViewModel.EmailId;
                updateStudent.Mobile = updateStudentViewModel.Mobile;
                updateStudent.Username = updateStudentViewModel.UserNAME;
                updateStudent.CityId = updateStudentViewModel.CityId;
                updateStudent.StateId = updateStudentViewModel.StateId;
                updateStudent.ZipId = updateStudentViewModel.ZipId;
                updateStudent.IsActive = updateStudentViewModel.IsActive;


                UpdateStudentResponseModel updateStudentResponseModel = null;
                ViewBag.UpdateStudentSuccess = null;
                ViewBag.UpdateStudentError = null;
                ResponseModel responseModel = new ResponseModel();

                updateStudentResponseModel = await _studentRepository.UpdateStudent(_apiBaseUrl.Value.LmsApiBaseUrl,updateStudent);


                if (updateStudentResponseModel.Succeeded)
                {
                    if (updateStudentResponseModel == null && updateStudentResponseModel?.data == null)
                    {
                        responseModel.ResponseMessage = updateStudentResponseModel.message;
                        responseModel.IsSuccess = updateStudentResponseModel.Succeeded;
                    }
                    if (updateStudentResponseModel != null)
                    {
                        if (updateStudentResponseModel?.data != null)
                        {
                            responseModel.ResponseMessage = updateStudentResponseModel.message;
                            responseModel.IsSuccess = updateStudentResponseModel.Succeeded;
                            ViewBag.UpdateStudentSuccess = "Details Updated Successfully";
                            var page = 1;
                            var size = 5;
                            
                            int recsCount = (await _studentRepository.GetAllStudentBySchoolList(_apiBaseUrl.Value.LmsApiBaseUrl,principal.data.schoolid)).data.Count();

                            if (page < 1)
                                page = 1;
                            ViewBag.GetStudentById = TempData["GetStudentById"] as string;
                            var pager = new Pager(recsCount, page, size);
                            ViewBag.Pager = pager;
                            return View("ManageStudentUsers", pager);
                        }
                        else
                        {
                            responseModel.ResponseMessage = updateStudentResponseModel.message;
                            responseModel.IsSuccess = updateStudentResponseModel.Succeeded;
                            ViewBag.UpdateStudentError = updateStudentResponseModel.message;
                            return View(updateStudentResponseModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = updateStudentResponseModel.message;
                    responseModel.IsSuccess = updateStudentResponseModel.Succeeded;
                    ViewBag.UpdateStudentError = updateStudentResponseModel.message;
                }
            }
            return View(updateStudentViewModel);
        }

        [HttpGet]
        public async Task<List<SelectListItem>> GetStudentName()
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var principal = await _principalRepository.GetPrincipalByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);
            var data = await _studentRepository.GetAllStudentBySchoolList(_apiBaseUrl.Value.LmsApiBaseUrl,principal.data.schoolid);
            List<SelectListItem> students = new List<SelectListItem>();
            foreach (var item in data.data)
            {
                students.Add(new SelectListItem
                {
                    Text = item.StudentName,
                    Value = Convert.ToString(item.Id)
                });
            }
            return students;
        }


        [HttpGet]
        public async Task<IActionResult> ManageStudentUsers()
        {
            var page = 1;
            var size = 5;
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var principal = await _principalRepository.GetPrincipalByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);
            int recsCount = (await _studentRepository.GetAllStudentBySchoolList(_apiBaseUrl.Value.LmsApiBaseUrl,principal.data.schoolid)).data.Count();

            if (page < 1)
                page = 1;
            ViewBag.GetStudentById = TempData["GetStudentById"] as string;
            var pager = new Pager(recsCount, page, size);
            ViewBag.Pager = pager;
            return View(pager);
        }

        [HttpGet]
        public async Task<IEnumerable<StudentDetailsViewModel>> GetAllStudentDetails()
        {
            var data = new List<StudentDetailsViewModel>();
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var principal = await _principalRepository.GetPrincipalByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);
            var dataList = await _studentRepository.GetAllStudentBySchoolList(_apiBaseUrl.Value.LmsApiBaseUrl,principal.data.schoolid);
            foreach (var student in dataList.data)
            {
                data.Add(new StudentDetailsViewModel()
                {
                    Id = student.Id,
                    StudentName = student.StudentName,
                    UserType= student.UserType,
                    AddressLine1 = student.AddressLine1,
                    AddressLine2 = student.AddressLine2,
                    CityName = student.City.CityName,
                    StateName = student.State.StateName,
                    CreatedDate = student.CreatedDate,
                    Email = student.Email,
                    IsActive = student.IsActive,
                    Mobile = student.Mobile,
                    ClassName = student.Class.ClassName,
                    SectionName= student.Section.SectionName,
                    Username = student.Username,
                    ZipCode = student.Zip.Zipcode
                });
            }
            return data;
        }

        [HttpGet]
        public async Task<IEnumerable<StudentDetailsViewModel>> GetAllStudentDetailsByPagination(int page = 1, int size = 5)
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var principal = await _principalRepository.GetPrincipalByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);
            int recsCount = (await _studentRepository.GetAllStudentBySchoolList(_apiBaseUrl.Value.LmsApiBaseUrl,principal.data.schoolid)).data.Count();
            if (page < 1)
                page = 1;
            var pager = new Pager(recsCount, page, size);

            ViewBag.Pager = pager;
            var data = new List<StudentDetailsViewModel>();

            var dataList = await _studentRepository.GetStudentListBySchoolPagination(_apiBaseUrl.Value.LmsApiBaseUrl,page, size , principal.data.schoolid);

            foreach (var student in dataList.data)
            {
                data.Add(new StudentDetailsViewModel()
                {

                    Id = student.Id,
                    StudentName = student.StudentName,
                    UserType = student.UserType,
                    AddressLine1 = student.AddressLine1,
                    AddressLine2 = student.AddressLine2,
                    CityName = student.City.CityName,
                    StateName = student.State.StateName,
                    CreatedDate = student.CreatedDate,
                    Email = student.Email,
                    IsActive = student.IsActive,
                    Mobile = student.Mobile,
                    ClassName = student.Class.ClassName,
                    SectionName = student.Section.SectionName,
                    Username = student.Username,
                    ZipCode = student.Zip.Zipcode
                });
            }
            return data;
        }

        [HttpGet]
        public async Task<GetStudentByIdResponseModel> GetStudentById(int Id)
        {

            var student = await _studentRepository.GetStudentById(_apiBaseUrl.Value.LmsApiBaseUrl,Id);
            return student;
        }

        [HttpGet]
        public async Task<IActionResult> DownloadExcel()
        {
            var data = new List<StudentDetailsViewModel>();

            var dataList = await _studentRepository.GetAllStudentDetails(_apiBaseUrl.Value.LmsApiBaseUrl);
            //Creating DataTable  
            DataTable dt = new DataTable();
            //Setiing Table Name  
            dt.TableName = "StudentData";
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("User_type", typeof(string));
            dt.Columns.Add("Student_Name", typeof(string));
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
            dt.Columns.Add("Class_Id", typeof(int));
            dt.Columns.Add("Class_Name", typeof(string));
            dt.Columns.Add("Section_Id", typeof(int));
            dt.Columns.Add("Section_Name", typeof(string));            
            dt.Columns.Add("CreatedDate", typeof(DateTime));
            foreach (var _student in dataList.data)
            {
                dt.Rows.Add(_student.Id, _student.UserType, _student.StudentName, _student.AddressLine1, _student.AddressLine2, _student.Mobile, _student.Username, _student.Email, _student.IsActive ? "Active" : "Inactive", _student.City.Id, _student.City.CityName, _student.State.Id, _student.State.StateName, _student.Zip.Id, _student.Zip.Zipcode, _student.Class.Id, _student.Class.ClassName, _student.Section.Id, _student.Section.SectionName, _student.CreatedDate?.ToShortDateString());
            }
            string fileName = "StudentData_" + DateTime.Now.ToShortDateString() + ".xlsx";

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }

            }
        }

        [HttpGet]
        public async Task<IEnumerable<StudentDetailsViewModel>> GetStudentByFilters( string ClassName, string SectionName,string StudentName, bool IsActive, DateTime CreatedDate)
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var principal = await _principalRepository.GetPrincipalByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);
            var data = new List<StudentDetailsViewModel>();
            var dataList = await _studentRepository.GetStudentByFilters(_apiBaseUrl.Value.LmsApiBaseUrl, principal.data.schoolid, ClassName, SectionName,  StudentName, IsActive, CreatedDate);
            if (dataList.data != null)
            {
                foreach (var student in dataList.data)
                {
                    data.Add(new StudentDetailsViewModel()
                    {
                        Id = student.Id,
                        StudentName = student.StudentName,
                        UserType = student.UserType,
                        AddressLine1 = student.AddressLine1,
                        AddressLine2 = student.AddressLine2,
                        CityName = student.City.CityName,
                        StateName = student.State.StateName,
                        CreatedDate = student.CreatedDate,
                        Email = student.Email,
                        IsActive = student.IsActive,
                        Mobile = student.Mobile,
                        ClassName = student.Class.ClassName,
                        SectionName = student.Section.SectionName,
                        Username = student.Username,
                        ZipCode = student.Zip.Zipcode
                    });
                }
            }
            return data;
        }


        //PARENTS

        [HttpGet]
        public async Task<IActionResult> AddParents()
        {           
            ViewBag.AddParentsError = null;
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var principal = await _principalRepository.GetPrincipalByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);
            ParentsModel parents = new ParentsModel();
            parents.States = await _common.GetAllStates();
            parents.Classes = await _common.GetClass();
            parents.Sections = await _common.GetSection();
          
            return View(parents);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddParents(ParentsModel parentsModel)
        {
            ViewBag.AddParentsSuccess = null;
            ViewBag.AddParentsError = null;
            parentsModel.States = await _common.GetAllStates();
            parentsModel.Classes = await _common.GetClass();
            parentsModel.Sections = await _common.GetSection();
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var principal = await _principalRepository.GetPrincipalByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);
            CreateParentsDto createNewParent = new CreateParentsDto();
         
            if (ModelState.IsValid)
            {
                createNewParent.SchoolId = principal.data.schoolid;
                createNewParent.ClassId = parentsModel.ClassId;
                createNewParent.SectionId = parentsModel.SectionId;
                createNewParent.AddressLine1 = parentsModel.AddressLine1;
                createNewParent.AddressLine2 = parentsModel.AddressLine2;
                createNewParent.ParentName = parentsModel.ParentName;
                createNewParent.Email = parentsModel.EmailId;
                createNewParent.Mobile = parentsModel.Mobile;
                createNewParent.Password = parentsModel.Password;
                createNewParent.Username = parentsModel.UserNAME;
                createNewParent.CityId = parentsModel.CityId;
                createNewParent.StateId = parentsModel.StateId;
                createNewParent.ZipId = parentsModel.ZipId;
                createNewParent.IsActive = parentsModel.IsActive;
                createNewParent.UserType = parentsModel.UserType;
                createNewParent.StudentId = string.Join(",", parentsModel.Students);


                ParentsResponseModel parentsResponseModel = null;
                
                ViewBag.AddStudentError = null;
                ResponseModel responseModel = new ResponseModel();

                parentsResponseModel = await _parentsRepository.AddNewParents(_apiBaseUrl.Value.LmsApiBaseUrl,createNewParent);


                if (parentsResponseModel.Succeeded)
                {
                    if (parentsResponseModel == null && parentsResponseModel.data == null)
                    {
                        responseModel.ResponseMessage = parentsResponseModel.message;
                        responseModel.IsSuccess = parentsResponseModel.Succeeded;
                    }
                    if (parentsResponseModel != null)
                    {
                        if (parentsResponseModel.data != null)
                        {
                            responseModel.ResponseMessage = parentsResponseModel.message;
                            responseModel.IsSuccess = parentsResponseModel.Succeeded;
                            ViewBag.AddParentsSuccess = "Details added succesfully.";
                            var page = 1;
                            var size = 5;
                            
                            int recsCount = (await _parentsRepository.GetAllParentsBySchoolList(_apiBaseUrl.Value.LmsApiBaseUrl,principal.data.schoolid)).data.Count();
                            if (page < 1)
                                page = 1;
                            ViewBag.GetParentById = TempData["GetParentById"] as string;
                            var pager = new Pager(recsCount, page, size);
                            ViewBag.Pager = pager;
                            return View("ManageParentsUsers", pager);
                        }
                        else
                        {
                            responseModel.ResponseMessage = parentsResponseModel.message;
                            responseModel.IsSuccess = parentsResponseModel.Succeeded;
                            ViewBag.AddParentsError = parentsResponseModel.message;
                            return View(parentsModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = parentsResponseModel.message;
                    responseModel.IsSuccess = parentsResponseModel.Succeeded;
                    ViewBag.AddParentsError = parentsResponseModel.message;
                }
            }
            return View(parentsModel);

        }

        [HttpGet]
        public async Task<IActionResult> EditParents(int id)
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var principal = await _principalRepository.GetPrincipalByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);
            var parents = await _parentsRepository.GetParentsById(_apiBaseUrl.Value.LmsApiBaseUrl,id);
            if (parents.data == null)
            {
                TempData["GetParentsById"] = parents.message;
                return RedirectToAction("ParentsDetails", "User");
            }
            HttpContext.Session.SetString("UpdateParentsEmail", parents.data.Email);
            HttpContext.Session.SetString("UpdateParentsUsername", parents.data.Username);
            List<int> tempStd = new List<int>();
            foreach(var std in parents.data.Student)
            {
                tempStd.Add(std.Id);
            }

            var data = await _studentRepository.GetAllStudentBySchoolList(_apiBaseUrl.Value.LmsApiBaseUrl,principal.data.schoolid);
            List<SelectListItem> students = new List<SelectListItem>();
            foreach (var item in data.data)
            {
                foreach(var stdId in tempStd)
                {
                    if(item.Id == stdId)
                    {
                        students.Add(new SelectListItem
                        {
                            Text = item.StudentName,
                            Value = Convert.ToString(item.Id),Selected=true
                        });
                        break;
                    }
                    else
                    {
                        students.Add(new SelectListItem
                        {
                            Text = item.StudentName,
                            Value = Convert.ToString(item.Id)
                        });
                        break;
                    }
                }
               
            }
            var parentData = new UpdateParentsViewModel()
            {
                Id = parents.data.Id,
                ParentName = parents.data.ParentName,
                UserType = parents.data.UserType,
                AddressLine1 = parents.data.AddressLine1,
                UserId = parents.data.UserId,
                AddressLine2 = parents.data.AddressLine2,
                CityId = parents.data.City.Id,
                StateId = parents.data.State.Id,
                EmailId = parents.data.Email,
                IsActive = parents.data.IsActive,
                Mobile = parents.data.Mobile,
                ClassId = parents.data.Class.Id,
                SectionId = parents.data.Section.Id,
                UserNAME = parents.data.Username,
                ZipId = parents.data.Zip.Id ,
                StudentId = tempStd

            };

            parentData.Classes = await _common.GetClass();
            parentData.Sections = await _common.GetSection();
            parentData.Students = students;
            TempData["UserId"] = parentData.UserId;
            parentData.States = await _common.GetAllStates();
            parentData.Cities = await _common.GetAllCityByStateId(parents.data.State.Id);
            parentData.ZipCode = await _common.GetAllZipByCityId(parents.data.Zip.Id);
            return View(parentData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditParents(UpdateParentsViewModel updateParentsViewModel)
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var principal = await _principalRepository.GetPrincipalByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);
            ViewBag.UpdateParentsSuccess = null;
            ViewBag.UpdateParentsError = null;
            updateParentsViewModel.Classes = await _common.GetClass();
            updateParentsViewModel.Sections = await _common.GetSection();
            updateParentsViewModel.States = await _common.GetAllStates();
            updateParentsViewModel.Cities = await _common.GetAllCityByStateId(updateParentsViewModel.StateId);
            updateParentsViewModel.ZipCode = await _common.GetAllZipByCityId(updateParentsViewModel.CityId);
            UpdateParentsDto updateParents = new UpdateParentsDto();
            if (ModelState.IsValid)
            {
                updateParents.Id = updateParentsViewModel.Id;
                updateParents.SchoolId = principal.data.schoolid;
                updateParents.UserId = TempData["UserId"].ToString();
                updateParents.ClassId = updateParentsViewModel.ClassId;
                updateParents.SectionId = updateParentsViewModel.SectionId;
                updateParents.AddressLine1 = updateParentsViewModel.AddressLine1;
                updateParents.AddressLine2 = updateParentsViewModel.AddressLine2;
                updateParents.ParentName = updateParentsViewModel.ParentName;
                updateParents.UserType = updateParentsViewModel.UserType;
                updateParents.Email = updateParentsViewModel.EmailId;
                updateParents.Mobile = updateParentsViewModel.Mobile;
                updateParents.Username = updateParentsViewModel.UserNAME;
                updateParents.CityId = updateParentsViewModel.CityId;
                updateParents.StateId = updateParentsViewModel.StateId;
                updateParents.ZipId = updateParentsViewModel.ZipId;
                updateParents.IsActive = updateParentsViewModel.IsActive;
                updateParents.StudentId = string.Join(",", updateParentsViewModel.StudentId);


                UpdateParentsResponseModel updateParentsResponseModel = null;
                
                ViewBag.UpdateParentsError = null;
                ResponseModel responseModel = new ResponseModel();

                updateParentsResponseModel = await _parentsRepository.UpdateParents(_apiBaseUrl.Value.LmsApiBaseUrl,updateParents);


                if (updateParentsResponseModel.Succeeded)
                {
                    if (updateParentsResponseModel == null && updateParentsResponseModel?.data == null)
                    {
                        responseModel.ResponseMessage = updateParentsResponseModel.message;
                        responseModel.IsSuccess = updateParentsResponseModel.Succeeded;
                    }
                    if (updateParentsResponseModel != null)
                    {
                        if (updateParentsResponseModel?.data != null)
                        {
                            responseModel.ResponseMessage = updateParentsResponseModel.message;
                            responseModel.IsSuccess = updateParentsResponseModel.Succeeded;
                            ViewBag.UpdateParentsSuccess = "Details Updated Successfully";
                            var page = 1;
                            var size = 5;

                            int recsCount = (await _parentsRepository.GetAllParentsBySchoolList(_apiBaseUrl.Value.LmsApiBaseUrl,principal.data.schoolid)).data.Count();
                            if (page < 1)
                                page = 1;
                            ViewBag.GetParentById = TempData["GetParentById"] as string;
                            var pager = new Pager(recsCount, page, size);
                            ViewBag.Pager = pager;
                            return View("ManageParentsUsers", pager);
                            return RedirectToAction("ManageParentsUsers", "User");
                        }
                        else
                        {
                            responseModel.ResponseMessage = updateParentsResponseModel.message;
                            responseModel.IsSuccess = updateParentsResponseModel.Succeeded;
                            ViewBag.UpdateParentsError = updateParentsResponseModel.message;
                            return View(updateParentsResponseModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = updateParentsResponseModel.message;
                    responseModel.IsSuccess = updateParentsResponseModel.Succeeded;
                    ViewBag.UpdateParentsError = updateParentsResponseModel.message;
                }
            }
            return View(updateParentsViewModel);
        }

        [HttpGet]
        public async Task<List<SelectListItem>> GetParentName()
        {
            var data = await _parentsRepository.GetAllParentsDetails(_apiBaseUrl.Value.LmsApiBaseUrl);
           
            List<SelectListItem> parents = new List<SelectListItem>();
            
            foreach (var item in data.data)
            {
                parents.Add(new SelectListItem
                {
                    Text = item.ParentName,
                    Value = Convert.ToString(item.Id)
                });
            }
            parents = parents.GroupBy(x => x.Text).Select(x => x.First()).ToList();
            return parents;
        }

        [HttpGet]
        public async Task<IActionResult> ManageParentsUsers()
        {
            var page = 1;
            var size = 5;
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var principal = await _principalRepository.GetPrincipalByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);
            int recsCount = (await _parentsRepository.GetAllParentsBySchoolList(_apiBaseUrl.Value.LmsApiBaseUrl,principal.data.schoolid)).data.Count();
            if (page < 1)
                page = 1;
            ViewBag.GetParentById = TempData["GetParentById"] as string;
            var pager = new Pager(recsCount, page, size);
            ViewBag.Pager = pager;
            return View(pager);
        }

        [HttpGet]
        public async Task<IEnumerable<ParentsDetailsViewModel>> GetAllParentsDetails()
        {
            var data = new List<ParentsDetailsViewModel>();
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var principal = await _principalRepository.GetPrincipalByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);

            var dataList = await _parentsRepository.GetAllParentsBySchoolList(_apiBaseUrl.Value.LmsApiBaseUrl,principal.data.schoolid);
            foreach (var parents in dataList.data)
            {
                data.Add(new ParentsDetailsViewModel()
                {
                    Id = parents.Id,
                    ParentsName = parents.ParentName,
                    StudentName = parents.StudentName,
                    ClassName = parents.ClassName,
                    SectionName = parents.SectionName,
                    IsActive = parents.IsActive,
                    CreatedDate = parents.CreatedDate
                });
            }
            return data;
        }

        [HttpGet]
        public async Task<IEnumerable<ParentsDetailsViewModel>> GetAllParentDetailsByPagination(int page = 1, int size = 5)
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var principal = await _principalRepository.GetPrincipalByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);
            int recsCount = (await _parentsRepository.GetAllParentsBySchoolList(_apiBaseUrl.Value.LmsApiBaseUrl,principal.data.schoolid)).data.Count();            
            if (page < 1)
                page = 1;
            var pager = new Pager(recsCount, page, size);

            ViewBag.Pager = pager;
            var data = new List<ParentsDetailsViewModel>();

            var dataList = await _parentsRepository.GetParentsListBySchoolPagination(_apiBaseUrl.Value.LmsApiBaseUrl,page, size,principal.data.schoolid);
           
            foreach (var parents in dataList.data)
            {
                data.Add(new ParentsDetailsViewModel()
                {
                    Id = parents.Id,
                    ParentsName = parents.ParentName,
                    StudentName = parents.StudentName,
                    ClassName = parents.ClassName,
                    SectionName = parents.SectionName,
                    IsActive = parents.IsActive,
                    CreatedDate = parents.CreatedDate
                });
            }
            return data;
        }

        [HttpGet]
        public async Task<IActionResult> DownloadExcelParents()
        {
            var data = new List<ParentsDetailsViewModel>();

            var dataList = await _parentsRepository.GetAllParentsDetails(_apiBaseUrl.Value.LmsApiBaseUrl);
            //Creating DataTable  
            DataTable dt = new DataTable();
            //Setiing Table Name  
            dt.TableName = "ParentsData";
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("User_type", typeof(string));
            dt.Columns.Add("Parents_Name", typeof(string));
            dt.Columns.Add("Student_Name", typeof(string));
            dt.Columns.Add("AddressLine1", typeof(string));
            dt.Columns.Add("AddressLine2", typeof(string));
            dt.Columns.Add("Mobile", typeof(string));
            dt.Columns.Add("Username", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("IsActive", typeof(string));
            dt.Columns.Add("City_Name", typeof(string));
            dt.Columns.Add("State_Name", typeof(string));
            dt.Columns.Add("ZipCode", typeof(string));
            dt.Columns.Add("Class_Name", typeof(string));
            dt.Columns.Add("Section_Name", typeof(string));
            dt.Columns.Add("CreatedDate", typeof(DateTime));
            foreach (var parent in dataList.data)
            {
                dt.Rows.Add(parent.Id, parent.UserType, parent.ParentName, parent.StudentName, parent.AddressLine1, parent.AddressLine2, parent.Mobile, parent.Username, parent.Email, parent.IsActive ? "Active" : "Inactive", parent.CityName, parent.State, parent.Zip, parent.ClassName, parent.SectionName, parent.CreatedDate?.ToShortDateString());

            }
            string fileName = "ParentsData_" + DateTime.Now.ToShortDateString() + ".xlsx";

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }

            }
        }

        [HttpGet]
        public async Task<ParentsDetailsViewModel> GetParentsById(int Id , string StudentName)
        {

            string std = null;
            var parents = await _parentsRepository.GetParentsById(_apiBaseUrl.Value.LmsApiBaseUrl,Id);
            for ( int i=0;i< parents.data.Student.Count();i++ )
            {
                if(parents.data.Student[i].StudentName == StudentName)
                {
                    std = parents.data.Student[i].StudentName;
                }
            }
            var data = new ParentsDetailsViewModel() {
                Id = parents.data.Id,
                ParentsName = parents.data.ParentName,
                StudentName = std,
                ClassName = parents.data.Class.ClassName,
                SectionName = parents.data.Section.SectionName,
                AddressLine1 = parents.data.AddressLine1,
                AddressLine2 = parents.data.AddressLine2,
                CityName = parents.data.City.CityName,
                StateName = parents.data.State.StateName,
                ZipCode = parents.data.Zip.Zipcode,
                Username = parents.data.Username,
                UserType = parents.data.UserType,
                Email = parents.data.Email,
                Mobile = parents.data.Mobile,
                IsActive = parents.data.IsActive,
                CreatedDate = parents.data.CreatedDate
            };
          
            return data;
        }

        [HttpGet]
        public async Task<IEnumerable<ParentsDetailsViewModel>> GetParentsByFilters( string ClassName, string SectionName, string StudentName,string ParentsName, bool IsActive, DateTime CreatedDate)
        {
            var data = new List<ParentsDetailsViewModel>();
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var principal = await _principalRepository.GetPrincipalByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);
            var dataList = await _parentsRepository.GetParentsByFilters(_apiBaseUrl.Value.LmsApiBaseUrl,principal.data.schoolid,ClassName, SectionName, StudentName,ParentsName, IsActive, CreatedDate);
            if (dataList.data != null)
            {
                foreach (var student in dataList.data)
                {
                    data.Add(new ParentsDetailsViewModel()
                    {
                        Id = student.Id,
                        StudentName = student.StudentName,                       
                        ParentsName = student.ParentName,
                        CreatedDate = student.CreatedDate,                       
                        IsActive = student.IsActive,                        
                        ClassName = student.ClassName,
                        SectionName = student.SectionName
                       
                    });
                }
            }
            return data;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ManageProfile ManageProfile)
        {
            ViewBag.UpdateChangePasswordSuccess = null;
            ViewBag.UpdateChangePasswordError = null;
            var userId = Convert.ToString(Request.Cookies["Id"]);
            ChangePasswordDto changePasswordDto = new ChangePasswordDto();
            if (ModelState.IsValid)
            {
                changePasswordDto.UserId = userId;
                changePasswordDto.CurrentPassword = ManageProfile.ChangePassword.CurrentPassword;
                changePasswordDto.NewPassword = ManageProfile.ChangePassword.NewPassword;

                ChangePasswordResponseModel changePasswordResponseModel = null;
                ViewBag.UpdateChangePasswordSuccess = null;
                ViewBag.UpdateChangePasswordError = null;
                ResponseModel responseModel = new ResponseModel();

                changePasswordResponseModel = await _userRepository.UpdateChangePassword(_apiBaseUrl.Value.LmsApiBaseUrl,changePasswordDto);


                if (changePasswordResponseModel.Succeeded)
                {
                    if (changePasswordResponseModel == null && changePasswordResponseModel?.data == null)
                    {
                        responseModel.ResponseMessage = changePasswordResponseModel.message;
                        responseModel.IsSuccess = changePasswordResponseModel.Succeeded;
                    }
                    if (changePasswordResponseModel != null)
                    {
                        if (changePasswordResponseModel?.data != null)
                        {
                            responseModel.ResponseMessage = changePasswordResponseModel.message;
                            responseModel.IsSuccess = changePasswordResponseModel.Succeeded;

                            ViewBag.UpdateChangePasswordSuccess = changePasswordResponseModel.message;
                            return RedirectToAction("Index", "Home");

                        }
                        else
                        {
                            responseModel.ResponseMessage = changePasswordResponseModel.message;
                            responseModel.IsSuccess = changePasswordResponseModel.Succeeded;
                            ViewBag.UpdateChangePasswordError = changePasswordResponseModel.message;
                            return RedirectToAction("Index", "Home");

                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = changePasswordResponseModel.message;
                    responseModel.IsSuccess = changePasswordResponseModel.Succeeded;
                    ViewBag.UpdateChangePasswordError = changePasswordResponseModel.message;
                }
            }
            return View("ManageProfile");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfileInformation(ManageProfile ManageProfile)
        {
            ViewBag.UpdateProfileSuccess = null;
            ViewBag.UpdateProfileError = null;

            UpdateProfileInfoDto updateProfileInformationDto = new UpdateProfileInfoDto();
            if (ModelState.IsValid)
            {

                updateProfileInformationDto.Id = Convert.ToInt32(TempData["CommonId"].ToString());

                updateProfileInformationDto.roleName = Convert.ToString(Request.Cookies["RoleName"]);
                updateProfileInformationDto.Name = ManageProfile.ProfileInformation.Name;
                updateProfileInformationDto.Mobile = ManageProfile.ProfileInformation.Mobile;

                UpdateProfileInformationResponseModel updateProfileInformationResponseModel = null;
                ViewBag.UpdateProfileSuccess = null;
                ViewBag.UpdateProfileError = null;
                ResponseModel responseModel = new ResponseModel();

                updateProfileInformationResponseModel = await _userRepository.UpdatePersonalInformation(_apiBaseUrl.Value.LmsApiBaseUrl,updateProfileInformationDto);


                if (updateProfileInformationResponseModel.Succeeded)
                {
                    if (updateProfileInformationResponseModel == null && updateProfileInformationResponseModel?.data == null)
                    {
                        responseModel.ResponseMessage = updateProfileInformationResponseModel.message;
                        responseModel.IsSuccess = updateProfileInformationResponseModel.Succeeded;
                    }
                    if (updateProfileInformationResponseModel != null)
                    {
                        if (updateProfileInformationResponseModel?.data != null)
                        {
                            responseModel.ResponseMessage = updateProfileInformationResponseModel.message;
                            responseModel.IsSuccess = updateProfileInformationResponseModel.Succeeded;
                            ViewBag.UpdateProfileSuccess = "Details Updated Successfully";

                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            responseModel.ResponseMessage = updateProfileInformationResponseModel.message;
                            responseModel.IsSuccess = updateProfileInformationResponseModel.Succeeded;
                            ViewBag.UpdateProfileError = updateProfileInformationResponseModel.message;
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = updateProfileInformationResponseModel.message;
                    responseModel.IsSuccess = updateProfileInformationResponseModel.Succeeded;
                    ViewBag.UpdateProfileError = updateProfileInformationResponseModel.message;
                }
            }
            return View("ManageProfile");
        }


        [HttpGet]
        public async Task<IActionResult> StudentManageProfile()
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var student = await _studentRepository.GetStudentByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);
            var studentvm = new ManageProfile()
            {
                ProfileInformation = new ProfileInformation()
                {
                    Mobile = student.data.Mobile,
                    Name = student.data.Name,
                    Id = student.data.Id,
                    RoleName = Convert.ToString(Request.Cookies["RoleName"])
                }
            };
            TempData["CommonId"] = student.data.Id;
            HttpContext.Session.SetString("ProfilrInformationId", student.data.Id.ToString());
            return View(studentvm);


        }

        [HttpGet]
        public async Task<IActionResult> ParentManageProfile()
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var parent = await _parentsRepository.GetParentByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);
            var parentvm = new ManageProfile()
            {
                ProfileInformation = new ProfileInformation()
                {
                    Mobile = parent.data.Mobile,
                    Name = parent.data.Name,
                    Id = parent.data.Id,
                    RoleName = Convert.ToString(Request.Cookies["RoleName"])
                }
            };
            TempData["CommonId"] = parent.data.Id;
            HttpContext.Session.SetString("ProfilrInformationId", parent.data.Id.ToString());
            return View(parentvm);


        }

        [HttpGet]
        public async Task<IActionResult> ManageAllEvents()
        {
            var data = new List<GetEventsListBySchoolId>();
            EventsDetailsModel model = new EventsDetailsModel();
            var role = Convert.ToString(Request.Cookies["RoleName"]);
            int schoolID = 0;
            if(role == "Parent")
            {
                var userId = Convert.ToString(Request.Cookies["Id"]);
                var parent = await _parentsRepository.GetParentByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);
                schoolID = parent.data.SchoolId;
            }
            else if(role == "Student")
            {
                var userId = Convert.ToString(Request.Cookies["Id"]);
                var student = await _studentRepository.GetStudentByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);
                schoolID = student.data.Schoolid;
            }
            
            var dataList = await _eventsRepository.GetAllEventsBySchoolList(_apiBaseUrl.Value.LmsApiBaseUrl,schoolID);
           
            foreach (var eventsdata in dataList.data)
            {
                if (eventsdata.Status)
                {
                    data.Add(new GetEventsListBySchoolId()
                    {

                        Id = eventsdata.Id,
                        EventTitle = eventsdata.EventTitle,
                        Description=eventsdata.Description,
                        EventCoordinator = eventsdata.EventCoordinator,
                        EventDateTime = eventsdata.EventDateTime,
                        CoordinatorNumber = eventsdata.CoordinatorNumber,
                        Venue = eventsdata.Venue

                    });
                }
                
            }
            model.GetEventsListBySchoolId = data;
            return View(model);
        }

        [HttpGet]
        public async Task<GetEventsByIdResponseModel> ViewEventsData(int Id)
        {
            var eventdata = await _eventsRepository.GetEventsById(_apiBaseUrl.Value.LmsApiBaseUrl,Id);
            return eventdata;
        }
    }
}
