
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
        private readonly JSC_LSM.UI.Common.Common _common;
        private readonly IKnowledgeBaseRepository _knowledgebaseRepository;
        public KnowledgeController(ICategoryRepository categoryRepository, IKnowledgeBaseRepository knowledgebaseRepository, JSC_LSM.UI.Common.Common common)
        {
            _categoryRepository = categoryRepository;
            _knowledgebaseRepository = knowledgebaseRepository;
            _common = common;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> KnowledgeBase()
        {
            KnowledgeBaseViewModel knowledgeBaseViewModel = new KnowledgeBaseViewModel();
            var category = await _common.GetAllCategory();
            knowledgeBaseViewModel.Categories = await _common.GetAllCategory();
            return View(knowledgeBaseViewModel);
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
