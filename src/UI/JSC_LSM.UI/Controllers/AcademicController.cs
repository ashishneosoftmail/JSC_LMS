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
        public IActionResult AddAcademic()
        {
            return View();
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
        public async Task<IEnumerable<AcademicViewModel>> GetAcademicByFilters(string className, string schoolName, string sectionName, string subjectName,string teacherName,string Type, DateTime createdDate, bool isActive)
        {
            var data = new List<AcademicViewModel>();
            var dataList = await _academicRepository.GetAcademicByFilters(schoolName, className, sectionName, subjectName,teacherName,Type, createdDate, isActive);
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

    }
}
