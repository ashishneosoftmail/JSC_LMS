using ClosedXML.Excel;
using JSC_LMS.Application.Features.Students.Commands.CreateStudent;
using JSC_LMS.Application.Features.Students.Commands.UpdateStudent;
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
                            return RedirectToAction("ManageStudentUsers","User");
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

        [HttpGet]
        public async Task<IActionResult> EditStudent(int id)
        {
            var student = await _studentRepository.GetStudentById(id);
            if (student.data == null)
            {
                TempData["GetStudentById"] = student.message;
                return RedirectToAction("StudentDetails", "User");
            }
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
                Email = student.data.Email,
                IsActive = student.data.IsActive,
                Mobile = student.data.Mobile,
                ClassId = student.data.Class.Id,
                SectionId = student.data.Section.Id,
                Username = student.data.Username,
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
            updateStudentViewModel.Classes = await _common.GetClass();
            updateStudentViewModel.Sections = await _common.GetSection();
            updateStudentViewModel.States = await _common.GetAllStates();
            updateStudentViewModel.Cities = await _common.GetAllCityByStateId(updateStudentViewModel.StateId);
            updateStudentViewModel.ZipCode = await _common.GetAllZipByCityId(updateStudentViewModel.CityId);
            UpdateStudentDto updateStudent = new UpdateStudentDto();
            if (ModelState.IsValid)
            {
                updateStudent.Id = updateStudentViewModel.Id;
                updateStudent.UserId = TempData["UserId"].ToString();
                updateStudent.ClassId = updateStudentViewModel.ClassId;
                updateStudent.SectionId = updateStudentViewModel.SectionId;
                updateStudent.AddressLine1 = updateStudentViewModel.AddressLine1;
                updateStudent.AddressLine2 = updateStudentViewModel.AddressLine2;
                updateStudent.StudentName = updateStudentViewModel.StudentName;
                updateStudent.UserType = updateStudentViewModel.UserType;
                updateStudent.Email = updateStudentViewModel.Email;
                updateStudent.Mobile = updateStudentViewModel.Mobile;
                updateStudent.Username = updateStudentViewModel.Username;
                updateStudent.CityId = updateStudentViewModel.CityId;
                updateStudent.StateId = updateStudentViewModel.StateId;
                updateStudent.ZipId = updateStudentViewModel.ZipId;
                updateStudent.IsActive = updateStudentViewModel.IsActive;


                UpdateStudentResponseModel updateStudentResponseModel = null;
                ViewBag.UpdateStudentSuccess = null;
                ViewBag.UpdateStudentError = null;
                ResponseModel responseModel = new ResponseModel();

                updateStudentResponseModel = await _studentRepository.UpdateStudent(updateStudent);


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

                            return RedirectToAction("ManageStudentUsers", "User");
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
            var data = await _studentRepository.GetAllStudentDetails();
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
            int recsCount = (await _studentRepository.GetAllStudentDetails()).data.Count();
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

            var dataList = await _studentRepository.GetAllStudentDetails();
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
            int recsCount = (await _studentRepository.GetAllStudentDetails()).data.Count();
            if (page < 1)
                page = 1;
            var pager = new Pager(recsCount, page, size);

            ViewBag.Pager = pager;
            var data = new List<StudentDetailsViewModel>();

            var dataList = await _studentRepository.GetStudentByPagination(page, size);

            foreach (var student in dataList.data.GetStudentListPaginationDto)
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

            var student = await _studentRepository.GetStudentById(Id);
            return student;
        }

        [HttpGet]
        public async Task<IActionResult> DownloadExcel()
        {
            var data = new List<StudentDetailsViewModel>();

            var dataList = await _studentRepository.GetAllStudentDetails();
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
            var data = new List<StudentDetailsViewModel>();
            var dataList = await _studentRepository.GetStudentByFilters( ClassName, SectionName,  StudentName, IsActive, CreatedDate);
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

        public ActionResult AddParents()
        {
            return View();
        }
    }
}
