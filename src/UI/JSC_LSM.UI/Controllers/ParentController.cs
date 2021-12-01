using JSC_LSM.UI.Models;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Controllers
{
    public class ParentController : Controller
    {
        private readonly ICircularRepository _circularRepository;
        private readonly IParentsRepository _parentRepository;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly JSC_LSM.UI.Common.Common _common;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IPrincipalRepository _principalRepository;
        private readonly ISubjectRepository _subjectRepository;

        public ParentController(ICircularRepository circularRepository, JSC_LSM.UI.Common.Common common, IParentsRepository parentRepository, IConfiguration configuration, IWebHostEnvironment webHostEnvironment, IAnnouncementRepository announcementRepository, ITeacherRepository teacherRepository, ISchoolRepository schoolRepository, IPrincipalRepository principalRepository, ISubjectRepository subjectRepository)
        {
            _common = common;
            _circularRepository = circularRepository;
            _parentRepository = parentRepository;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _announcementRepository = announcementRepository;
            _teacherRepository = teacherRepository;
            _schoolRepository = schoolRepository;
            _principalRepository = principalRepository;
            _subjectRepository = subjectRepository;
        }
        public async Task<IActionResult> Index()
        {
            ParentClassInformationModel model = new ParentClassInformationModel();
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var parentData = await _parentRepository.GetParentByUserId(userId);
            var getParent = await _parentRepository.GetParentsById(parentData.data.Id);
            //string[] studentList = getParent.data..Split(",");
            model.StudentName = "";
            foreach (var d in getParent.data.Student)
            {
                model.StudentName = d.StudentName + "," + model.StudentName;
            }
            var school = await _schoolRepository.GetSchoolById(getParent.data.SchoolId);
            model.SchoolName = school.data.SchoolName;
            model.ClassName = getParent.data.Class.ClassName;
            model.SectionName = getParent.data.Section.SectionName;
            List<SubjectTeacher> subList = new List<SubjectTeacher>();
            var teacherlist = (await _teacherRepository.GetAllTeacherDetails()).data.Where(x => x.SchoolId.Id == getParent.data.SchoolId).Where(x => x.SectionId.Id == getParent.data.Section.Id).Where(x => x.ClassId.Id == getParent.data.Class.Id).ToList();
            foreach (var d in teacherlist)
            {
                subList.Add(new SubjectTeacher()
                {
                    SubjectName = d.SubjectId.SubjectName,
                    TeacherSubjectName = d.TeacherName,
                });
            }
            model.SubjectName = subList;
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> ManageCircular(int page = 1, int size = 5)
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var principal = await _parentRepository.GetParentByUserId(userId);
            int recsCount = (await _circularRepository.GetAllCircularBySchoolList(principal.data.SchoolId)).data.Count();
            if (page < 1)
                page = 1;
            var pager = new Pager(recsCount, page, size);
            ViewBag.Pager = pager;
            ManageCircularModel model = new ManageCircularModel();
            model.Pager = pager;

            var paginationData = await _circularRepository.GetCircularListBySchoolPagination(page, size, principal.data.SchoolId);
            List<CircularPagination> pagedData = new List<CircularPagination>();
            foreach (var data in paginationData.data)
            {
                if (data.Status)
                {
                    pagedData.Add(new CircularPagination()
                    {
                        Id = data.Id,
                        CircularTitle = data.CircularTitle,
                        Description = data.Description,
                        SchoolData = new JSC_LMS.Application.Features.Circulars.Queries.GetCircularListByPagination.SchoolDto() { Id = data.SchoolData.Id, SchoolName = data.SchoolData.SchoolName },
                        Status = data.Status,
                        CreatedDate = data.CreatedDate,

                    });
                }

            }
            model.CircularListPagination = pagedData;
            return View(model);
        }
        [HttpGet]
        public async Task<GetCircularByIdResponseModel> ViewCircular(int Id)
        {
            var circular = await _circularRepository.GetCircularById(Id);
            return circular;
        }
        [HttpGet]
        public IActionResult ViewFileData(string fileName)
        {
            ViewCircularFileModel model = new ViewCircularFileModel();
            model.FileName = fileName;

            var FilePath = _configuration["Circulars"];
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath + FilePath);
            model.FilePath = uploadsFolder + fileName;
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> SearchCircular(string circularTitle, string description, DateTime date)
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var principal = await _parentRepository.GetParentByUserId(userId);
            List<CircularPagination> data = new List<CircularPagination>();
            var dataList = await _circularRepository.GetAllCircularListByFilterSchoolAndCreatedDate(circularTitle, description, date, principal.data.SchoolId);
            if (dataList.data != null)
            {
                foreach (var d in dataList.data)
                {
                    if (d.Status)
                    {
                        data.Add(new CircularPagination()
                        {
                            Id = d.Id,
                            CircularTitle = d.CircularTitle,
                            Description = d.Description,
                            SchoolData = new JSC_LMS.Application.Features.Circulars.Queries.GetCircularListByPagination.SchoolDto() { Id = d.SchoolData.Id, SchoolName = d.SchoolData.SchoolName },
                            Status = d.Status,
                            CreatedDate = d.CreatedDate,


                        });
                    }
                }
            }
            else
            {

            }
            ManageCircularModel model = new ManageCircularModel();
            model.CircularListPagination = data;
            if (dataList.data.Count() == 0)
            {
                model.Pager = new Pager(1, 1, 1);
            }
            else
            {
                model.Pager = new Pager(dataList.data.Count(), 1, dataList.data.Count());
            }
            ViewBag.Pager = model.Pager;
            return View("ManageCircular", model);
        }


        [HttpGet]
        public async Task<List<SelectListItem>> GetTeacherName()
        {
            var data = await _teacherRepository.GetAllTeacherDetails();
            List<SelectListItem> teachers = new List<SelectListItem>();
            foreach (var item in data.data)
            {
                teachers.Add(new SelectListItem
                {
                    Text = item.TeacherName,
                    Value = Convert.ToString(item.TeacherName)
                });
            }
            return teachers;
        }
        [HttpGet]
        public async Task<IActionResult> ManageAnnouncement(int page = 1, int size = 20)
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var parent = await _parentRepository.GetParentByUserId(userId);
            int recsCount = (await _announcementRepository.GetAllAnnouncementBySchoolClassSectionList(parent.data.SchoolId, parent.data.Classid, parent.data.Sectionid)).data.Count();
            if (page < 1)
                page = 1;
            var pager = new Pager(recsCount, page, size);
            ViewBag.Pager = pager;
            ManageAnnouncementModel model = new ManageAnnouncementModel();
            model.Pager = pager;
            //model.Classes = await _common.GetClass();
            //model.Sections = await _common.GetSection();
            model.Subjects = await _common.GetSubject();
            model.Teachers = await GetTeacherName();
            var paginationData = await _announcementRepository.GetAnnouncementListBySchoolClassSectionPagination(page, size, parent.data.SchoolId, parent.data.Classid, parent.data.Sectionid);
            List<AnnouncementPagination> pagedData = new List<AnnouncementPagination>();
            foreach (var data in paginationData.data)
            {
                pagedData.Add(new AnnouncementPagination()
                {
                    Id = data.Id,
                    AnnouncementTitle = data.AnnouncementTitle,
                    AnnouncementContent = $"{data.AnnouncementContent.Substring(0, 10)}...",
                    Class = new JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination.ClassDto() { Id = data.Class.Id, ClassName = data.Class.ClassName },
                    Section = new JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination.SectionDto() { Id = data.Section.Id, SectionName = data.Section.SectionName },
                    Subject = new JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination.SubjectDto() { Id = data.Subject.Id, SubjectName = data.Subject.SubjectName },
                    Teacher = new JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination.TeacherDto() { Id = data.Teacher.Id, TeacherName = data.Teacher.TeacherName },
                    CreatedDate = data.CreatedDate,


                });
            }
            model.AnnouncementPagination = pagedData;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SearchAnnouncement(string AnnouncementTitle, string AnnouncementContent, DateTime CreatedDate)
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var parent = await _parentRepository.GetParentByUserId(userId);

            List<AnnouncementPagination> data = new List<AnnouncementPagination>();
            var dataList = await _announcementRepository.GetAnnouncementByFilters(parent.data.SchoolId, parent.data.Classid, parent.data.Sectionid, 0, "Select Teacher", "Select Type", AnnouncementTitle, AnnouncementContent, CreatedDate);
            if (dataList.data != null)
            {
                foreach (var d in dataList.data)
                {
                    data.Add(new AnnouncementPagination()
                    {
                        Id = d.Id,
                        AnnouncementTitle = d.AnnouncementTitle,
                        AnnouncementContent = $"{d.AnnouncementContent.Substring(0, 10)}...",
                        Class = new JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination.ClassDto()
                        {
                            Id = d.Class.Id,
                            ClassName = d.Class.ClassName
                        },
                        Section = new JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination.SectionDto()
                        {
                            Id = d.Section.Id,
                            SectionName = d.Section.SectionName
                        },
                        Subject = new JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination.SubjectDto()
                        {
                            Id = d.Subject.Id,
                            SubjectName = d.Subject.SubjectName
                        },
                        Teacher = new JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination.TeacherDto()
                        {
                            Id = d.Teacher.Id,
                            TeacherName = d.Teacher.TeacherName
                        },
                        CreatedDate = d.CreatedDate

                    });
                }
            }
            ManageAnnouncementModel model = new ManageAnnouncementModel();
            model.AnnouncementPagination = data;
            if (dataList.data.Count() == 0)
            {
                model.Pager = new Pager(1, 1, 1);
            }
            else
            {

                model.Pager = new Pager(dataList.data.Count(), 1, dataList.data.Count());
            }
            ViewBag.Pager = model.Pager;

            //model.Classes = await _common.GetClass();
            //model.Sections = await _common.GetSection();
            model.Subjects = await _common.GetSubject();
            model.Teachers = await GetTeacherName();
            return View("ManageAnnouncement", model);
        }

        [HttpGet]
        public async Task<GetAnnouncementByIdResponseModel> ViewAnnouncement(int Id)
        {
            var announcement = await _announcementRepository.GetAnnouncementById(Id);
            return announcement;
        }

    }
}
