using JSC_LMS.Application.Features.Class.Commands.CreateClass;
using JSC_LSM.UI.Models;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Controllers
{
    public class ClassController : BaseController
    {
        private readonly JSC_LSM.UI.Common.Common _common;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IClassRepository _classRepository;

        public ClassController(JSC_LSM.UI.Common.Common common, ISchoolRepository schoolRepository, IClassRepository classRepository)
        {
            _common = common;
            _schoolRepository = schoolRepository;
            _classRepository = classRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ManageClass()
        {
            ClassModel classModel = new ClassModel();

            classModel.Schools = await _common.GetSchool();

            return View(classModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddClass(ClassModel classModel)
        {
            ViewBag.AddPrincipalSuccess = null;
            ViewBag.AddPrincipalError = null;

            classModel.Schools = await _common.GetSchool();
            CreateClassDto createNewClass = new CreateClassDto();
          
            if (ModelState.IsValid)
            {

                createNewClass.SchoolId = classModel.SchoolId;
                createNewClass.ClassName = classModel.ClassName;



                createNewClass.IsActive = classModel.IsActive;
               


                ClassResponseModel classResponseModel = null;
                ViewBag.AddClassSuccess = null;
                ViewBag.AddClassError = null;
                ResponseModel responseModel = new ResponseModel();

                classResponseModel = await _classRepository.AddNewClass(createNewClass);


                if (classResponseModel.Succeeded)
                {
                    if (classResponseModel == null && classResponseModel.data == null)
                    {
                        responseModel.ResponseMessage = classResponseModel.message;
                        responseModel.IsSuccess = classResponseModel.Succeeded;
                    }
                    if (classResponseModel != null)
                    {
                        if (classResponseModel.data != null)
                        {
                            responseModel.ResponseMessage = classResponseModel.message;
                            responseModel.IsSuccess = classResponseModel.Succeeded;
                            ViewBag.AddClassSuccess = "Details Added Successfully";
                            ModelState.Clear();
                            var newClassModel = new ClassModel();
                         
                            newClassModel.Schools = await _common.GetSchool();
                            return View(newClassModel);
                        }
                        else
                        {
                            responseModel.ResponseMessage = classResponseModel.message;
                            responseModel.IsSuccess = classResponseModel.Succeeded;
                            ViewBag.AddClassError = classResponseModel.message;
                            return View(classModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = classResponseModel.message;
                    responseModel.IsSuccess = classResponseModel.Succeeded;
                    ViewBag.AddClassError = classResponseModel.message;
                }
            }
            return View(classModel);

        }







    }
    
}