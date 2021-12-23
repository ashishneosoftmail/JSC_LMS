using JSC_LSM.UI.Helpers;
using JSC_LSM.UI.Models;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Controllers
{
    public class StudentController : BaseController
    {
        private readonly IStateRepository _stateRepository;
        private readonly ICircularRepository _circularRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly JSC_LSM.UI.Common.Common _common;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly ISchoolRepository _schoolRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IClassRepository _classRepository;
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IConfiguration _configuration;
        private readonly IEventsDetailsRepository _eventsRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public StudentController(IStateRepository stateRepository, ISchoolRepository schoolRepository, JSC_LSM.UI.Common.Common common, IOptions<ApiBaseUrl> apiBaseUrl, ITeacherRepository teacherRepository, IClassRepository classRepository, ISectionRepository sectionRepository, ISubjectRepository subjectRepository, IAnnouncementRepository announcementRepository, ICircularRepository circularRepository, IStudentRepository studentRepository, IConfiguration configuration, IWebHostEnvironment webHostEnvironment , IEventsDetailsRepository eventsRepository)
        {
            _stateRepository = stateRepository;
            _teacherRepository = teacherRepository;
            _common = common;
            _apiBaseUrl = apiBaseUrl;
            _schoolRepository = schoolRepository;
            _classRepository = classRepository;
            _sectionRepository = sectionRepository;
            _subjectRepository = subjectRepository;
            _announcementRepository = announcementRepository;
            _circularRepository = circularRepository;
            _studentRepository = studentRepository;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _eventsRepository = eventsRepository;
        }
        public async Task<IActionResult> Index()
        {
            ParentClassInformationModel model = new ParentClassInformationModel();
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var studentData = await _studentRepository.GetStudentByUserId(userId);
            var getStudent = await _studentRepository.GetStudentById(studentData.data.Id);

            model.StudentName = getStudent.data.StudentName;

            var school = await _schoolRepository.GetSchoolById(getStudent.data.SchoolId);
            model.SchoolName = school.data.SchoolName;
            model.ClassName = getStudent.data.Class.ClassName;
            model.SectionName = getStudent.data.Section.SectionName;
            List<SubjectTeacher> subList = new List<SubjectTeacher>();
            var teacherlist = (await _teacherRepository.GetAllTeacherDetails()).data.Where(x => x.SchoolId.Id == getStudent.data.SchoolId).Where(x => x.SectionId.Id == getStudent.data.Section.Id).Where(x => x.ClassId.Id == getStudent.data.Class.Id).ToList();
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

        public async Task<IActionResult> Dashboard()
        {
            StudentChartDetails model = new StudentChartDetails();
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var student =await _studentRepository.GetStudentByUserId(userId);
            model.CircularCount = (await _circularRepository.GetAllCircularBySchoolList(student.data.Schoolid)).data.Count();
            model.EventsCount = (await _eventsRepository.GetAllEventsBySchoolList(student.data.Schoolid)).data.Count();
            model.AnnouncementCount = (await _announcementRepository.GetAllAnnouncementBySchoolClassSectionList(student.data.Schoolid, student.data.Classid, student.data.Sectionid)).data.Count();
            return View(model);
        }

        [HttpGet]
        public JsonResult EventsDataStudentsBarChart()
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var student = _studentRepository.GetStudentByUserId(userId).GetAwaiter().GetResult();
            var eventsData = _eventsRepository.GetAllEventsBySchoolList(student.data.Schoolid).GetAwaiter().GetResult();

            var list = eventsData.data.Where(v => v.EventDateTime.Year == DateTime.Now.Year).GroupBy(u => u.EventDateTime.Month)
                          .Select(u => new EventsStudentData
                          {
                              EventsStudentsCount = u.Count(),
                              Month = u.FirstOrDefault().EventDateTime.Month.ToString()
                          }).ToList();
            for (int i = 1; i <= 12; i++)
            {
                int f = 0;
                for (int j = 0; j < list.Count(); j++)
                {
                    if (i == Convert.ToInt32(list[j].Month)) { f = 1; break; }

                }
                if (f == 0)
                {

                    list.Add(
                        new EventsStudentData
                        {
                            EventsStudentsCount = 0,
                            Month = i.ToString()
                        });
                }
            }
            list.Sort(new EventsStudentsDataSortByMonth());
            foreach (var userdata in list)
            {
                switch (userdata.Month)
                {
                    case "1":
                        userdata.Month = "Jan";
                        break;
                    case "2":
                        userdata.Month = "Feb";
                        break;
                    case "3":
                        userdata.Month = "Mar";
                        break;
                    case "4":
                        userdata.Month = "Apr";
                        break;
                    case "5":
                        userdata.Month = "May";
                        break;
                    case "6":
                        userdata.Month = "Jun";
                        break;
                    case "7":
                        userdata.Month = "Jul";
                        break;
                    case "8":
                        userdata.Month = "Aug";
                        break;
                    case "9":
                        userdata.Month = "Sep";
                        break;
                    case "10":
                        userdata.Month = "Oct";
                        break;
                    case "11":
                        userdata.Month = "Nov";
                        break;
                    case "12":
                        userdata.Month = "Dec";
                        break;
                    default:
                        userdata.Month = "error";
                        break;
                }
            }
            return Json(list);

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
            var student = await _studentRepository.GetStudentByUserId(userId);
            int recsCount = (await _announcementRepository.GetAllAnnouncementBySchoolClassSectionList(student.data.Schoolid, student.data.Classid, student.data.Sectionid)).data.Count();
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
            var paginationData = await _announcementRepository.GetAnnouncementListBySchoolClassSectionPagination(page, size, student.data.Schoolid, student.data.Classid, student.data.Sectionid);
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
            var student = await _studentRepository.GetStudentByUserId(userId);

            List<AnnouncementPagination> data = new List<AnnouncementPagination>();
            var dataList = await _announcementRepository.GetAnnouncementByFilters(student.data.Schoolid, student.data.Classid, student.data.Sectionid, 0, "Select Teacher", "Select Type", AnnouncementTitle, AnnouncementContent, CreatedDate);
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


        [HttpGet]
        public async Task<IActionResult> ManageCircular(int page = 1, int size = 5)
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var principal = await _studentRepository.GetStudentByUserId(userId);
            int recsCount = (await _circularRepository.GetAllCircularBySchoolList(principal.data.Schoolid)).data.Count();
            if (page < 1)
                page = 1;
            var pager = new Pager(recsCount, page, size);
            ViewBag.Pager = pager;
            ManageCircularModel model = new ManageCircularModel();
            model.Pager = pager;
            model.Schools = await _common.GetSchool();
            var paginationData = await _circularRepository.GetCircularListBySchoolPagination(page, size, principal.data.Schoolid);
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
            var principal = await _studentRepository.GetStudentByUserId(userId);
            List<CircularPagination> data = new List<CircularPagination>();
            var dataList = await _circularRepository.GetAllCircularListByFilterSchoolAndCreatedDate(circularTitle, description, date, principal.data.Schoolid);
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
            model.Schools = await _common.GetSchool();
            return View("ManageCircular", model);
        }


    }
}
