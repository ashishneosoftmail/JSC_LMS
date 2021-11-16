
using JSC_LMS.Application.Features.Common.Categories.Commands;
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
    public class KnowledgeController : BaseController
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IKnowledgeBaseRepository _knowledgebaseRepository;
        public KnowledgeController(ICategoryRepository categoryRepository, IKnowledgeBaseRepository knowledgebaseRepository)
        {
            _categoryRepository = categoryRepository;
            _knowledgebaseRepository = knowledgebaseRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult KnowledgeBase()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(AddCategory AddCategory)
        {
            ViewBag.AddCategorySuccess = null;
            ViewBag.AddCategoryError = null;

            CreateCategoryDto createCategoryDto = new CreateCategoryDto();

            if (ModelState.IsValid)
            {
                createCategoryDto.CategoryName = AddCategory.CategoryName;
                createCategoryDto.IsActive = true;
                CreateCategoryResponseModel createCategoryResponseModel = null;
                ViewBag.AddCategorySuccess = null;
                ViewBag.AddCategoryError = null;
                ResponseModel responseModel = new ResponseModel();

                createCategoryResponseModel = await _categoryRepository.AddCategory(createCategoryDto);


                if (createCategoryResponseModel.Succeeded)
                {
                    if (createCategoryResponseModel == null && createCategoryResponseModel?.data == null)
                    {
                        responseModel.ResponseMessage = createCategoryResponseModel.message;
                        responseModel.IsSuccess = createCategoryResponseModel.Succeeded;
                    }
                    if (createCategoryResponseModel != null)
                    {
                        if (createCategoryResponseModel?.data != null)
                        {
                            responseModel.ResponseMessage = createCategoryResponseModel.message;
                            responseModel.IsSuccess = createCategoryResponseModel.Succeeded;
                            ViewBag.AddCategorySuccess = "Category Added Successfully";
                            ModelState.Clear();

                            return View("KnowledgeBase");
                        }
                        else
                        {
                            responseModel.ResponseMessage = createCategoryResponseModel.message;
                            responseModel.IsSuccess = createCategoryResponseModel.Succeeded;
                            ViewBag.AddCategoryError = createCategoryResponseModel.message;
                            return View("KnowledgeBase");
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = createCategoryResponseModel.message;
                    responseModel.IsSuccess = createCategoryResponseModel.Succeeded;
                    ViewBag.AddCategoryError = createCategoryResponseModel.message;
                }
            }
            return View("KnowledgeBase");
        }
    }
}
