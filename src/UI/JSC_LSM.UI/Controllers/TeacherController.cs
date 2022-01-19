
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
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using JSC_LMS.Application.Features.Announcement.Commands.CreateAnnouncement;
using JSC_LMS.Application.Features.Announcement.Commands.UpdateAnnouncement;
using JSC_LSM.UI.ResponseModels.GallaryResponseModel;
using JSC_LMS.Application.Features.Gallary.Commands.UploadImage;


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
        private readonly IConfiguration _configuration;
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly IInstituteRepository _instituteRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPrincipalRepository _principalRepository;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IGallaryRepository _gallaryRepository;
        public TeacherController(IStateRepository stateRepository, ISchoolRepository schoolRepository, JSC_LSM.UI.Common.Common common, IOptions<ApiBaseUrl> apiBaseUrl, ITeacherRepository teacherRepository , IClassRepository classRepository, ISectionRepository sectionRepository , ISubjectRepository subjectRepository , IInstituteRepository instituteRepository ,IAnnouncementRepository announcementRepository , IUserRepository userRepository , IPrincipalRepository principalRepository , IFeedbackRepository feedbackRepository, IGallaryRepository gallaryRepository, IConfiguration configuration)
        {
            _stateRepository = stateRepository;
            _teacherRepository = teacherRepository;
            _common = common;
            _configuration = configuration;
            _apiBaseUrl = apiBaseUrl;
            _schoolRepository = schoolRepository;
            _classRepository = classRepository;
            _sectionRepository = sectionRepository;
            _subjectRepository = subjectRepository;
            _announcementRepository = announcementRepository;
            _instituteRepository = instituteRepository;
            _userRepository = userRepository;
            _principalRepository = principalRepository;
            _feedbackRepository = feedbackRepository;
            _gallaryRepository=gallaryRepository;


        }


        public async Task<IActionResult> Index()
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var teacherUserID = await _teacherRepository.GetTeacherByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);
            var teacherData = (await _teacherRepository.GetAllTeacherDetails(_apiBaseUrl.Value.LmsApiBaseUrl)).data.Where(x => x.UserId == userId).ToList();
           
            List<TeacherInformation> model = new List<TeacherInformation>();
            foreach(var data in teacherData)
            {
                    var school = await _schoolRepository.GetSchoolById(_apiBaseUrl.Value.LmsApiBaseUrl,data.ClassId.Id);
                model.Add(new TeacherInformation()
                {
                    SchoolName = data.ClassId.ClassName,
                    InstituteName = (await _instituteRepository.GetInstituteById(_apiBaseUrl.Value.LmsApiBaseUrl,school.data.Institute.Id)).data.InstituteName,
                    ClassName = data.ClassId.ClassName,
                SectionName =data.SectionId.SectionName,
                SubjectName = data.SubjectId.SubjectName
                });
        }


            return View(model);

        }

        public async Task<IActionResult> Dashboard()
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            TeacherChartDetails model = new TeacherChartDetails();
            var teacher = await _teacherRepository.GetTeacherByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);
            model.AnnouncementCount = (await _announcementRepository.GetAllAnnouncementBySchoolList(_apiBaseUrl.Value.LmsApiBaseUrl,teacher.data.schoolid)).data.Count();
            return View(model);
        }

        [HttpGet]
        public JsonResult AnnouncementDataTeacherBarChart()
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var teacher = _teacherRepository.GetTeacherByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId).GetAwaiter().GetResult();
            var announcementData = _announcementRepository.GetAllAnnouncementBySchoolList(_apiBaseUrl.Value.LmsApiBaseUrl,teacher.data.schoolid).GetAwaiter().GetResult();

            var list = announcementData.data.Where(v => v.CreatedDate.Year == DateTime.Now.Year).GroupBy(u => u.CreatedDate.Month)
                          .Select(u => new AnnouncementPrincipalData
                          {
                              AnnouncementChartCount = u.Count(),
                              Month = u.FirstOrDefault().CreatedDate.Month.ToString()
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
                        new AnnouncementPrincipalData
                        {
                            AnnouncementChartCount = 0,
                            Month = i.ToString()
                        });
                }
            }
            list.Sort(new AnnouncementDataSortByMonth());
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

        public async Task<ActionResult> CheckEmailExistsForUpdate(string EmailId)
        {
            var email = HttpContext.Session.GetString("UpdateTeacherEmail");
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
        public async Task<ActionResult> CheckUsernameExistsForUpdate(string UserNAME)
        {

            var username = HttpContext.Session.GetString("UpdateTeacherUsername");

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
                createNewTeacher.Email = teacher.EmailId;
                createNewTeacher.Mobile = teacher.Mobile;
                createNewTeacher.Password = teacher.Password;
                createNewTeacher.Username = teacher.UserNAME;
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

                teacherResponseModel = await _teacherRepository.CreateTeacher(_apiBaseUrl.Value.LmsApiBaseUrl,createNewTeacher);


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

                            var page = 1;
                            var size = 5;
                            int recsCount = (await _teacherRepository.GetAllTeacherDetails(_apiBaseUrl.Value.LmsApiBaseUrl)).data.Count();
                            if (page < 1)
                                page = 1;
                            ViewBag.GetTeacherById = TempData["GetTeacherById"] as string;
                            var pager = new Pager(recsCount, page, size);
                            ViewBag.Pager = pager;
                            return View("ManageTeacherUsers", pager);

                          
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
            var teacher = await _teacherRepository.GetTeacherById(_apiBaseUrl.Value.LmsApiBaseUrl,id);
            if (teacher.data == null)
            {
                TempData["GetTeacherById"] = teacher.message;
                return RedirectToAction("ManageStudentUsers", "Teacher");
            }
            HttpContext.Session.SetString("UpdateTeacherEmail", teacher.data.Email);
            HttpContext.Session.SetString("UpdateTeacherUsername", teacher.data.Username);
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
                EmailId = teacher.data.Email,
                IsActive = teacher.data.IsActive,
                Mobile = teacher.data.Mobile,
                SchoolId = teacher.data.School.Id,
                ClassId = teacher.data.Class.Id,
                SectionId = teacher.data.Section.Id,
                SubjectId=teacher.data.Subject.Id,
                UserNAME = teacher.data.Username,
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
                updateTeacher.Email = updateTeacherViewModel.EmailId;
                updateTeacher.Mobile = updateTeacherViewModel.Mobile;
                updateTeacher.Username = updateTeacherViewModel.UserNAME;
                updateTeacher.CityId = updateTeacherViewModel.CityId;
                updateTeacher.StateId = updateTeacherViewModel.StateId;
                updateTeacher.ZipId = updateTeacherViewModel.ZipId;
                updateTeacher.IsActive = updateTeacherViewModel.IsActive;


                UpdateTeacherResponseModel updateTeacherResponseModel = null;
                ViewBag.UpdateTeacherSuccess = null;
                ViewBag.UpdateTeacherError = null;
                ResponseModel responseModel = new ResponseModel();

                updateTeacherResponseModel = await _teacherRepository.UpdateTeacher(_apiBaseUrl.Value.LmsApiBaseUrl,updateTeacher);


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


                            var page = 1;
                            var size = 5;
                            int recsCount = (await _teacherRepository.GetAllTeacherDetails(_apiBaseUrl.Value.LmsApiBaseUrl)).data.Count();
                            if (page < 1)
                                page = 1;
                            ViewBag.GetTeacherById = TempData["GetTeacherById"] as string;
                            var pager = new Pager(recsCount, page, size);
                            ViewBag.Pager = pager;
                            return View("ManageTeacherUsers", pager);


                           
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
                    ViewBag.UpdateTeacherError = updateTeacherResponseModel.message;
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
            int recsCount = (await _teacherRepository.GetAllTeacherDetails(_apiBaseUrl.Value.LmsApiBaseUrl)).data.Count();
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

            var dataList = await _teacherRepository.GetAllTeacherDetails(_apiBaseUrl.Value.LmsApiBaseUrl);
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
            int recsCount = (await _teacherRepository.GetAllTeacherDetails(_apiBaseUrl.Value.LmsApiBaseUrl)).data.Count();
            if (page < 1)
                page = 1;
            var pager = new Pager(recsCount, page, size);

            ViewBag.Pager = pager;
            var data = new List<TeacherDetailsViewModel>();

            var dataList = await _teacherRepository.GetTeacherByPagination(_apiBaseUrl.Value.LmsApiBaseUrl,page, size);

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

            var principal = await _teacherRepository.GetTeacherById(_apiBaseUrl.Value.LmsApiBaseUrl,Id);
            return principal;
        }

        [HttpGet]
        public async Task<IActionResult> DownloadExcel()
        {
            var data = new List<TeacherDetailsViewModel>();

            var dataList = await _teacherRepository.GetAllTeacherDetails(_apiBaseUrl.Value.LmsApiBaseUrl);
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
        public async Task<List<SelectListItem>> GetSubjectName()
        {
            var data = await _subjectRepository.GetAllSubjectDetails(_apiBaseUrl.Value.LmsApiBaseUrl);
            List<SelectListItem> subjects = new List<SelectListItem>();
            foreach (var item in data.data)
            {
                subjects.Add(new SelectListItem
                {
                    Text = item.SubjectName,
                    Value = Convert.ToString(item.Id)
                });
            }
            return subjects;
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
                    Value = Convert.ToString(item.Id)
                });
            }
            return teachers;
        }

        [HttpGet]
        public async Task<IEnumerable<TeacherDetailsViewModel>> GetTeacherByFilters(string SchoolName, string ClassName, string SectionName, string SubjectName, string TeacherName, DateTime CreatedDate, bool IsActive)
        {
            var data = new List<TeacherDetailsViewModel>();
            var dataList = await _teacherRepository.GetTeacherByFilters(_apiBaseUrl.Value.LmsApiBaseUrl,SchoolName, ClassName, SectionName,SubjectName,TeacherName, CreatedDate, IsActive);
            if (dataList.data != null)
            {
                foreach (var teacher in dataList.data)
                {
                    data.Add(new TeacherDetailsViewModel()
                    {
                      
                        Id = teacher.Id,
                        School = teacher.SchoolId.SchoolName,
                        Class = teacher.ClassId.ClassName,
                        Subject = teacher.SubjectId.SubjectName,
                        Section = teacher.SectionId.SectionName,
                        TeacherName = teacher.TeacherName,
                        CreatedDate = (DateTime)teacher.CreatedDate,
                        IsActive = teacher.IsActive
                    });
                }
            }
            return data;
        }

        [HttpGet]
        public async Task<IActionResult> ManageProfile()
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var teacher = await _teacherRepository.GetTeacherByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);
            var teachervm = new ManageProfile()
            {
                ProfileInformation = new ProfileInformation() { Mobile = teacher.data.Mobile, Name = teacher.data.Name, Id = teacher.data.Id, RoleName = Convert.ToString(Request.Cookies["RoleName"] )}
            };
            TempData["CommonId"] = teacher.data.Id;
            HttpContext.Session.SetString("ProfilrInformationId", teacher.data.Id.ToString());
            return View(teachervm);

        }

       
        [HttpGet]
        public async Task<IActionResult> ManageAnnouncement(int page = 1, int size = 20)
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var teacherUserId = await _teacherRepository.GetTeacherByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);

            int recsCount = (await _announcementRepository.GetAllAnnouncementBySchoolList(_apiBaseUrl.Value.LmsApiBaseUrl,teacherUserId.data.schoolid)).data.Count();
            if (page < 1)
                page = 1;
            var pager = new Pager(recsCount, page, size);
            ViewBag.Pager = pager;
            ManageAnnouncementModel model = new ManageAnnouncementModel();
            model.Pager = pager;
            model.Classes = await _common.GetClass();
            model.Sections = await _common.GetSection();
            model.Subjects = await _common.GetSubject();
            var paginationData = await _announcementRepository.GetAnnouncementListBySchoolPagination(_apiBaseUrl.Value.LmsApiBaseUrl,page, size , teacherUserId.data.schoolid);
            List<AnnouncementPagination> pagedData = new List<AnnouncementPagination>();
            foreach (var data in paginationData.data)
            {
                pagedData.Add(new AnnouncementPagination()
                {
                    Id = data.Id,
                   AnnouncementTitle = data.AnnouncementTitle,
                   AnnouncementContent = data.AnnouncementContent,
                    Class = new JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination.ClassDto() { Id = data.Class.Id, ClassName = data.Class.ClassName },
                    Section = new JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination.SectionDto() { Id = data.Section.Id, SectionName = data.Section.SectionName },
                    Subject = new JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination.SubjectDto() { Id = data.Subject.Id, SubjectName = data.Subject.SubjectName },
                    CreatedDate = data.CreatedDate,

                });
            }
            model.AnnouncementPagination = pagedData;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAnnouncement(string AddAnnouncement, ManageAnnouncementModel manageAnnouncementModel)
        {
            ViewBag.AddAnnouncementSuccess = null;
            ViewBag.AddAnnouncementError = null;
            manageAnnouncementModel.Classes = await _common.GetClass();
            manageAnnouncementModel.Sections = await _common.GetSection();
            manageAnnouncementModel.Subjects = await _common.GetSubject();

            var userId = Convert.ToString(Request.Cookies["Id"]);
            var teacher = await _teacherRepository.GetTeacherByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);
            
            CreateAnnouncementDto createAnnouncementDto = new CreateAnnouncementDto();

            if (ModelState.IsValid)
            {
                createAnnouncementDto.TeacherId = teacher.data.Id;
                createAnnouncementDto.SchoolId = teacher.data.schoolid;
                createAnnouncementDto.ClassId = manageAnnouncementModel.AddAnnouncement.ClassId;
                createAnnouncementDto.SectionId = manageAnnouncementModel.AddAnnouncement.SectionId;
                createAnnouncementDto.SubjectId = manageAnnouncementModel.AddAnnouncement.SubjectId;
                createAnnouncementDto.AnnouncementTitle = manageAnnouncementModel.AddAnnouncement.AnnouncementTitle;
                createAnnouncementDto.AnnouncementMadeBy = "Teacher";
                createAnnouncementDto.AnnouncementContent = manageAnnouncementModel.AddAnnouncement.AnnouncementContent;

                createAnnouncementDto.IsActive = true;
                AddAnnouncementResponseModel addAnnouncementResponseModel = null;
                ViewBag.AddAnnouncementSuccess = null;
                ViewBag.AddAnnouncementError = null;
                ResponseModel responseModel = new ResponseModel();

                addAnnouncementResponseModel = await _announcementRepository.AddAnnouncement(_apiBaseUrl.Value.LmsApiBaseUrl,createAnnouncementDto);


                if (addAnnouncementResponseModel.Succeeded)
                {
                    if (addAnnouncementResponseModel == null && addAnnouncementResponseModel.data == null)
                    {
                        responseModel.ResponseMessage = addAnnouncementResponseModel.message;
                        responseModel.IsSuccess = addAnnouncementResponseModel.Succeeded;
                    }
                    if (addAnnouncementResponseModel != null)
                    {
                        if (addAnnouncementResponseModel.data != null)
                        {
                            responseModel.ResponseMessage = addAnnouncementResponseModel.message;
                            responseModel.IsSuccess = addAnnouncementResponseModel.Succeeded;
                            ViewBag.AddAnnouncementSuccess = "Details Added Successfully";
                            ModelState.Clear();
                            ManageAnnouncementModel model = new ManageAnnouncementModel();
                            model.Classes = await _common.GetClass();
                            model.Sections = await _common.GetSection();
                            model.Subjects = await _common.GetSubject();
                            int recsCount = (await _announcementRepository.GetAnnouncementList(_apiBaseUrl.Value.LmsApiBaseUrl)).data.Count();
                            var page = 1;
                            var size = 5;
                            if (page < 1)
                                page = 1;
                            var pager = new Pager(recsCount, page, size);
                            ViewBag.Pager = pager;
                            model.Pager = pager;
                            var paginationData = await _announcementRepository.GetAnnouncementListByPagination(_apiBaseUrl.Value.LmsApiBaseUrl,page, size);
                            List<AnnouncementPagination> pagedData = new List<AnnouncementPagination>();
                            foreach (var data in paginationData.data)
                            {
                                pagedData.Add(new AnnouncementPagination()
                                {
                                    Id = data.Id,
                                    AnnouncementContent = data.AnnouncementContent,
                                    AnnouncementTitle = data.AnnouncementTitle,
                                    AnnouncementMadeBy=data.AnnouncementMadeBy,
                                    Class = data.Class,
                                    Section = data.Section,
                                    Subject = data.Subject,
                                    CreatedDate = data.CreatedDate,

                                });
                            }
                            model.AnnouncementPagination = pagedData;
                            return View("ManageAnnouncement", model);
                        }
                        else
                        {
                            responseModel.ResponseMessage = addAnnouncementResponseModel.message;
                            responseModel.IsSuccess = addAnnouncementResponseModel.Succeeded;


                            ViewBag.AddAnnouncementError = addAnnouncementResponseModel.message;
                            return View(manageAnnouncementModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = addAnnouncementResponseModel.message;
                    responseModel.IsSuccess = addAnnouncementResponseModel.Succeeded;
                    ViewBag.AddCircularError = addAnnouncementResponseModel.message;
                }
            }
            return View(manageAnnouncementModel);
        }

        [HttpGet]
        public async Task<IActionResult> SearchAnnouncement(int ClassId, int SectionId, int SubjectId)
        {
            List<AnnouncementPagination> data = new List<AnnouncementPagination>();
            DateTime date = new DateTime(0001,01,01,0,0,0);
            var dataList = await _announcementRepository.GetAnnouncementByFilters(_apiBaseUrl.Value.LmsApiBaseUrl,0, ClassId,SectionId,SubjectId,"Select Teacher", "Select Type",null,null, date);
            if (dataList.data != null)
            {
                foreach (var d in dataList.data)
                {
                    data.Add(new AnnouncementPagination()
                    {
                        Id = d.Id,
                        AnnouncementTitle = d.AnnouncementTitle,
                        AnnouncementContent = d.AnnouncementContent,
                         Class= new JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination.ClassDto(){ Id = d.Class.Id, ClassName = d.Class.ClassName },
                        Section = new JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination.SectionDto() { Id = d.Section.Id, SectionName = d.Section.SectionName },
                        Subject = new JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination.SubjectDto() { Id = d.Subject.Id, SubjectName = d.Subject.SubjectName },
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
            model.Classes = await _common.GetClass();
            model.Sections = await _common.GetSection();
            model.Subjects = await _common.GetSubject();
            return View("ManageAnnouncement", model);
        }

        [HttpGet]
        public async Task<GetAnnouncementByIdResponseModel> ViewAnnouncement(int Id)
        {
            var announcement = await _announcementRepository.GetAnnouncementById(_apiBaseUrl.Value.LmsApiBaseUrl,Id);
            return announcement;
        }
        public async Task<IActionResult> UpdateAnnouncement(ManageAnnouncementModel manageAnnouncementModel, string UpdateAnnouncement)
        {
            ViewBag.UpdateAnnouncementSuccess = null;
            ViewBag.UpdateAnnouncementError = null;
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var teacher = await _teacherRepository.GetTeacherByUserId(_apiBaseUrl.Value.LmsApiBaseUrl,userId);

            manageAnnouncementModel.Classes = await _common.GetClass();
            manageAnnouncementModel.Sections = await _common.GetSection();
            manageAnnouncementModel.Subjects = await _common.GetSubject();
            UpdateAnnouncementDto updateAnnouncementDto = new UpdateAnnouncementDto();
            if (ModelState.IsValid)
            {
                updateAnnouncementDto.Id = manageAnnouncementModel.UpdateAnnouncement.Id;
                updateAnnouncementDto.TeacherId = teacher.data.Id;
                updateAnnouncementDto.SchoolId = teacher.data.schoolid;
                updateAnnouncementDto.ClassId = manageAnnouncementModel.UpdateAnnouncement.ClassId;
                updateAnnouncementDto.SectionId = manageAnnouncementModel.UpdateAnnouncement.SectionId;
                updateAnnouncementDto.SubjectId = manageAnnouncementModel.UpdateAnnouncement.SubjectId;
                updateAnnouncementDto.AnnouncementTitle = manageAnnouncementModel.UpdateAnnouncement.AnnouncementTitle;
                updateAnnouncementDto.AnnouncementMadeBy = "Teacher";
                updateAnnouncementDto.AnnouncementContent = manageAnnouncementModel.UpdateAnnouncement.AnnouncementContent;
                updateAnnouncementDto.IsActive = true;

                UpdateAnnouncementResponseModel updateAnnouncementResponseModel = null;
                ViewBag.UpdateAnnouncementSuccess = null;
                ViewBag.UpdateAnnouncementError = null;
                ResponseModel responseModel = new ResponseModel();

                updateAnnouncementResponseModel = await _announcementRepository.UpdateAnnouncement(_apiBaseUrl.Value.LmsApiBaseUrl,updateAnnouncementDto);


                if (updateAnnouncementResponseModel.Succeeded)
                {
                    if (updateAnnouncementResponseModel == null && updateAnnouncementResponseModel?.data == null)
                    {
                        responseModel.ResponseMessage = updateAnnouncementResponseModel.message;
                        responseModel.IsSuccess = updateAnnouncementResponseModel.Succeeded;
                    }
                    if (updateAnnouncementResponseModel != null)
                    {
                        if (updateAnnouncementResponseModel?.data != null)
                        {
                            responseModel.ResponseMessage = updateAnnouncementResponseModel.message;
                            responseModel.IsSuccess = updateAnnouncementResponseModel.Succeeded;
                            ViewBag.UpdateAnnouncementSuccess = "Details Updated Successfully";

                            ModelState.Clear();
                            ManageAnnouncementModel model = new ManageAnnouncementModel();
                            model.Classes = await _common.GetClass();
                            model.Sections = await _common.GetSection();
                            model.Subjects = await _common.GetSubject();
                            int recsCount = (await _announcementRepository.GetAnnouncementList(_apiBaseUrl.Value.LmsApiBaseUrl)).data.Count();
                            var page = 1;
                            var size = 5;
                            if (page < 1)
                                page = 1;
                            var pager = new Pager(recsCount, page, size);
                            ViewBag.Pager = pager;
                            model.Pager = pager;
                            var paginationData = await _announcementRepository.GetAnnouncementListByPagination(_apiBaseUrl.Value.LmsApiBaseUrl,page, size);
                            List<AnnouncementPagination> pagedData = new List<AnnouncementPagination>();
                            foreach (var data in paginationData.data)
                            {
                                pagedData.Add(new AnnouncementPagination()
                                {
                                    Id = data.Id,
                                    AnnouncementContent = data.AnnouncementContent,
                                    AnnouncementTitle = data.AnnouncementTitle,
                                    AnnouncementMadeBy = data.AnnouncementMadeBy,
                                    Class = data.Class,
                                    Section = data.Section,
                                    Subject = data.Subject,
                                    CreatedDate = data.CreatedDate,

                                });
                            }
                            model.AnnouncementPagination = pagedData;
                            return View("ManageAnnouncement", model);
                        }
                        else
                        {
                            responseModel.ResponseMessage = updateAnnouncementResponseModel.message;
                            responseModel.IsSuccess = updateAnnouncementResponseModel.Succeeded;
                            ViewBag.UpdateAnnouncementError = updateAnnouncementResponseModel.message;
                            return View(manageAnnouncementModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = updateAnnouncementResponseModel.message;
                    responseModel.IsSuccess = updateAnnouncementResponseModel.Succeeded;
                    ViewBag.UpdateAnnouncementError = updateAnnouncementResponseModel.message;
                }
            }
            return View(manageAnnouncementModel);
        }

        public async Task<IActionResult> ManageFeedback()
        {
            var data = new List<GetAllFeedbackDetails>();
            FeedbackModel model = new FeedbackModel();
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var teacher = await _teacherRepository.GetTeacherByUserId(_apiBaseUrl.Value.LmsApiBaseUrl, userId);

            model.Classes = await _common.GetClass();
            model.Subjects = await _common.GetSubject();
            model.Sections = await _common.GetSection();
            model.Students = await _common.GetAllsStudent();
            model.Parents = await _common.GetAllParents();



            var dataList = await _feedbackRepository.GetAllFeedbackDetails(_apiBaseUrl.Value.LmsApiBaseUrl);
            var tempstatus = "";
            foreach (var feedbackdata in dataList.data)
            {
                if (feedbackdata.SchoolId == teacher.data.schoolid)
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
        public async Task<IActionResult> DeleteGallary(int id)
        {
            await _gallaryRepository.DeleteGallary(_apiBaseUrl.Value.LmsApiBaseUrl, id);
            ViewBag.DeleteGallarySuccess = "Images Deleted Successfully";
            return RedirectToAction("ListGallary");
        }
        [HttpGet]
        public async Task<GetGallaryListByIdResponseModel> ViewGallary(int Id)
        {

            var gallary = await _gallaryRepository.GetGallaryById(_apiBaseUrl.Value.LmsApiBaseUrl, Id);
            return gallary;
        }



        [HttpGet]
        public async Task<IActionResult> ListGallary()
        {
            var data = new List<GetGallaryList>();
            GallaryDetailsModel model = new GallaryDetailsModel();

            model.Events = await _common.GetEvent();
            model.Schools = await _common.GetSchool();
            var dataList = await _gallaryRepository.GetGallaryList(_apiBaseUrl.Value.LmsApiBaseUrl);

            foreach (var gallarydata in dataList.data)
            {

                data.Add(new GetGallaryList()
                {
                    Id = gallarydata.Id,
                    EventsTableId = gallarydata.EventsTableId,
                    EventTitle = gallarydata.EventsData.EventTitle,
                    FileName = gallarydata.FileName,
                    FileType = gallarydata.FileType,
                    image = gallarydata.image,


                });
            }
            model.GetGallaryList = data;
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> ManageGallary()
        {
            var data = new List<GetGallaryList>();
            GallaryDetailsModel model = new GallaryDetailsModel();

            model.Events = await _common.GetEvent();
            model.Schools = await _common.GetSchool();
            var dataList = await _gallaryRepository.GetGallaryList(_apiBaseUrl.Value.LmsApiBaseUrl);

            foreach (var gallarydata in dataList.data)
            {

                data.Add(new GetGallaryList()
                {
                    Id = gallarydata.Id,
                    EventsTableId = gallarydata.EventsTableId,
                    EventTitle = gallarydata.EventsData.EventTitle,
                    FileName = gallarydata.FileName,
                    FileType = gallarydata.FileType,

                    image = gallarydata.image,


                });
            }
            model.GetGallaryList = data;
            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> DeleteGallaryList(int id)
        {
            await _gallaryRepository.DeleteGallary(_apiBaseUrl.Value.LmsApiBaseUrl, id);
            ViewBag.DeleteGallarySuccess = "Images Deleted Successfully";
            return RedirectToAction("ManageGallary");
        }

        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename is not availble");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Gallary", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        // Get content type
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        // Get mime types
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
    {
        {".txt", "text/plain"},
        {".pdf", "application/pdf"},
        {".doc", "application/vnd.ms-word"},
        {".docx", "application/vnd.ms-word"},
        {".ppt","application/vnd.ms-powerpoint"},
        {".xls", "application/vnd.ms-excel"},
        {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
        {".png", "image/png"},
        {".jpg", "image/jpeg"},
        {".jpeg", "image/jpeg"},
        {".gif", "image/gif"},
        {".jfif","image/jpeg" },
        {".csv", "text/csv"}
    };
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAllGallary()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Gallary");
            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            await _gallaryRepository.DeleteAllGallary(_apiBaseUrl.Value.LmsApiBaseUrl);
            ViewBag.DeleteAllGallarySuccess = "All Images Deleted Successfully";
            return RedirectToAction("ManageGallary");
        }



        [HttpGet]
        public async Task<IActionResult> DeleteAllGallaryList()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Gallary");
            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            await _gallaryRepository.DeleteAllGallary(_apiBaseUrl.Value.LmsApiBaseUrl);
            ViewBag.DeleteAllGallarySuccess = "All Images Deleted Successfully";
            return RedirectToAction("ListGallary");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadImage(GallaryDetailsModel gallaryDetailsModel)
        {
            ViewBag.AddGallarySuccess = null;
            ViewBag.AddGallaryError = null;
            gallaryDetailsModel.Events = await _common.GetEvent();
            gallaryDetailsModel.Schools = await _common.GetSchool();
            UploadImageDto uploadImageDto = new UploadImageDto();

            if (ModelState.IsValid)
            {

                var ImagePath = _configuration["Gallary"];
                string imagename = null;

                if (gallaryDetailsModel.AddGallary.imageUpload != null)
                {
                    imagename = _common.ProcessUploadFile(gallaryDetailsModel.AddGallary.imageUpload, ImagePath);
                }


                uploadImageDto.EventsTableId = gallaryDetailsModel.AddGallary.EventsTableId;
                uploadImageDto.FileName = gallaryDetailsModel.AddGallary.FileName;
                uploadImageDto.FileType = gallaryDetailsModel.AddGallary.FileType;
                uploadImageDto.IsActive = true;
                uploadImageDto.image = imagename;
                uploadImageDto.SchoolId = gallaryDetailsModel.AddGallary.SchoolId;

                //if (Image != null)
                //{
                //    imagename = _common.ProcessUploadFile(Image, ImagePath);
                //}
                //else
                //{
                //    imagename = ImageName;
                //}

                AddGallaryResponseModel addGallaryResponseModel = null;
                ViewBag.AddGallarySuccess = null;
                ViewBag.AddGallaryError = null;
                ResponseModel responseModel = new ResponseModel();
                addGallaryResponseModel = await _gallaryRepository.AddGallary(_apiBaseUrl.Value.LmsApiBaseUrl, uploadImageDto);


                if (addGallaryResponseModel.Succeeded)
                {
                    if (addGallaryResponseModel == null && addGallaryResponseModel.data == null)
                    {
                        responseModel.ResponseMessage = addGallaryResponseModel.message;
                        responseModel.IsSuccess = addGallaryResponseModel.Succeeded;
                    }
                    if (addGallaryResponseModel != null)
                    {
                        if (addGallaryResponseModel.data != null)
                        {
                            responseModel.ResponseMessage = addGallaryResponseModel.message;
                            responseModel.IsSuccess = addGallaryResponseModel.Succeeded;
                            ViewBag.AddGallarySuccess = "File Added Successfully";
                            //ModelState.Clear();

                            GallaryDetailsModel model = new GallaryDetailsModel();

                            var data = new List<GetGallaryList>();

                            var dataList = await _gallaryRepository.GetGallaryList(_apiBaseUrl.Value.LmsApiBaseUrl);

                            foreach (var gallarydata in dataList.data)
                            {

                                data.Add(new GetGallaryList()
                                {

                                    image = gallarydata.image,


                                });
                            }
                            model.GetGallaryList = data;
                            return View("ManageGallary", model);
                        }
                        else
                        {
                            responseModel.ResponseMessage = addGallaryResponseModel.message;
                            responseModel.IsSuccess = addGallaryResponseModel.Succeeded;


                            ViewBag.AddGallaryError = addGallaryResponseModel.message;
                            return View(gallaryDetailsModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = addGallaryResponseModel.message;
                    responseModel.IsSuccess = addGallaryResponseModel.Succeeded;
                    ViewBag.AddGallaryError = addGallaryResponseModel.message;
                }
            }
            return View(gallaryDetailsModel);
        }

    }
}
