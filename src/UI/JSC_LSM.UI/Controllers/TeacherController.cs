
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


        public ActionResult ManageStudentUsers()
        {
            return View();
        }

    }
}
