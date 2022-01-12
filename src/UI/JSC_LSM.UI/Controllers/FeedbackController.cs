using AutoMapper.Configuration;
using JSC_LMS.Application.Features.Feedback.Commands.CreateFeedback;
using JSC_LSM.UI.Helpers;
using JSC_LSM.UI.Models;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Controllers
{
    public class FeedbackController : BaseController
    {


        private readonly JSC_LSM.UI.Common.Common _common;
        private readonly IFeedbackRepository _feedbackRepository;
        //private readonly IFeedbackTitleRepository _feedbackTitleRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IParentsRepository _parentsRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;


        public FeedbackController(JSC_LSM.UI.Common.Common common, IFeedbackRepository feedbackRepository, IStudentRepository studentRepository, IClassRepository classRepository, ISectionRepository sectionRepository, ISubjectRepository subjectRepository, IParentsRepository parentsRepository,  IWebHostEnvironment webHostEnvironment , IOptions<ApiBaseUrl> apiBaseUrl)
        {
            _common = common;
            _feedbackRepository = feedbackRepository;
            _studentRepository = studentRepository;
            _classRepository = classRepository;
            _sectionRepository = sectionRepository;
            _subjectRepository = subjectRepository;
            _parentsRepository = parentsRepository;
            _webHostEnvironment = webHostEnvironment;
            _apiBaseUrl = apiBaseUrl;
        }



        public async Task<IActionResult> ManageFeedback()
        {
            var data = new List<GetAllFeedbackDetails>();
            FeedbackModel model = new FeedbackModel();
            
            model.Classes = await _common.GetClass();
            model.Subjects = await _common.GetSubject();
            model.Sections = await _common.GetSection();
            model.Students = await _common.GetAllsStudent();
            model.Parents = await _common.GetAllParents();
           
           

            var dataList = await _feedbackRepository.GetAllFeedbackDetails(_apiBaseUrl.Value.LmsApiBaseUrl);
            var tempstatus = "";
            foreach (var feedbackdata in dataList.data)
            {
                
                data.Add(new GetAllFeedbackDetails()
                {

                   Id = feedbackdata.Id,
                   feedbackTitle = feedbackdata.feedbackTitle.Feedback_Title,
                   ClassName = feedbackdata.Classes.ClassName,
                   SchoolName=feedbackdata.School.SchoolName,
                   SectionName = feedbackdata.Section.SectionName,
                   SubjectName = feedbackdata.Subject.SubjectName,
                    StudentName = feedbackdata.Section.SectionName,
                    ParentName = feedbackdata.Subject.SubjectName,
                    FeedbackType = feedbackdata.FeedbackType,
                   SendDate = feedbackdata.SendDate,
                  
                   

                });
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
           
            CreateFeedbackDto createNewFeedback = new CreateFeedbackDto();

            if (ModelState.IsValid)
            {

                createNewFeedback.SubjectId = feedbackModel.SubjectId;
                createNewFeedback.SectionId = feedbackModel.SubjectId;
                createNewFeedback.ClassId = feedbackModel.SubjectId;
                createNewFeedback.StudentId = feedbackModel.StudentId;
                createNewFeedback.ParentId = feedbackModel.ParentId;
                //createNewFeedback.FeedbackTitleId = feedbackModel.FeedbackTitleId;
                createNewFeedback.FeedbackType = feedbackModel.FeedbackType;
                createNewFeedback.IsActive = feedbackModel.IsActive;
                createNewFeedback.FeedbackComments = feedbackModel.FeedbackComments;
                FeedbackResponseModel feedbackResponseModel = null;
                ViewBag.AddFeedbackSuccess = null;
                ViewBag.AddFeedbackError = null;
                ResponseModel responseModel = new ResponseModel();

                feedbackResponseModel = await _feedbackRepository.AddNewFeedback(_apiBaseUrl.Value.LmsApiBaseUrl,createNewFeedback);


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
                            

                            var page = 1;
                            var size = 5;
                            int recsCount = (await _feedbackRepository.GetAllFeedbackDetails(_apiBaseUrl.Value.LmsApiBaseUrl)).data.Count();
                            if (page < 1)
                                page = 1;
                            ViewBag.GetFeedbackById = TempData["GetFeedbackById"] as string;
                            var pager = new Pager(recsCount, page, size);
                            ViewBag.Pager = pager;
                            return View("ManageFeedback", pager);



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

        [HttpGet]
        public async Task<GetFeedbackByIdResponseModel> GetFeedbackById(int Id)
        {

            var subjects = await _feedbackRepository.GetFeedbackById(_apiBaseUrl.Value.LmsApiBaseUrl,Id);
            return subjects;
        }


        [HttpGet]
        public async Task<IEnumerable<FeedbackDetailsViewModel>> GetAllFeedbackDetails()
        {
            var data = new List<FeedbackDetailsViewModel>();

            var dataList = await _feedbackRepository.GetAllFeedbackDetails(_apiBaseUrl.Value.LmsApiBaseUrl);
            foreach (var feedbacks in dataList.data)
            {
                data.Add(new FeedbackDetailsViewModel()
                {
                    Id = feedbacks.Id,
                    feedbackTitle = feedbacks.feedbackTitle.Feedback_Title,


                    SchoolName=feedbacks.School.SchoolName,
                    StudentName = feedbacks.Students.StudentName,
                    ClassName = feedbacks.Classes.ClassName,
                    SectionName = feedbacks.Section.SectionName,
                   SubjectName = feedbacks.Subject.SubjectName,
                   ParentName = feedbacks.Parents.ParentName,

                    Type = feedbacks.FeedbackType,
                    SendDate = feedbacks.SendDate,

                    IsActive = feedbacks.IsActive





                });
            }
            return data;
        }


        //[HttpGet]
        //public async Task<List<SelectListItem>> GetAllStudent()
        //{
        //    var student = await _common.GetStudent();
        //    return student;
        //}

  

        [HttpGet]
        public async Task<List<SelectListItem>> GetAllClass()
        {
            var classes = await _common.GetClass();
            return classes;
        }


        //[HttpGet]
        //public async Task<List<SelectListItem>> GetAllParent()
        //{
        //    var parent = await _common.GetParent();
        //    return parent;
        //}


        [HttpGet]
        public async Task<List<SelectListItem>> GetAllSubject()
        {
            var subject = await _common.GetSubject();
            return subject;
        }



        [HttpGet]
        public async Task<List<SelectListItem>> GetAllSection()
        {
            var classes = await _common.GetClass();
            return classes;
        }


        [HttpGet]
        public async Task<List<SelectListItem>> GetFeedbackType()
        {
            var data = await _feedbackRepository.GetAllFeedbackDetails(_apiBaseUrl.Value.LmsApiBaseUrl);
            List<SelectListItem> feedbacks = new List<SelectListItem>();
            foreach (var item in data.data)
            {
                feedbacks.Add(new SelectListItem
                {
                    Text = item.feedbackTitle.Feedback_Title,
                    Value = Convert.ToString(item.Id)
                });
            }
            return feedbacks;
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public async Task<IActionResult> SendFeedbackData(string SendFeedbackData, FeedbackModel feedbackModel)
        //{
        //    ViewBag.AddFeedbackSuccess = null;
        //    ViewBag.AddFeedbackError = null;
        //    feedbackModel.Subjects = await _common.GetSubject();
        //    //feedbackModel.FeedbackTitles = await _common.GetFeedback();
        //    CreateFeedbackDto createFeedbackDto = new CreateFeedbackDto();

        //    if (ModelState.IsValid)
        //    {

        //        createFeedbackDto.SubjectId = feedbackModel.AddNewFeedbacks.SubjectId;
        //        //createFeedbackDto.FeedbackTitleId = feedbackModel.AddNewFeedbacks.FeedbackTitleId;
        //        createFeedbackDto.FeedbackComments = feedbackModel.AddNewFeedbacks.Comments;

        //        //switch (SendFeedbackData)
        //        //{

        //        //    case "Send":
        //        //        createFeedbackDto.Status = true;
        //        //        break;
        //        //    default:
        //        //        createFeedbackDto.Status = false;
        //        //        break;
        //        //}

        //        createFeedbackDto.IsActive = true;
        //        AddFeedbackResponseModel addFeedbackResponseModel = null;
        //        ViewBag.AddFeedbackSuccess = null;
        //        ViewBag.AddFeedbackError = null;
        //        ResponseModel responseModel = new ResponseModel();

        //        addFeedbackResponseModel = await _feedbackRepository.AddFeedbackData(_apiBaseUrl.Value.LmsApiBaseUrl,createFeedbackDto);


        //        if (addFeedbackResponseModel.Succeeded)
        //        {
        //            if (addFeedbackResponseModel == null && addFeedbackResponseModel.data == null)
        //            {
        //                responseModel.ResponseMessage = addFeedbackResponseModel.message;
        //                responseModel.IsSuccess = addFeedbackResponseModel.Succeeded;
        //            }
        //            if (addFeedbackResponseModel != null)
        //            {
        //                if (addFeedbackResponseModel.data != null)
        //                {
        //                    responseModel.ResponseMessage = addFeedbackResponseModel.message;
        //                    responseModel.IsSuccess = addFeedbackResponseModel.Succeeded;
        //                    ViewBag.AddFeedbackSuccess = "Details Added Successfully";
        //                    ModelState.Clear();
        //                    FeedbackModel model = new FeedbackModel();

        //                    var data = new List<GetAllFeedbackDetails>();

        //                    var dataList = await _feedbackRepository.GetAllFeedbackDetails();
        //                    var tempstatus = "";
        //                    foreach (var feedbackdata in dataList.data)
        //                    {                               
        //                        data.Add(new GetAllFeedbackDetails()
        //                        {                                   
        //                            SubjectName = feedbackdata.Subject.SubjectName,
        //                            //feedbackTitle = feedbackdata.feedbackTitle.Feedback_Title,
        //                            FeedbackComments = feedbackdata.FeedbackComments,

        //                        });
        //                    }
        //                    model.GetFeedbackList = data;
        //                    return View("ManageFeedback", model);
        //                }
        //                else
        //                {
        //                    responseModel.ResponseMessage = addFeedbackResponseModel.message;
        //                    responseModel.IsSuccess = addFeedbackResponseModel.Succeeded;
        //                    ViewBag.AddCircularError = addFeedbackResponseModel.message;
        //                    return View(feedbackModel);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            responseModel.ResponseMessage = addFeedbackResponseModel.message;
        //            responseModel.IsSuccess = addFeedbackResponseModel.Succeeded;
        //            ViewBag.AddCircularError = addFeedbackResponseModel.message;
        //        }
        //    }
        //    return View(feedbackModel);
        //}

        [HttpGet]
        public async Task<GetFeedbackByIdResponseModel> ViewFeedbackData(int Id)
        {

            var feedbacksdata = await _feedbackRepository.GetFeedbackById(_apiBaseUrl.Value.LmsApiBaseUrl,Id);
            return feedbacksdata;
        }

    }
}
