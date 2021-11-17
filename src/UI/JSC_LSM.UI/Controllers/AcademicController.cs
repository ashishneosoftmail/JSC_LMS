using JSC_LMS.Application.Features.Academics.Commands.CreateAcademic;
using JSC_LMS.Application.Features.Academics.Commands.UpdateAcademic;
using JSC_LSM.UI.Models;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Controllers
{
    public class AcademicController : BaseController
    {

        private readonly JSC_LSM.UI.Common.Common _common;
        private readonly ISchoolRepository _schoolRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IClassRepository _classRepository;
        private readonly IAcademicRepository _academicRepository;
        private readonly ITeacherRepository _teacherRepository;
        public AcademicController(JSC_LSM.UI.Common.Common common, IClassRepository classRepository, ISchoolRepository schoolRepository, ISectionRepository sectionRepository, ISubjectRepository subjectRepository, ITeacherRepository teacherRepository,  IAcademicRepository academicRepository)
        {
            _common = common;
            _schoolRepository = schoolRepository;
            _sectionRepository = sectionRepository;
            _classRepository = classRepository;
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
            _academicRepository = academicRepository;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> ManageAcademic()
        {
            var page = 1;
            var size = 5;
            int recsCount = (await _academicRepository.GetAllAcademicDetails()).data.Count();
            if (page < 1)
                page = 1;

            var pager = new Pager(recsCount, page, size);
            this.ViewBag.Pager = pager;
            return View(pager);



        }
      


        [HttpGet]
        public async Task<GetAcademicByIdResponseModel> GetAcademicById(int Id)
        {

            var subjects = await _academicRepository.GetAcademicById(Id);
            return subjects;
        }


        [HttpGet]
        public async Task<IEnumerable<AcademicViewModel>> GetAllAcademicDetails()
        {
            var data = new List<AcademicViewModel>();

            var dataList = await _academicRepository.GetAllAcademicDetails();
            foreach (var academics in dataList.data)
            {
                data.Add(new AcademicViewModel()
                {
                    Id = academics.Id,

                    Type = academics.Type,

                    CutOff=academics.CutOff,

                    CreatedDate = academics.CreatedDate,

                    IsActive = academics.IsActive,

                    SchoolName = academics.School.SchoolName,

                    ClassName = academics.Class.ClassName,

                    SectionName = academics.Section.SectionName,

                    SubjectName = academics.Subject.SubjectName,

                    TeacherName = academics.Teacher.TeacherName,

                });
            }
            return data;
        }

        [HttpGet]
        public async Task<IEnumerable<AcademicViewModel>> GetAllAcademicDetailsByPagination(int page = 1, int size = 5)
        {
            int recsCount = (await _academicRepository.GetAllAcademicDetails()).data.Count();
            if (page < 1)
                page = 1;
            var pager = new Pager(recsCount, page, size);

            this.ViewBag.Pager = pager;
            var data = new List<AcademicViewModel>();

            //int recSkip = (page - 1) * size;
            var dataList = await _academicRepository.GetAcademicByPagination(page, size);

            foreach (var academics in dataList.data.GetAcademicListPaginationDto)
            {
                data.Add(new AcademicViewModel()
                {
                    Id = academics.Id,

                    Type = academics.Type,

                    CutOff = academics.CutOff,

                    CreatedDate = academics.CreatedDate,

                    IsActive = academics.IsActive,

                    SchoolName = academics.School.SchoolName,

                    ClassName = academics.Class.ClassName,

                    SectionName = academics.Section.SectionName,

                    SubjectName = academics.Subject.SubjectName,

                    TeacherName = academics.Teacher.TeacherName,

                });
            }
            return data;
        }
        [HttpGet]
        public async Task<List<SelectListItem>> GetAllSchool()
        {
            var schools = await _common.GetSchool();
            return schools;
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
        public async Task<List<SelectListItem>> GetAllSubject()
        {
            var subjects = await _common.GetSubject();
            return subjects;
        }

        [HttpGet]
        public async Task<List<SelectListItem>> GetAllTeacher()
        {
            var teachers = await _common.GetTeacher();
            return teachers;
        }

        [HttpGet]
        public async Task<IEnumerable<AcademicViewModel>> GetAcademicByFilters(string SchoolName, string ClassName, string SectionName, string SubjectName, string TeacherName, string Type, DateTime CreatedDate, bool IsActive)
        {
            var data = new List<AcademicViewModel>();
            var dataList = await _academicRepository.GetAcademicByFilters( SchoolName, ClassName,  SectionName, SubjectName, TeacherName,  Type, CreatedDate,  IsActive);
            if (dataList.data != null)
            {
                foreach (var academics in dataList.data)
                {
                    data.Add(new AcademicViewModel()
                    {
                        Id = academics.Id,

                        Type = academics.Type,

                        CutOff = academics.CutOff,

                        CreatedDate = academics.CreatedDate,

                        IsActive = academics.IsActive,

                        SchoolName = academics.School.SchoolName,

                        ClassName = academics.Class.ClassName,

                        SectionName = academics.Section.SectionName,

                        SubjectName = academics.Subject.SubjectName,

                        TeacherName = academics.Teacher.TeacherName,

                    });
                }
            }
            return data;
        }

        [HttpGet]
        public async Task<List<SelectListItem>> GetAcademicName()
        {
            var data = await _academicRepository.GetAllAcademicDetails();
            List<SelectListItem> academics = new List<SelectListItem>();
            foreach (var item in data.data)
            {
                academics.Add(new SelectListItem
                {
                    Text = item.Type,
                    Value = Convert.ToString(item.Type)


                });
            }
            return academics;
        }

        [HttpGet]
        public async Task<IActionResult> EditAcademic(int id)
        {
            var academic = await _academicRepository.GetAcademicById(id);
            if (academic.data == null)
            {
                TempData["GetAcademicById"] = academic.message;
                return RedirectToAction("ManageAcademic", "Academic");
            }
            var subjectData = new UpdateAcademicViewModel()
            {
                Id = academic.data.Id,
                Type = academic.data.Type,
                CutOff=academic.data.CutOff,

                IsActive = academic.data.IsActive,

                SchoolId = academic.data.School.Id,

                ClassId = academic.data.Class.Id,

                SectionId = academic.data.Section.Id,

                SubjectId = academic.data.Subject.Id,

                TeacherId = academic.data.Teacher.Id,


            };
            subjectData.Schools = await _common.GetSchool();
            subjectData.Classes = await _common.GetClass();
            subjectData.Sections = await _common.GetSection();
            subjectData.Subjects = await _common.GetSubject();
            subjectData.Teachers = await _common.GetTeacher();

            return View(subjectData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAcademic(UpdateAcademicViewModel updateAcademicViewModel)
        {
            ViewBag.UpdateAcademicSuccess = null;
            ViewBag.UpdateAcademicError = null;
            updateAcademicViewModel.Schools = await _common.GetSchool();
            updateAcademicViewModel.Classes = await _common.GetClass();
            updateAcademicViewModel.Sections = await _common.GetSection();
            updateAcademicViewModel.Subjects = await _common.GetSubject();
            updateAcademicViewModel.Teachers = await _common.GetTeacher();
            UpdateAcademicDto updateAcademic = new UpdateAcademicDto();
            updateAcademic.Id = updateAcademicViewModel.Id;
            if (ModelState.IsValid)
            {

                updateAcademic.SchoolId = updateAcademicViewModel.SchoolId;

                updateAcademic.ClassId = updateAcademicViewModel.ClassId;

                updateAcademic.SectionId = updateAcademicViewModel.SectionId;

                updateAcademic.SubjectId = updateAcademicViewModel.SubjectId;

                updateAcademic.TeacherId = updateAcademicViewModel.TeacherId;

              

                updateAcademic.Type = updateAcademicViewModel.Type;


                updateAcademic.CutOff = updateAcademicViewModel.CutOff;


                updateAcademic.IsActive = updateAcademicViewModel.IsActive;


                UpdateAcademicResponseModel updateAcademicResponseModel = null;
                ViewBag.UpdateSubjectSuccess = null;
                ViewBag.UpdateSubjectError = null;
                ResponseModel responseModel = new ResponseModel();

                updateAcademicResponseModel = await _academicRepository.UpdateAcademic(updateAcademic);


                if (updateAcademicResponseModel.Succeeded)
                {
                    if (updateAcademicResponseModel == null && updateAcademicResponseModel?.data == null)
                    {
                        responseModel.ResponseMessage = updateAcademicResponseModel.message;
                        responseModel.IsSuccess = updateAcademicResponseModel.Succeeded;
                    }
                    if (updateAcademicResponseModel != null)
                    {
                        if (updateAcademicResponseModel?.data != null)
                        {
                            responseModel.ResponseMessage = updateAcademicResponseModel.message;
                            responseModel.IsSuccess = updateAcademicResponseModel.Succeeded;
                            ViewBag.UpdateAcademicSuccess = "Details Updated Successfully";

                            return RedirectToAction("ManageAcademic", "Academic");
                        }
                        else
                        {
                            responseModel.ResponseMessage = updateAcademicResponseModel.message;
                            responseModel.IsSuccess = updateAcademicResponseModel.Succeeded;
                            ViewBag.UpdateAcademicError = updateAcademicResponseModel.message;
                            return View(updateAcademicResponseModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = updateAcademicResponseModel.message;
                    responseModel.IsSuccess = updateAcademicResponseModel.Succeeded;
                    ViewBag.UpdateSubjectError = updateAcademicResponseModel.message;
                }
            }
            return View(updateAcademicViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddAcademic()
        {
            AcademicModel academicModel = new AcademicModel();

            academicModel.Schools = await _common.GetSchool();
            academicModel.Classes = await _common.GetClass();
            academicModel.Sections = await _common.GetSection();
            academicModel.Subjects = await _common.GetSubject();
            academicModel.Teachers = await _common.GetTeacher();
            academicModel.Types = await GetAcademicName();

            return View(academicModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAcademic(AcademicModel academicModel)



        {
            ViewBag.AddAcademicSuccess = null;
            ViewBag.AddAcademicError = null;

            academicModel.Schools = await _common.GetSchool();
            academicModel.Classes = await _common.GetClass();
            academicModel.Sections = await _common.GetSection();
            academicModel.Subjects = await _common.GetSubject();
            academicModel.Teachers = await _common.GetTeacher();

            CreateAcademicDto createNewAcademic = new CreateAcademicDto();

            if (ModelState.IsValid)
            {

                createNewAcademic.SchoolId = academicModel.SchoolId;
                createNewAcademic.ClassId = academicModel.ClassId;
                createNewAcademic.SectionId = academicModel.SectionId;
                createNewAcademic.SubjectId = academicModel.SubjectId;
                createNewAcademic.TeacherId = academicModel.TeacherId;
                createNewAcademic.CutOff = academicModel.CutOff;
                createNewAcademic.Type = academicModel.Type;
                createNewAcademic.IsActive = academicModel.IsActive;

                AcademicResponseModel academicResponseModel = null;
                ViewBag.AddAcademicSuccess = null;
                ViewBag.AddAcademicError = null;
                ResponseModel responseModel = new ResponseModel();

                academicResponseModel = await _academicRepository.AddNewAcademic(createNewAcademic);


                if (academicResponseModel.Succeeded)
                {
                    if (academicResponseModel == null && academicResponseModel.data == null)
                    {
                        responseModel.ResponseMessage = academicResponseModel.message;
                        responseModel.IsSuccess = academicResponseModel.Succeeded;
                    }
                    if (academicResponseModel != null)
                    {
                        if (academicResponseModel.data != null)
                        {
                            responseModel.ResponseMessage = academicResponseModel.message;
                            responseModel.IsSuccess = academicResponseModel.Succeeded;
                            ViewBag.AddAcademicSuccess = "Details Added Successfully";
                            ModelState.Clear();
                            var newAcademicModel = new AcademicModel();

                            newAcademicModel.Schools = await _common.GetSchool();
                            newAcademicModel.Classes = await _common.GetClass();
                            newAcademicModel.Sections = await _common.GetSection();
                            newAcademicModel.Subjects = await _common.GetSubject();
                            newAcademicModel.Teachers = await _common.GetTeacher();
                            return RedirectToAction("ManageAcademic", "Academic");
                        }
                        else
                        {
                            responseModel.ResponseMessage = academicResponseModel.message;
                            responseModel.IsSuccess = academicResponseModel.Succeeded;
                            ViewBag.AddAcademicError = academicResponseModel.message;
                            return View(academicModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = academicResponseModel.message;
                    responseModel.IsSuccess = academicResponseModel.Succeeded;
                    ViewBag.AddAcademicError = academicResponseModel.message;
                }
            }
            return View(academicModel);

        }

      

    }
}
