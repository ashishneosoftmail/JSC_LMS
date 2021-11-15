
using JSC_LSM.UI.Models;
using JSC_LSM.UI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JSC_LSM.UI.Services.IRepositories;
using JSC_LMS.Application.Features.Teachers.Commands.CreateTeacher;
using JSC_LSM.UI.ResponseModels;
using JSC_LMS.Application.Features.Teachers.Commands.UpdateTeacher;
using System.Data;
using ClosedXML.Excel;
using System.IO;

namespace JSC_LSM.UI.Controllers
{
    public class TeacherController : BaseController
    {
        private readonly IStateRepository _stateRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly JSC_LSM.UI.Common.Common _common;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly ISchoolRepository _schoolRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IClassRepository _classRepository;

        public TeacherController(IStateRepository stateRepository, ISchoolRepository schoolRepository, JSC_LSM.UI.Common.Common common, IOptions<ApiBaseUrl> apiBaseUrl, ITeacherRepository teacherRepository , IClassRepository classRepository, ISectionRepository sectionRepository , ISubjectRepository subjectRepository)
        {
            _stateRepository = stateRepository;
            _teacherRepository = teacherRepository;
            _common = common;
            _apiBaseUrl = apiBaseUrl;
            _schoolRepository = schoolRepository;
            _classRepository = classRepository;
            _sectionRepository = sectionRepository;
            _subjectRepository = subjectRepository;

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
        public async Task<IActionResult> AddTeacher()
        {
            ViewBag.AddTeacherSuccess = null;
            ViewBag.AddTeacherError = null;
            Teacher teacher = new Teacher();
            teacher.States = await _common.GetAllStates();
            teacher.Schools = await _common.GetSchool();
            teacher.Class = await _common.GetClass();
            teacher.Section = await _common.GetSection();
            teacher.Subject = await _common.GetSubject();
            return View(teacher);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTeacher(Teacher teacher)
        {
            ViewBag.AddTeacherSuccess = null;
            ViewBag.AddTeacherError = null;
            teacher.States = await _common.GetAllStates();
            teacher.Schools = await _common.GetSchool();
            teacher.Class = await _common.GetClass();
            teacher.Section = await _common.GetSection();
            teacher.Subject = await _common.GetSubject();
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
                createNewTeacher.SchoolId = teacher.SchoolId;


                TeacherResponseModel teacherResponseModel = null;
                ViewBag.AddTeacherSuccess = null;
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
                            newTeacherModel.Schools = await _common.GetSchool();
                            newTeacherModel.Class = await _common.GetClass();
                            newTeacherModel.Section = await _common.GetSection();
                            newTeacherModel.Subject = await _common.GetSubject();
                            return RedirectToAction("AddTeacher", "Teacher");
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

        [HttpGet]
        public async Task<IActionResult> EditTeacher(int id)
        {
            var teacher = await _teacherRepository.GetTeacherById(id);
            if (teacher.data == null)
            {
                TempData["GetTeacherById"] = teacher.message;
                return RedirectToAction("ManageStudentUsers", "Teacher");
            }
            var teacherData = new UpdateTeacherViewModel()
            {
                Id = teacher.data.Id,
                UserType = teacher.data.UserType,
                TeacherName = teacher.data.TeacherName,
                AddressLine1 = teacher.data.AddressLine1,
                UserId = teacher.data.UserId,
                AddressLine2 = teacher.data.AddressLine2,
                CityId = teacher.data.City.Id,
                StateId = teacher.data.State.Id,
                Email = teacher.data.Email,
                IsActive = teacher.data.IsActive,
                Mobile = teacher.data.Mobile,
                SchoolId = teacher.data.School.Id,
                ClassId = teacher.data.Class.Id,
                SectionId = teacher.data.Section.Id,
                SubjectId=teacher.data.Subject.Id,
                Username = teacher.data.Username,
                ZipId = teacher.data.Zip.Id
            };
            teacherData.Schools = await _common.GetSchool();
            teacherData.Class = await _common.GetClass();
            teacherData.Section = await _common.GetSection();
            teacherData.Subject = await _common.GetSubject();
            TempData["UserId"] = teacherData.UserId;
            teacherData.States = await _common.GetAllStates();
            teacherData.Cities = await _common.GetAllCityByStateId(teacher.data.State.Id);
            teacherData.ZipCode = await _common.GetAllZipByCityId(teacher.data.City.Id);
            return View(teacherData);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTeacher(UpdateTeacherViewModel updateTeacherViewModel)
        {
            ViewBag.UpdateTeacherSuccess = null;
            ViewBag.UpdateTeacherError = null;
            updateTeacherViewModel.Schools = await _common.GetSchool();
            updateTeacherViewModel.Class = await _common.GetClass();
            updateTeacherViewModel.Section = await _common.GetSection();
            updateTeacherViewModel.Subject = await _common.GetSubject();
            updateTeacherViewModel.States = await _common.GetAllStates();
            updateTeacherViewModel.Cities = await _common.GetAllCityByStateId(updateTeacherViewModel.StateId);
            updateTeacherViewModel.ZipCode = await _common.GetAllZipByCityId(updateTeacherViewModel.CityId);
            UpdateTeacherDto updateTeacher = new UpdateTeacherDto();
            if (ModelState.IsValid)
            {
                updateTeacher.Id = updateTeacherViewModel.Id;
                updateTeacher.UserType = "Teacher";
                updateTeacher.UserId = TempData["UserId"].ToString();
                updateTeacher.SchoolId = updateTeacherViewModel.SchoolId;
                updateTeacher.ClassId = updateTeacherViewModel.ClassId;
                updateTeacher.SectionId = updateTeacherViewModel.SectionId;
                updateTeacher.SubjectId = updateTeacherViewModel.SubjectId;
                updateTeacher.AddressLine1 = updateTeacherViewModel.AddressLine1;
                updateTeacher.AddressLine2 = updateTeacherViewModel.AddressLine2;
                updateTeacher.TeacherName = updateTeacherViewModel.TeacherName;
                updateTeacher.Email = updateTeacherViewModel.Email;
                updateTeacher.Mobile = updateTeacherViewModel.Mobile;
                updateTeacher.Username = updateTeacherViewModel.Username;
                updateTeacher.CityId = updateTeacherViewModel.CityId;
                updateTeacher.StateId = updateTeacherViewModel.StateId;
                updateTeacher.ZipId = updateTeacherViewModel.ZipId;
                updateTeacher.IsActive = updateTeacherViewModel.IsActive;


                UpdateTeacherResponseModel updateTeacherResponseModel = null;
                ViewBag.UpdateTeacherSuccess = null;
                ViewBag.UpdateTeacherError = null;
                ResponseModel responseModel = new ResponseModel();

                updateTeacherResponseModel = await _teacherRepository.UpdateTeacher(updateTeacher);


                if (updateTeacherResponseModel.Succeeded)
                {
                    if (updateTeacherResponseModel == null && updateTeacherResponseModel?.data == null)
                    {
                        responseModel.ResponseMessage = updateTeacherResponseModel.message;
                        responseModel.IsSuccess = updateTeacherResponseModel.Succeeded;
                    }
                    if (updateTeacherResponseModel != null)
                    {
                        if (updateTeacherResponseModel?.data != null)
                        {
                            responseModel.ResponseMessage = updateTeacherResponseModel.message;
                            responseModel.IsSuccess = updateTeacherResponseModel.Succeeded;
                            ViewBag.UpdateTeacherSuccess = "Details Updated Successfully";

                            return RedirectToAction("EditTeacher", "Teacher");
                        }
                        else
                        {
                            responseModel.ResponseMessage = updateTeacherResponseModel.message;
                            responseModel.IsSuccess = updateTeacherResponseModel.Succeeded;
                            ViewBag.UpdateTeacherError = updateTeacherResponseModel.message;
                            return View(updateTeacherResponseModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = updateTeacherResponseModel.message;
                    responseModel.IsSuccess = updateTeacherResponseModel.Succeeded;
                    ViewBag.UpdatePrincipalError = updateTeacherResponseModel.message;
                }
            }
            return View(updateTeacherViewModel);
        }

        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ManageTeacherUsers()
        {
            var page = 1;
            var size = 5;
            int recsCount = (await _teacherRepository.GetAllTeacherDetails()).data.Count();
            if (page < 1)
                page = 1;
            ViewBag.GetTeacherById = TempData["GetTeacherById"] as string;
            var pager = new Pager(recsCount, page, size);
            ViewBag.Pager = pager;
            return View(pager);
        }

        [HttpGet]
        public async Task<IEnumerable<TeacherDetailsViewModel>> GetAllTeacherDetails()
        {
            var data = new List<TeacherDetailsViewModel>();

            var dataList = await _teacherRepository.GetAllTeacherDetails();
            foreach (var teacher in dataList.data)
            {
                data.Add(new TeacherDetailsViewModel()
                {
                    Id = teacher.Id,
                    TeacherName = teacher.TeacherName,
                    AddressLine1 = teacher.AddressLine1,
                    AddressLine2 = teacher.AddressLine2,
                    City = teacher.City.CityName,
                    State = teacher.State.StateName,
                    CreatedDate = (DateTime)teacher.CreatedDate,
                    Email = teacher.Email,
                    IsActive = teacher.IsActive,
                    Mobile = teacher.Mobile,
                    School = teacher.SchoolId.SchoolName,
                    Class = teacher.ClassId.ClassName,
                    Section = teacher.SectionId.SectionName,
                    Subject = teacher.SubjectId.SubjectName,
                    Username = teacher.Username,
                    Zip = teacher.Zip.Zipcode
                });
            }
            return data;
        }

        [HttpGet]
        public async Task<IEnumerable<TeacherDetailsViewModel>> GetAllTeacherDetailsByPagination(int page = 1, int size = 5)
        {
            int recsCount = (await _teacherRepository.GetAllTeacherDetails()).data.Count();
            if (page < 1)
                page = 1;
            var pager = new Pager(recsCount, page, size);

            ViewBag.Pager = pager;
            var data = new List<TeacherDetailsViewModel>();

            var dataList = await _teacherRepository.GetTeacherByPagination(page, size);

            foreach (var teacher in dataList.data.GetTeacherListPaginationDto)
            {
                data.Add(new TeacherDetailsViewModel()
                {
                         Id=teacher.Id,
                         School=teacher.SchoolId.SchoolName,
                         Class=teacher.ClassId.ClassName,
                         Subject=teacher.SubjectId.SubjectName,
                         Section=teacher.SectionId.SectionName,
                         TeacherName = teacher.TeacherName,
                         CreatedDate = (DateTime)teacher.CreatedDate,
                         IsActive = teacher.IsActive

                   
                });
            }
            return data;
        }

        [HttpGet]
        public async Task<GetTeacherByIdResponseModel> GetTeacherById(int Id)
        {

            var principal = await _teacherRepository.GetTeacherById(Id);
            return principal;
        }

        [HttpGet]
        public async Task<IActionResult> DownloadExcel()
        {
            var data = new List<TeacherDetailsViewModel>();

            var dataList = await _teacherRepository.GetAllTeacherDetails();
            //Creating DataTable  
            DataTable dt = new DataTable();
            //Setiing Table Name  
            dt.TableName = "TeacherData";
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("User_type", typeof(string));
            dt.Columns.Add("Teacher_Name", typeof(string));
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
            dt.Columns.Add("Class_Id", typeof(int));
            dt.Columns.Add("Class_Name", typeof(string));
            dt.Columns.Add("Section_Id", typeof(int));
            dt.Columns.Add("Section_Name", typeof(string));
            dt.Columns.Add("Subject_Id", typeof(int));
            dt.Columns.Add("Subject_Name", typeof(string));
            dt.Columns.Add("CreatedDate", typeof(DateTime));
            foreach (var _teacher in dataList.data)
            {
                dt.Rows.Add(_teacher.Id,_teacher.UserType, _teacher.TeacherName, _teacher.AddressLine1, _teacher.AddressLine2, _teacher.Mobile, _teacher.Username, _teacher.Email, _teacher.IsActive ? "Active" : "Inactive", _teacher.City.Id, _teacher.City.CityName, _teacher.State.Id, _teacher.State.StateName, _teacher.Zip.Id, _teacher.Zip.Zipcode, _teacher.SchoolId.Id, _teacher.SchoolId.SchoolName, _teacher.ClassId.Id, _teacher.ClassId.ClassName, _teacher.SectionId.Id, _teacher.SectionId.SectionName, _teacher.SubjectId.Id, _teacher.SubjectId.SubjectName, _teacher.CreatedDate?.ToShortDateString());
            }
            string fileName = "TeacherData_" + DateTime.Now.ToShortDateString() + ".xlsx";

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
}
