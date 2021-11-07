using JSC_LMS.Application.Features.Section.Commands.CreateSection;
using JSC_LSM.UI.Models;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;
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
        public SectionController(JSC_LSM.UI.Common.Common common, ISchoolRepository schoolRepository, ISectionRepository sectionRepository)
        {
            _common = common;
            _schoolRepository = schoolRepository;
            _sectionRepository = sectionRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddPrincipal()
        {
            SectionModel sectionModel = new SectionModel();
     
            sectionModel.Schools = await _common.GetSchool();
            return View(sectionModel);
        }
      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPrincipal(SectionModel sectionModel)
        {
            ViewBag.AddPrincipalSuccess = null;
            ViewBag.AddPrincipalError = null;
          
            sectionModel.Schools = await _common.GetSchool();
            CreateSectionDto createNewSection = new CreateSectionDto();
           
            if (ModelState.IsValid)
            {

                createNewSection.SchoolId = sectionModel.SchoolId;
                createNewSection.SectionName = sectionModel.SectionName;
                createNewSection.ClassId = sectionModel.ClassId;
               


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
                            return View(newSectionModel);
                        }
                        else
                        {
                            responseModel.ResponseMessage = sectionResponseModel.message;
                            responseModel.IsSuccess = sectionResponseModel.Succeeded;
                            ViewBag.AddSectionError = sectionResponseModel.message;
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
     
    }
}
