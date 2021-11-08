using JSC_LMS.Application.Features.Class.Commands.CreateClass;
using JSC_LSM.UI.Models;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var page = 1;
            var size = 5;
            int recsCount = (await _classRepository.GetAllClassDetails()).data.Count();
            if (page < 1)
                page = 1;

            var pager = new Pager(recsCount, page, size);
            this.ViewBag.Pager = pager;
            return View(pager);


           
        }


        public async Task<IActionResult> ClassDetails()
        {
            var page = 1;
            var size = 5;
            int recsCount = (await _classRepository.GetAllClassDetails()).data.Count();
            if (page < 1)
                page = 1;

            var pager = new Pager(recsCount, page, size);
            this.ViewBag.Pager = pager;
            return View(pager);


        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageClass(ClassModel classModel)
        
        
        
        {
            ViewBag.AddClassSuccess = null;
            ViewBag.AddClassError = null;

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

        [HttpGet]
        public async Task<GetClassByIdResponseModel> GetClassById(int Id)
        {

            var classes = await _classRepository.GetClassById(Id);
            return classes;
        }






        [HttpGet]
        public async Task<IEnumerable<ClassDetailsViewModel>> GetClassByFilters(string className, string schoolName, DateTime createdDate, bool isActive)
        {
            var data = new List<ClassDetailsViewModel>();
            var dataList = await _classRepository.GetClassByFilters(schoolName, className, createdDate, isActive);
            if (dataList.data != null)
            {
                foreach (var classes in dataList.data)
                {
                    data.Add(new ClassDetailsViewModel()
                    {
                        Id = classes.Id,

                        ClassName = classes.ClassName,
                  
                        CreatedDate = classes.CreatedDate,
                       
                        IsActive = classes.IsActive,
                   
                        SchoolName = classes.School.SchoolName,
                 
                    });
                }
            }
            return data;
        }

        [HttpGet]
        public async Task<IEnumerable<ClassDetailsViewModel>> GetAllClassDetails()
        {
            var data = new List<ClassDetailsViewModel>();

            var dataList = await _classRepository.GetAllClassDetails();
            foreach (var classes in dataList.data)
            {
                data.Add(new ClassDetailsViewModel()
                {
                    Id = classes.Id,
                    ClassName = classes.ClassName,
               
                    
                    CreatedDate = classes.CreatedDate,
                  
                    IsActive = classes.IsActive,
                  
                    SchoolName = classes.School.SchoolName,
                 
                });
            }
            return data;
        }

        [HttpGet]
        public async Task<IEnumerable<ClassDetailsViewModel>> GetAllClasssDetailsByPagination(int page = 1, int size = 5)
        {
            int recsCount = (await _classRepository.GetAllClassDetails()).data.Count();
            if (page < 1)
                page = 1;
            var pager = new Pager(recsCount, page, size);

            this.ViewBag.Pager = pager;
            var data = new List<ClassDetailsViewModel>();

            //int recSkip = (page - 1) * size;
            var dataList = await _classRepository.GetClassByPagination(page, size);

            foreach (var classes in dataList.data.GetClassListPaginationDto)
            {
                data.Add(new ClassDetailsViewModel()
                {
                    Id = classes.Id,
                    ClassName = classes.ClassName,
              
                  
                    CreatedDate = classes.CreatedDate,
              
                    IsActive = classes.IsActive,
                
                    SchoolName = classes.School.SchoolName,
                   
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
        public async Task<List<SelectListItem>> GetClassName()
        {
            var data = await _classRepository.GetAllClassDetails();
            List<SelectListItem> classes = new List<SelectListItem>();
            foreach (var item in data.data)
            {
                classes.Add(new SelectListItem
                {
                    Text = item.ClassName,
                    Value = Convert.ToString(item.Id)
                });
            }
            return classes;
        }











    }

}





