using JSC_LMS.Application.Features.Section.Commands.CreateSection;
using JSC_LMS.Application.Features.Section.Commands.CreateUpdate;
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
    public class SectionController : BaseController
    {
        // public IActionResult AddSection()
        //{
        //   return View();
        //}
        //public IActionResult ManageSection()
        //{
        //  return View();
        // }
        private readonly JSC_LSM.UI.Common.Common _common;
        private readonly ISchoolRepository _schoolRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IClassRepository _classRepository;
        public SectionController(JSC_LSM.UI.Common.Common common, IClassRepository classRepository, ISchoolRepository schoolRepository, ISectionRepository sectionRepository)
        {
            _common = common;
            _schoolRepository = schoolRepository;
            _sectionRepository = sectionRepository;
            _classRepository = classRepository;
        }
        [HttpGet]
        public async Task<IActionResult> ManageSection()
        {
            var page = 1;
            var size = 5;
            int recsCount = (await _sectionRepository.GetAllSectionDetails()).data.Count();
            if (page < 1)
                page = 1;

            var pager = new Pager(recsCount, page, size);
            this.ViewBag.Pager = pager;
            return View(pager);



        }


     


        [HttpGet]
        public async Task<IActionResult> AddSection()
        {
            SectionModel sectionModel = new SectionModel();

            sectionModel.Schools = await _common.GetSchool();
            sectionModel.Classes = await _common.GetClass();
            return View(sectionModel);
        }



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

                sectionResponseModel = await _sectionRepository.AddNewSection(createNewSection);


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
                            return View(newSectionModel);
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

        [HttpGet]
        public async Task<GetSectionByIdResponseModel> GetSectionById(int Id)
        {

            var classes = await _sectionRepository.GetSectionById(Id);
            return classes;
        }






 

        [HttpGet]
        public async Task<IEnumerable<SectionDetailsViewModel>> GetAllSectionDetails()
        {
            var data = new List<SectionDetailsViewModel>();

            var dataList = await _sectionRepository.GetAllSectionDetails();
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

        [HttpGet]
        public async Task<IEnumerable<SectionDetailsViewModel>> GetAllSectionsDetailsByPagination(int page = 1, int size = 5)
        {
            int recsCount = (await _sectionRepository.GetAllSectionDetails()).data.Count();
            if (page < 1)
                page = 1;
            var pager = new Pager(recsCount, page, size);

            this.ViewBag.Pager = pager;
            var data = new List<SectionDetailsViewModel>();

            //int recSkip = (page - 1) * size;
            var dataList = await _sectionRepository.GetSectionByPagination(page, size);

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
        public async Task<List<SelectListItem>> GetSectionName()
        {
            var data = await _sectionRepository.GetAllSectionDetails();
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

        [HttpGet]
        public async Task<IActionResult> EditSection(int id)
        {
            var sections = await _sectionRepository.GetSectionById(id);
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

                updateSectionResponseModel = await _sectionRepository.UpdateSection(updateSection);


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

                            return RedirectToAction("ManageSection", "Section");
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


        [HttpGet]
        public async Task<IEnumerable<SectionDetailsViewModel>> GetSectionByFilters(string className, string schoolName, string sectionName, DateTime createdDate, bool isActive)
        {
            var data = new List<SectionDetailsViewModel>();
            var dataList = await _sectionRepository.GetSectionByFilters(schoolName, sectionName, className, createdDate, isActive);
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
