using JSC_LMS.Application.Features.Section.Commands.CreateSection;
using JSC_LMS.Application.Features.Section.Commands.CreateUpdate;
using JSC_LSM.UI.Helpers;
using JSC_LSM.UI.Models;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#region- Controller for Section module:by Vishram Sawant
namespace JSC_LSM.UI.Controllers
{
    public class SectionController : BaseController
    {
      
        private readonly JSC_LSM.UI.Common.Common _common;
        private readonly ISchoolRepository _schoolRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IClassRepository _classRepository;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        public SectionController(JSC_LSM.UI.Common.Common common, IClassRepository classRepository, ISchoolRepository schoolRepository, ISectionRepository sectionRepository , IOptions<ApiBaseUrl> apiBaseUrl)
        {
            _common = common;
            _schoolRepository = schoolRepository;
            _sectionRepository = sectionRepository;
            _classRepository = classRepository;
            _apiBaseUrl = apiBaseUrl;
        }

        /// <summary>
        /// Gives all the details in form of a list:by Vishram Sawant
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ManageSection()
        {
            var page = 1;
            var size = 5;
            int recsCount = (await _sectionRepository.GetAllSectionDetails(_apiBaseUrl.Value.LmsApiBaseUrl)).data.Count();
            if (page < 1)
                page = 1;
            ViewBag.GetSectionById = TempData["GetSectionById"] as string;
            var pager = new Pager(recsCount, page, size);
            ViewBag.Pager = pager;
            return View(pager);



        }



        /// <summary>
        /// Get method To Add a new Section :by Vishram Sawant
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> AddSection()
        {
            SectionModel sectionModel = new SectionModel();

            sectionModel.Schools = await _common.GetSchool();
            sectionModel.Classes = await _common.GetClass();
            return View(sectionModel);
        }


        /// <summary>
        /// Post method To Add a new Section :by Vishram Sawant
        /// </summary>
        /// <param name="sectionModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSection(SectionModel sectionModel)



        {
            ViewBag.AddSectionSuccess = null;
            ViewBag.AddSectionError = null;

            sectionModel.Schools = await _common.GetSchool();
            sectionModel.Classes = await _common.GetClass();

            CreateSectionDto createNewSection = new CreateSectionDto();

            if (ModelState.IsValid)
            {

                createNewSection.SchoolId = sectionModel.SchoolId;
                createNewSection.ClassId = sectionModel.ClassId;
                createNewSection.SectionName = sectionModel.SectionName;
                createNewSection.IsActive = sectionModel.IsActive;

                SectionResponseModel sectionResponseModel = null;
                ViewBag.AddSectionSuccess = null;
                ViewBag.AddSectionError = null;
                ResponseModel responseModel = new ResponseModel();

                sectionResponseModel = await _sectionRepository.AddNewSection(_apiBaseUrl.Value.LmsApiBaseUrl,createNewSection);


                if (sectionResponseModel.Succeeded)
                {
                    if (sectionResponseModel == null && sectionResponseModel.data == null)
                    {
                        responseModel.ResponseMessage = sectionResponseModel.message;
                        responseModel.IsSuccess = sectionResponseModel.Succeeded;
                    }
                    if (sectionResponseModel != null)
                    {
                        if (sectionResponseModel.data != null)
                        {
                            responseModel.ResponseMessage = sectionResponseModel.message;
                            responseModel.IsSuccess = sectionResponseModel.Succeeded;
                            ViewBag.AddSectionSuccess = "Details Added Successfully";
                            ModelState.Clear();
                            var newSectionModel = new SectionModel();


                            newSectionModel.Schools = await _common.GetSchool();
                            newSectionModel.Classes = await _common.GetClass();

                            var page = 1;
                            var size = 5;
                            int recsCount = (await _sectionRepository.GetAllSectionDetails(_apiBaseUrl.Value.LmsApiBaseUrl)).data.Count();
                            if (page < 1)
                                page = 1;
                            ViewBag.GetSectionById = TempData["GetSectionById"] as string;
                            var pager = new Pager(recsCount, page, size);
                            ViewBag.Pager = pager;
                            return View("ManageSection", pager);


                           
                        }
                        else
                        {
                            responseModel.ResponseMessage = sectionResponseModel.message;
                            responseModel.IsSuccess = sectionResponseModel.Succeeded;
                            ViewBag.AddClassError = sectionResponseModel.message;
                            return View(sectionModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = sectionResponseModel.message;
                    responseModel.IsSuccess = sectionResponseModel.Succeeded;
                    ViewBag.AddSectionError = sectionResponseModel.message;
                }
            }
            return View(sectionModel);

        }

        /// <summary>
        /// Gives Section details using Id :by Vishram Sawant
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<GetSectionByIdResponseModel> GetSectionById(int Id)
        {

            var classes = await _sectionRepository.GetSectionById(_apiBaseUrl.Value.LmsApiBaseUrl,Id);
            return classes;
        }






        /// <summary>
        ///  Gives all the Section details:by Vishram Sawant
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<IEnumerable<SectionDetailsViewModel>> GetAllSectionDetails()
        {
            var data = new List<SectionDetailsViewModel>();

            var dataList = await _sectionRepository.GetAllSectionDetails(_apiBaseUrl.Value.LmsApiBaseUrl);
            foreach (var sections in dataList.data)
            {
                data.Add(new SectionDetailsViewModel()
                {
                    Id = sections.Id,
                    SectionName = sections.SectionName,


                    CreatedDate = sections.CreatedDate,

                    IsActive = sections.IsActive,

                    SchoolName = sections.School.SchoolName,

                    ClassName = sections.Class.ClassName,

                });
            }
            return data;
        }

        /// <summary>
        /// Custom Pagination for Section module:by Vishram Sawant
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>

        [HttpGet]
        public async Task<IEnumerable<SectionDetailsViewModel>> GetAllSectionsDetailsByPagination(int page = 1, int size = 5)
        {
            int recsCount = (await _sectionRepository.GetAllSectionDetails(_apiBaseUrl.Value.LmsApiBaseUrl)).data.Count();
            if (page < 1)
                page = 1;
            var pager = new Pager(recsCount, page, size);

            this.ViewBag.Pager = pager;
            var data = new List<SectionDetailsViewModel>();

            //int recSkip = (page - 1) * size;
            var dataList = await _sectionRepository.GetSectionByPagination(_apiBaseUrl.Value.LmsApiBaseUrl,page, size);

            foreach (var sections in dataList.data.GetSectionListPaginationDto)
            {
                data.Add(new SectionDetailsViewModel()
                {
                    Id = sections.Id,
                    SectionName = sections.SectionName,


                    CreatedDate = sections.CreatedDate,

                    IsActive = sections.IsActive,

                    SchoolName = sections.School.SchoolName,

                    ClassName = sections.Class.ClassName,

                });
            }
            return data;
        }


        /// <summary>
        ///   get School list  :by Vishram Sawant
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
        /// get Section Name  :by Vishram Sawant
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<SelectListItem>> GetSectionName()
        {
            var data = await _sectionRepository.GetAllSectionDetails(_apiBaseUrl.Value.LmsApiBaseUrl);
            List<SelectListItem> sections = new List<SelectListItem>();
            foreach (var item in data.data)
            {
                sections.Add(new SelectListItem
                {
                    Text = item.SectionName,
                    Value = Convert.ToString(item.Id)
                });
            }
            return sections;
        }


        /// <summary>
        /// Updates the Section details of the given Id:by Vishram Sawant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> EditSection(int id)
        {
            var sections = await _sectionRepository.GetSectionById(_apiBaseUrl.Value.LmsApiBaseUrl,id);
            if (sections.data == null)
            {
                TempData["GetSectionById"] = sections.message;
                return RedirectToAction("ManageSection", "Section");
            }
            var sectionData = new UpdateSectionViewModel()
            {
                Id = sections.data.Id,
                SectionName = sections.data.SectionName,


                IsActive = sections.data.IsActive,

                SchoolId = sections.data.School.Id,

                ClassId = sections.data.Class.Id,


            };
            sectionData.Schools = await _common.GetSchool();
            sectionData.Classes = await _common.GetClass();

            return View(sectionData);
        }


        /// <summary>
        /// Post method to update the Section details :by Vishram Sawant
        /// </summary>
        /// <param name="updateSectionViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSection(UpdateSectionViewModel updateSectionViewModel)
        {
            ViewBag.UpdateSectionSuccess = null;
            ViewBag.UpdateSectionError = null;
            updateSectionViewModel.Schools = await _common.GetSchool();
            updateSectionViewModel.Classes = await _common.GetClass();

            UpdateSectionDto updateSection = new UpdateSectionDto();
            updateSection.Id = updateSectionViewModel.Id;
            if (ModelState.IsValid)
            {

                updateSection.SchoolId = updateSectionViewModel.SchoolId;

                updateSection.ClassId = updateSectionViewModel.ClassId;

                updateSection.SectionName = updateSectionViewModel.SectionName;


                updateSection.IsActive = updateSectionViewModel.IsActive;


                UpdateSectionResponseModel updateSectionResponseModel = null;
                ViewBag.UpdateSectionlSuccess = null;
                ViewBag.UpdateSectionError = null;
                ResponseModel responseModel = new ResponseModel();

                updateSectionResponseModel = await _sectionRepository.UpdateSection(_apiBaseUrl.Value.LmsApiBaseUrl,updateSection);


                if (updateSectionResponseModel.Succeeded)
                {
                    if (updateSectionResponseModel == null && updateSectionResponseModel?.data == null)
                    {
                        responseModel.ResponseMessage = updateSectionResponseModel.message;
                        responseModel.IsSuccess = updateSectionResponseModel.Succeeded;
                    }
                    if (updateSectionResponseModel != null)
                    {
                        if (updateSectionResponseModel?.data != null)
                        {
                            responseModel.ResponseMessage = updateSectionResponseModel.message;
                            responseModel.IsSuccess = updateSectionResponseModel.Succeeded;
                            ViewBag.UpdateSectionSuccess = "Details Updated Successfully";

                            var page = 1;
                            var size = 5;
                            int recsCount = (await _sectionRepository.GetAllSectionDetails(_apiBaseUrl.Value.LmsApiBaseUrl)).data.Count();
                            if (page < 1)
                                page = 1;
                            ViewBag.GetSectionById = TempData["GetSectionById"] as string;
                            var pager = new Pager(recsCount, page, size);
                            ViewBag.Pager = pager;
                            return View("ManageSection", pager);
                        }
                        else
                        {
                            responseModel.ResponseMessage = updateSectionResponseModel.message;
                            responseModel.IsSuccess = updateSectionResponseModel.Succeeded;
                            ViewBag.UpdateSectionError = updateSectionResponseModel.message;
                            return View(updateSectionResponseModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = updateSectionResponseModel.message;
                    responseModel.IsSuccess = updateSectionResponseModel.Succeeded;
                    ViewBag.UpdateSectionError = updateSectionResponseModel.message;
                }
            }
            return View(updateSectionViewModel);
        }

        /// <summary>
        /// Get Method for Section By Filters:by Vishram Sawant
        /// </summary>
        /// <param name="className"></param>
        /// <param name="schoolName"></param>
        /// <param name="sectionName"></param>
        /// <param name="createdDate"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<SectionDetailsViewModel>> GetSectionByFilters(string className, string schoolName, string sectionName, DateTime createdDate, bool isActive)
        {
            var data = new List<SectionDetailsViewModel>();
            var dataList = await _sectionRepository.GetSectionByFilters(_apiBaseUrl.Value.LmsApiBaseUrl,schoolName,  className, sectionName, createdDate, isActive);
            if (dataList.data != null)
            {
                foreach (var sections in dataList.data)
                {
                    data.Add(new SectionDetailsViewModel()
                    {
                        Id = sections.Id,

                        SectionName = sections.SectionName,

                        CreatedDate = sections.CreatedDate,

                        IsActive = sections.IsActive,

                        SchoolName = sections.SchoolId.SchoolName,

                        ClassName = sections.ClassId.ClassName,

                    });
                }
            }
            return data;
        }


    }
}
#endregion