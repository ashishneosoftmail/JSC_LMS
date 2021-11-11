
using JSC_LMS.Application.Features.Subject.Commands.CreateSubject;
using JSC_LMS.Application.Features.Subject.Commands.UpdateSubject;
using JSC_LSM.UI.Models;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#region- Controller for Subject module:by Vishram Sawant
namespace JSC_LSM.UI.Controllers
{
    public class SubjectController : BaseController
    {
        // }
        private readonly JSC_LSM.UI.Common.Common _common;
        private readonly ISchoolRepository _schoolRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IClassRepository _classRepository;
        public SubjectController(JSC_LSM.UI.Common.Common common, IClassRepository classRepository, ISchoolRepository schoolRepository, ISectionRepository sectionRepository, ISubjectRepository subjectRepository)
        {
            _common = common;
            _schoolRepository = schoolRepository;
            _sectionRepository = sectionRepository;
            _classRepository = classRepository;
            _subjectRepository = subjectRepository;
        }

        /// <summary>
        /// Gives all the details in form of a list:by Vishram Sawant
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> ManageSubject()
        {
            var page = 1;
            var size = 5;
            int recsCount = (await _subjectRepository.GetAllSubjectDetails()).data.Count();
            if (page < 1)
                page = 1;

            var pager = new Pager(recsCount, page, size);
            this.ViewBag.Pager = pager;
            return View(pager);



        }

        /// <summary>
        ///  Get method To Add a new Subject :by Vishram Sawant
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> AddSubject()
        {
            SubjectModel subjectModel = new SubjectModel();

            subjectModel.Schools = await _common.GetSchool();
            subjectModel.Classes = await _common.GetClass();
            subjectModel.Sections = await _common.GetSection();
            return View(subjectModel);
        }

        /// <summary>
        /// Post method To Add a new Subject :by Vishram Sawant
        /// </summary>
        /// <param name="subjectModel"></param>
        /// <returns></returns>

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSubject(SubjectModel subjectModel)



        {
            ViewBag.AddSubjectSuccess = null;
            ViewBag.AddSubjectError = null;

            subjectModel.Schools = await _common.GetSchool();
            subjectModel.Classes = await _common.GetClass();
            subjectModel.Sections = await _common.GetSection();

            CreateSubjectDto createNewSubject = new CreateSubjectDto();

            if (ModelState.IsValid)
            {

                createNewSubject.SchoolId = subjectModel.SchoolId;
                createNewSubject.ClassId = subjectModel.ClassId;
                createNewSubject.SectionId = subjectModel.SectionId;
                createNewSubject.SubjectName = subjectModel.SubjectName;
                createNewSubject.IsActive = subjectModel.IsActive;

                SubjectResponseModel subjectResponseModel = null;
                ViewBag.AddSubjectSuccess = null;
                ViewBag.AddSubjectError = null;
                ResponseModel responseModel = new ResponseModel();

                subjectResponseModel = await _subjectRepository.AddNewSubject(createNewSubject);


                if (subjectResponseModel.Succeeded)
                {
                    if (subjectResponseModel == null && subjectResponseModel.data == null)
                    {
                        responseModel.ResponseMessage = subjectResponseModel.message;
                        responseModel.IsSuccess = subjectResponseModel.Succeeded;
                    }
                    if (subjectResponseModel != null)
                    {
                        if (subjectResponseModel.data != null)
                        {
                            responseModel.ResponseMessage = subjectResponseModel.message;
                            responseModel.IsSuccess = subjectResponseModel.Succeeded;
                            ViewBag.AddSubjectSuccess = "Details Added Successfully";
                            ModelState.Clear();
                            var newSubjectModel = new SubjectModel();

                            newSubjectModel.Schools = await _common.GetSchool();
                            newSubjectModel.Classes = await _common.GetClass();
                            newSubjectModel.Sections = await _common.GetSection();
                            return View(newSubjectModel);
                        }
                        else
                        {
                            responseModel.ResponseMessage = subjectResponseModel.message;
                            responseModel.IsSuccess = subjectResponseModel.Succeeded;
                            ViewBag.AddSubjectError = subjectResponseModel.message;
                            return View(subjectModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = subjectResponseModel.message;
                    responseModel.IsSuccess = subjectResponseModel.Succeeded;
                    ViewBag.AddSubjectError = subjectResponseModel.message;
                }
            }
            return View(subjectModel);

        }

        /// <summary>
        /// Gives Subject details using Id :by Vishram Sawant
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        [HttpGet]
        public async Task<GetSubjectByIdResponseModel> GetSubjectById(int Id)
        {

            var subjects = await _subjectRepository.GetSubjectById(Id);
            return subjects;
        }


        /// <summary>
        ///  Gives all the Subject details:by Vishram Sawant
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<SubjectDetailsViewModel>> GetAllSubjectDetails()
        {
            var data = new List<SubjectDetailsViewModel>();

            var dataList = await _subjectRepository.GetAllSubjectDetails();
            foreach (var subjects in dataList.data)
            {
                data.Add(new SubjectDetailsViewModel()
                {
                    Id = subjects.Id,
                    SubjectName = subjects.SubjectName,


                    CreatedDate = subjects.CreatedDate,

                    IsActive = subjects.IsActive,

                    SchoolName = subjects.School.SchoolName,

                    ClassName = subjects.Class.ClassName,

                    SectionName = subjects.Section.SectionName,

                });
            }
            return data;
        }


        /// <summary>
        /// Custom Pagination for Subject module:by Vishram Sawant
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>

        [HttpGet]
        public async Task<IEnumerable<SubjectDetailsViewModel>> GetAllSubjectsDetailsByPagination(int page = 1, int size = 5)
        {
            int recsCount = (await _subjectRepository.GetAllSubjectDetails()).data.Count();
            if (page < 1)
                page = 1;
            var pager = new Pager(recsCount, page, size);

            this.ViewBag.Pager = pager;
            var data = new List<SubjectDetailsViewModel>();

            //int recSkip = (page - 1) * size;
            var dataList = await _subjectRepository.GetSubjectByPagination(page, size);

            foreach (var subjects in dataList.data.GetSubjectListPaginationDto)
            {
                data.Add(new SubjectDetailsViewModel()
                {
                    Id = subjects.Id,
                    SubjectName = subjects.SubjectName,


                    CreatedDate = subjects.CreatedDate,

                    IsActive = subjects.IsActive,

                    SchoolName = subjects.School.SchoolName,

                    ClassName = subjects.Class.ClassName,

                    SectionName = subjects.Section.SectionName,

                });
            }
            return data;
        }

        /// <summary>
        ///  get School list  :by Vishram Sawant
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<List<SelectListItem>> GetAllSchool()
        {
            var schools = await _common.GetSchool();
            return schools;
        }

        /// <summary>
        ///  get Class list  :by Vishram Sawant
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<List<SelectListItem>> GetAllClass()
        {
            var classes = await _common.GetClass();
            return classes;
        }

        /// <summary>
        ///  get Section list  :by Vishram Sawant
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<List<SelectListItem>> GetAllSection()
        {
            var sections = await _common.GetSection();
            return sections;
        }

        /// <summary>
        ///  get Subject Name  :by Vishram Sawant
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<SelectListItem>> GetSubjectName()
        {
            var data = await _subjectRepository.GetAllSubjectDetails();
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

        /// <summary>
        /// Updates the Subject details of the given Id:by Vishram Sawant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> EditSubject(int id)
        {
            var subjects = await _subjectRepository.GetSubjectById(id);
            if (subjects.data == null)
            {
                TempData["GetSubjectById"] = subjects.message;
                return RedirectToAction("ManageSubject", "Subject");
            }
            var subjectData = new UpdateSubjectViewModel()
            {
                Id = subjects.data.Id,
                SubjectName = subjects.data.SubjectName,


                IsActive = subjects.data.IsActive,

                SchoolId = subjects.data.School.Id,

                ClassId = subjects.data.Class.Id,

                SectionId = subjects.data.Section.Id,


            };
            subjectData.Schools = await _common.GetSchool();
            subjectData.Classes = await _common.GetClass();
            subjectData.Sections = await _common.GetSection();

            return View(subjectData);
        }

        /// <summary>
        /// Post method to update the Subject details :by Vishram Sawant
        /// </summary>
        /// <param name="updateSubjectViewModel"></param>
        /// <returns></returns>

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSubject(UpdateSubjectViewModel updateSubjectViewModel)
        {
            ViewBag.UpdateSubjectSuccess = null;
            ViewBag.UpdateSubjectError = null;
            updateSubjectViewModel.Schools = await _common.GetSchool();
            updateSubjectViewModel.Classes = await _common.GetClass();
            updateSubjectViewModel.Sections = await _common.GetSection();

            UpdateSubjectDto updateSubject = new UpdateSubjectDto();
            updateSubject.Id = updateSubjectViewModel.Id;
            if (ModelState.IsValid)
            {

                updateSubject.SchoolId = updateSubjectViewModel.SchoolId;

                updateSubject.ClassId = updateSubjectViewModel.ClassId;

                updateSubject.SectionId = updateSubjectViewModel.SectionId;

                updateSubject.SubjectName = updateSubjectViewModel.SubjectName;


                updateSubject.IsActive = updateSubjectViewModel.IsActive;


                UpdateSubjectResponseModel updateSubjectResponseModel = null;
                ViewBag.UpdateSubjectSuccess = null;
                ViewBag.UpdateSubjectError = null;
                ResponseModel responseModel = new ResponseModel();

                updateSubjectResponseModel = await _subjectRepository.UpdateSubject(updateSubject);


                if (updateSubjectResponseModel.Succeeded)
                {
                    if (updateSubjectResponseModel == null && updateSubjectResponseModel?.data == null)
                    {
                        responseModel.ResponseMessage = updateSubjectResponseModel.message;
                        responseModel.IsSuccess = updateSubjectResponseModel.Succeeded;
                    }
                    if (updateSubjectResponseModel != null)
                    {
                        if (updateSubjectResponseModel?.data != null)
                        {
                            responseModel.ResponseMessage = updateSubjectResponseModel.message;
                            responseModel.IsSuccess = updateSubjectResponseModel.Succeeded;
                            ViewBag.UpdateSubjectSuccess = "Details Updated Successfully";

                            return RedirectToAction("ManageSubject", "Subject");
                        }
                        else
                        {
                            responseModel.ResponseMessage = updateSubjectResponseModel.message;
                            responseModel.IsSuccess = updateSubjectResponseModel.Succeeded;
                            ViewBag.UpdateSubjectError = updateSubjectResponseModel.message;
                            return View(updateSubjectResponseModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = updateSubjectResponseModel.message;
                    responseModel.IsSuccess = updateSubjectResponseModel.Succeeded;
                    ViewBag.UpdateSubjectError = updateSubjectResponseModel.message;
                }
            }
            return View(updateSubjectViewModel);
        }

        /// <summary>
        /// Get Method for Subject By Filters:by Vishram Sawant
        /// </summary>
        /// <param name="className"></param>
        /// <param name="schoolName"></param>
        /// <param name="sectionName"></param>
        /// <param name="subjectName"></param>
        /// <param name="createdDate"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>

        [HttpGet]
        public async Task<IEnumerable<SubjectDetailsViewModel>> GetSubjectByFilters(string className, string schoolName, string sectionName, string subjectName, DateTime createdDate, bool isActive)
        {
            var data = new List<SubjectDetailsViewModel>();
            var dataList = await _subjectRepository.GetSubjectByFilters(schoolName, className, sectionName, subjectName, createdDate, isActive);
            if (dataList.data != null)
            {
                foreach (var subjects in dataList.data)
                {
                    data.Add(new SubjectDetailsViewModel()
                    {
                        Id = subjects.Id,

                        SubjectName = subjects.SubjectName,

                        CreatedDate = subjects.CreatedDate,

                        IsActive = subjects.IsActive,

                        SchoolName = subjects.School.SchoolName,

                        ClassName = subjects.Class.ClassName,

                        SectionName = subjects.Section.SectionName,

                    });
                }
            }
            return data;
        }



    }
}
#endregion