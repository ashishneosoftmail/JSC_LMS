using JSC_LMS.Application.Features.Class.Commands.CreateClass;
using JSC_LMS.Application.Features.Class.Commands.UpdateClass;
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

#region- Controller for Class module:by Vishram Sawant
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

        /// <summary>
        ///  Gives all the details in form of a list:by Vishram Sawant
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ManageClass()
        {
            var page = 1;
            var size = 5;
            int recsCount = (await _classRepository.GetAllClass()).data.Count();
            if (page < 1)
                page = 1;

            var pager = new Pager(recsCount, page, size);
            this.ViewBag.Pager = pager;
            return View(pager);


           
        }

        /// <summary>
        ///   method To get Class Details :by Vishram Sawant
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        ///  Get method To Add a new Class :by Vishram Sawant
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> AddClass()
        {
            ClassModel classModel = new ClassModel();

            classModel.Schools = await _common.GetSchool();
            return View(classModel);
        }


        /// <summary>
        /// Post method To Add a new Class :by Vishram Sawant
        /// </summary>
        /// <param name="classModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddClass(ClassModel classModel)
        
        
        
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
                           
                            return RedirectToAction("ManageClass", "Class");
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

        /// <summary>
        /// Gives Class details using Id :by Vishram Sawant
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        [HttpGet]
        public async Task<GetClassByIdResponseModel> GetClassById(int Id)
        {

            var classes = await _classRepository.GetClassById(Id);
            return classes;
        }




        /// <summary>
        /// Get Method for Class By Filters:by Vishram Sawant
        /// </summary>
        /// <param name="className"></param>
        /// <param name="schoolName"></param>
        /// <param name="createdDate"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>

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

        /// <summary>
        /// Method to get All Class Details
        /// </summary>
        /// <returns></returns>

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

        /// <summary>
        /// Custom Pagination for Class module:by Vishram Sawant
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<ClassDetailsViewModel>> GetAllClasssDetailsByPagination(int page = 1, int size = 5)
        {
            int recsCount = (await _classRepository.GetAllClass()).data.Count();
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


        /// <summary>
        /// Method to get School List:by Vishram Sawant
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<SelectListItem>> GetAllSchool()
        {
            var schools = await _common.GetSchool();
            return schools;
        }

        /// <summary>
        /// Method to get Class Name:by Vishram Sawant
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<SelectListItem>> GetClassName()
        {
            var data = await _classRepository.GetAllClass();
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

        /// <summary>
        ///  Updates the Class details of the given Id:by Vishram Sawant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> EditClass(int id)
        {
            var classes = await _classRepository.GetClassById(id);
            if (classes.data == null)
            {
                TempData["GetClassById"] = classes.message;
                return RedirectToAction("ManageClass", "Class");
            }
            var classData = new UpdateClassViewModel()
            {
                Id = classes.data.Id,
                ClassName = classes.data.ClassName,
          
              
                IsActive = classes.data.IsActive,
             
                SchoolId = classes.data.School.Id,
               
      
            };
            classData.Schools = await _common.GetSchool();
        
            return View(classData);
        }

        /// <summary>
        /// Post method to update the Class details :by Vishram Sawant
        /// </summary>
        /// <param name="updateClassViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditClass(UpdateClassViewModel updateClassViewModel)
        {
            ViewBag.UpdateClassSuccess = null;
            ViewBag.UpdateClassError = null;
            updateClassViewModel.Schools = await _common.GetSchool();
          
            UpdateClassDto updateClass = new UpdateClassDto();
                updateClass.Id = updateClassViewModel.Id;
            if (ModelState.IsValid)
            {

                updateClass.SchoolId = updateClassViewModel.SchoolId;

                updateClass.ClassName = updateClassViewModel.ClassName;


                updateClass.IsActive = updateClassViewModel.IsActive;


                UpdateClassResponseModel updateClassResponseModel = null;
                ViewBag.UpdateClasslSuccess = null;
                ViewBag.UpdateClassError = null;
                ResponseModel responseModel = new ResponseModel();

                updateClassResponseModel = await _classRepository.UpdateClass(updateClass);


                if (updateClassResponseModel.Succeeded)
                {
                    if (updateClassResponseModel == null && updateClassResponseModel?.data == null)
                    {
                        responseModel.ResponseMessage = updateClassResponseModel.message;
                        responseModel.IsSuccess = updateClassResponseModel.Succeeded;
                    }
                    if (updateClassResponseModel != null)
                    {
                        if (updateClassResponseModel?.data != null)
                        {
                            responseModel.ResponseMessage = updateClassResponseModel.message;
                            responseModel.IsSuccess = updateClassResponseModel.Succeeded;
                            ViewBag.UpdateClassSuccess = "Details Updated Successfully";

                            return RedirectToAction("ManageClass", "Class");
                        }
                        else
                        {
                            responseModel.ResponseMessage = updateClassResponseModel.message;
                            responseModel.IsSuccess = updateClassResponseModel.Succeeded;
                            ViewBag.UpdateClassError = updateClassResponseModel.message;
                            return View(updateClassResponseModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = updateClassResponseModel.message;
                    responseModel.IsSuccess = updateClassResponseModel.Succeeded;
                    ViewBag.UpdateClassError = updateClassResponseModel.message;
                }
            }
            return View(updateClassViewModel);
        }









    }

}
#endregion




