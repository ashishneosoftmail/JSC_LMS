using JSC_LMS.Application.Features.Feedback.Commands.CreateFeedback;
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
        private readonly IUserRepository _userRepository;
        private readonly IEventsDetailsRepository _eventsRepository;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IStudentRepository _studentRepository;
        public ParentController(ICircularRepository circularRepository, JSC_LSM.UI.Common.Common common, IParentsRepository parentRepository, IConfiguration configuration, IWebHostEnvironment webHostEnvironment, IAnnouncementRepository announcementRepository, ITeacherRepository teacherRepository, ISchoolRepository schoolRepository, IPrincipalRepository principalRepository, ISubjectRepository subjectRepository , IUserRepository userRepository , IEventsDetailsRepository eventsRepository,   IOptions<ApiBaseUrl> apiBaseUrl , IFeedbackRepository feedbackRepository , IStudentRepository studentRepository)
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
            _userRepository = userRepository;
            _eventsRepository = eventsRepository;
            _apiBaseUrl = apiBaseUrl;
            _feedbackRepository = feedbackRepository;
            _studentRepository = studentRepository;
        }
        public async Task<IActionResult> Index()
        {
            ParentClassInformationModel model = new ParentClassInformationModel();
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var parentData = await _parentRepository.GetParentByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);
            var getParent = await _parentRepository.GetParentsById(_apiBaseUrl.Value.LmsApiBaseUrl,parentData.data.Id);
            //string[] studentList = getParent.data..Split(",");
            model.StudentName = "";
            foreach (var d in getParent.data.Student)
            {
                model.StudentName = d.StudentName + "," + model.StudentName;
            }
            var school = await _schoolRepository.GetSchoolById(_apiBaseUrl.Value.LmsApiBaseUrl,getParent.data.SchoolId);
            model.SchoolName = school.data.SchoolName;
            model.ClassName = getParent.data.Class.ClassName;
            model.SectionName = getParent.data.Section.SectionName;
            List<SubjectTeacher> subList = new List<SubjectTeacher>();
            var teacherlist = (await _teacherRepository.GetAllTeacherDetails(_apiBaseUrl.Value.LmsApiBaseUrl)).data.Where(x => x.SchoolId.Id == getParent.data.SchoolId).Where(x => x.SectionId.Id == getParent.data.Section.Id).Where(x => x.ClassId.Id == getParent.data.Class.Id).ToList();
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
            ParentsChartDetails model = new ParentsChartDetails();

            var userId = Convert.ToString(Request.Cookies["Id"]);
            var parents = await _parentRepository.GetParentByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);
            model.CircularCount = (await _circularRepository.GetAllCircularBySchoolList(_apiBaseUrl.Value.LmsApiBaseUrl,parents.data.SchoolId)).data.Count();
            model.EventsCount = (await _eventsRepository.GetAllEventsBySchoolList(_apiBaseUrl.Value.LmsApiBaseUrl,parents.data.SchoolId)).data.Count();
            model.AnnouncementCount = (await _announcementRepository.GetAllAnnouncementBySchoolClassSectionList(_apiBaseUrl.Value.LmsApiBaseUrl,parents.data.SchoolId , parents.data.Classid, parents.data.Sectionid)).data.Count();
            return View(model);
        }


        [HttpGet]
        public JsonResult EventsDataParentsBarChart()
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var parents = _parentRepository.GetParentByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId).GetAwaiter().GetResult();
            var eventsData = _eventsRepository.GetAllEventsBySchoolList(_apiBaseUrl.Value.LmsApiBaseUrl,parents.data.SchoolId).GetAwaiter().GetResult();

            var list = eventsData.data.Where(v => v.EventDateTime.Year == DateTime.Now.Year).GroupBy(u => u.EventDateTime.Month)
                          .Select(u => new EventsParentsData
                          {
                              EventsParentsCount = u.Count(),
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
                        new EventsParentsData
                        {
                            EventsParentsCount = 0,
                            Month = i.ToString()
                        });
                }
            }
            list.Sort(new EventsParentsDataSortByMonth());
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
        public async Task<IActionResult> ManageCircular(int page = 1, int size = 5)
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var principal = await _parentRepository.GetParentByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);
            int recsCount = (await _circularRepository.GetAllCircularBySchoolList(_apiBaseUrl.Value.LmsApiBaseUrl,principal.data.SchoolId)).data.Count();
            if (page < 1)
                page = 1;
            var pager = new Pager(recsCount, page, size);
            ViewBag.Pager = pager;
            ManageCircularModel model = new ManageCircularModel();
            model.Pager = pager;

            var paginationData = await _circularRepository.GetCircularListBySchoolPagination(_apiBaseUrl.Value.LmsApiBaseUrl,page, size, principal.data.SchoolId);
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
            var circular = await _circularRepository.GetCircularById(_apiBaseUrl.Value.LmsApiBaseUrl,Id);
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
            var principal = await _parentRepository.GetParentByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);
            List<CircularPagination> data = new List<CircularPagination>();
            var dataList = await _circularRepository.GetAllCircularListByFilterSchoolAndCreatedDate(_apiBaseUrl.Value.LmsApiBaseUrl,circularTitle, description, date, principal.data.SchoolId);
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
            var data = await _teacherRepository.GetAllTeacherDetails(_apiBaseUrl.Value.LmsApiBaseUrl);
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
            var parent = await _parentRepository.GetParentByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);
            int recsCount = (await _announcementRepository.GetAllAnnouncementBySchoolClassSectionList(_apiBaseUrl.Value.LmsApiBaseUrl,parent.data.SchoolId, parent.data.Classid, parent.data.Sectionid)).data.Count();
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
            var paginationData = await _announcementRepository.GetAnnouncementListBySchoolClassSectionPagination(_apiBaseUrl.Value.LmsApiBaseUrl,page, size, parent.data.SchoolId, parent.data.Classid, parent.data.Sectionid);
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
            var parent = await _parentRepository.GetParentByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);

            List<AnnouncementPagination> data = new List<AnnouncementPagination>();
            var dataList = await _announcementRepository.GetAnnouncementByFilters(_apiBaseUrl.Value.LmsApiBaseUrl,parent.data.SchoolId, parent.data.Classid, parent.data.Sectionid, 0, "Select Teacher", "Select Type", AnnouncementTitle, AnnouncementContent, CreatedDate);
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
            var announcement = await _announcementRepository.GetAnnouncementById(_apiBaseUrl.Value.LmsApiBaseUrl,Id);
            return announcement;
        }

        public async Task<IActionResult> ManageFeedback()
        {
            var data = new List<GetAllFeedbackDetails>();
            FeedbackModel model = new FeedbackModel();
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var parent = await _parentRepository.GetParentByUserId(_apiBaseUrl.Value.LmsApiBaseUrl, userId);

            model.Classes = await _common.GetClass();
            model.Subjects = await _common.GetSubject();
            model.Sections = await _common.GetSection();
            model.Students = await _common.GetAllsStudent();
            model.Parents = await _common.GetAllParents();



            var dataList = await _feedbackRepository.GetAllFeedbackDetails(_apiBaseUrl.Value.LmsApiBaseUrl);
            var tempstatus = "";
            foreach (var feedbackdata in dataList.data)
            {
                if (feedbackdata.ParentId == parent.data.Id)
                {
                    data.Add(new GetAllFeedbackDetails()
                    {

                        Id = feedbackdata.Id,
                        feedbackTitle = feedbackdata.feedbackTitle.Feedback_Title,
                        ClassName = feedbackdata.Classes.ClassName,
                        SchoolName = feedbackdata.School.SchoolName,
                        SectionName = feedbackdata.Section.SectionName,
                        SubjectName = feedbackdata.Subject.SubjectName,
                        StudentName = feedbackdata.Students.StudentName,
                        ParentName = feedbackdata.Parents.ParentName,
                        FeedbackType = feedbackdata.FeedbackType,
                        SendDate = feedbackdata.SendDate,



                    });
                }

            }
            model.GetFeedbackList = data;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddFeedback()
        {
            FeedbackModel feedbackModel = new FeedbackModel();
            feedbackModel.Classes = await _common.GetClass();
            feedbackModel.Sections = await _common.GetSection();
            feedbackModel.Subjects = await _common.GetSubject();
            feedbackModel.Students = await _common.GetAllsStudent();
            feedbackModel.Parents = await _common.GetAllParents();
            feedbackModel.FeedbackTitles = await _common.GetFeedbackTitle();
            //feedbackModel.FeedbackTitles = await _common.GetFeedbackTitle();

            return View(feedbackModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFeedback(FeedbackModel feedbackModel)
        {
            ViewBag.AddFeedbackSuccess = null;
            ViewBag.AddFeedbackError = null;

            feedbackModel.Classes = await _common.GetClass();
            feedbackModel.Sections = await _common.GetSection();
            feedbackModel.Subjects = await _common.GetSubject();
            feedbackModel.Students = await _common.GetAllsStudent();
            feedbackModel.Parents = await _common.GetAllParents();
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var parent = await _parentRepository.GetParentByUserId(_apiBaseUrl.Value.LmsApiBaseUrl, userId);
            var parentbyid = await _parentRepository.GetParentsById(_apiBaseUrl.Value.LmsApiBaseUrl , parent.data.Id);

            var students = await _studentRepository.GetAllStudentDetails(_apiBaseUrl.Value.LmsApiBaseUrl);

            CreateFeedbackDto createNewFeedback = new CreateFeedbackDto();

            if (ModelState.IsValid)
            {

                createNewFeedback.SubjectId = feedbackModel.AddNewFeedbacks.SubjectId;
                createNewFeedback.SectionId = parent.data.Sectionid;
                createNewFeedback.ClassId = parent.data.Classid;
                createNewFeedback.SchoolId = parent.data.SchoolId;
                
                createNewFeedback.ParentId = parent.data.Id;
                createNewFeedback.FeedbackTitleId = feedbackModel.AddNewFeedbacks.FeedbackTitleId;
                createNewFeedback.FeedbackType = "Parent";
                createNewFeedback.IsActive = true;
                createNewFeedback.FeedbackComments = feedbackModel.AddNewFeedbacks.Comments;
                createNewFeedback.SendDate = DateTime.Now;
                foreach (var i in students.data)
                {
                    if (i.StudentName == parentbyid.data.Student[0].StudentName)
                        createNewFeedback.StudentId = i.Id;
                }


                FeedbackResponseModel feedbackResponseModel = null;
                ViewBag.AddFeedbackSuccess = null;
                ViewBag.AddFeedbackError = null;
                ResponseModel responseModel = new ResponseModel();

                feedbackResponseModel = await _feedbackRepository.AddNewFeedback(_apiBaseUrl.Value.LmsApiBaseUrl, createNewFeedback);


                if (feedbackResponseModel.Succeeded)
                {
                    if (feedbackResponseModel == null && feedbackResponseModel.data == null)
                    {
                        responseModel.ResponseMessage = feedbackResponseModel.message;
                        responseModel.IsSuccess = feedbackResponseModel.Succeeded;
                    }
                    if (feedbackResponseModel != null)
                    {
                        if (feedbackResponseModel.data != null)
                        {
                            responseModel.ResponseMessage = feedbackResponseModel.message;
                            responseModel.IsSuccess = feedbackResponseModel.Succeeded;
                            ViewBag.AddSectionSuccess = "Details Added Successfully";
                            ModelState.Clear();
                            var newFeedbackModel = new FeedbackModel();

                            newFeedbackModel.Students = await _common.GetAllsStudent();
                            newFeedbackModel.Classes = await _common.GetClass();
                            newFeedbackModel.Subjects = await _common.GetSubject();
                            newFeedbackModel.Sections = await _common.GetSection();
                            newFeedbackModel.Parents = await _common.GetAllParents();



                            RedirectToAction("ManageFeedback","Parent");



                        }
                        else
                        {
                            responseModel.ResponseMessage = feedbackResponseModel.message;
                            responseModel.IsSuccess = feedbackResponseModel.Succeeded;
                            ViewBag.AddSubjectError = feedbackResponseModel.message;
                            return View(feedbackModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = feedbackResponseModel.message;
                    responseModel.IsSuccess = feedbackResponseModel.Succeeded;
                    ViewBag.AddFeedbackError = feedbackResponseModel.message;
                }
            }
            return View(feedbackModel);


        }
    }
}
