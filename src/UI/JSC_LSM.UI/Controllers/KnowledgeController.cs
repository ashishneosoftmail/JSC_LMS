
using JSC_LMS.Application.Features.Common.Categories.Commands;
using JSC_LMS.Application.Features.KnowledgeBase.Commands.CreateKnowledgeBase;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddKnowledge(KnowledgeBaseViewModel KnowledgeBaseViewModel)
        {
            ViewBag.AddKnowledgeBaseSuccess = null;
            ViewBag.AddKnowledgeBaseError = null;

            CreateKnowledgeBaseDto createKnowledgeBaseDto = new CreateKnowledgeBaseDto();
            KnowledgeBaseViewModel.Categories = await _common.GetAllCategory();
            if (ModelState.IsValid)
            {
                createKnowledgeBaseDto.CategoryId = KnowledgeBaseViewModel.AddKnowledgeBase.CategoryId;
                createKnowledgeBaseDto.DocTitle = KnowledgeBaseViewModel.AddKnowledgeBase.DocTitle;
                createKnowledgeBaseDto.SubTitle = KnowledgeBaseViewModel.AddKnowledgeBase.SubTitle;
                createKnowledgeBaseDto.SlugUrl = KnowledgeBaseViewModel.AddKnowledgeBase.SlugUrl;
                createKnowledgeBaseDto.AddContent = KnowledgeBaseViewModel.AddKnowledgeBase.AddContent;
                createKnowledgeBaseDto.IsActive = true;

                AddKnowledgeBaseResponseModel addKnowledgeBaseResponseModel = null;
                ViewBag.AddKnowledgeBaseSuccess = null;
                ViewBag.AddKnowledgeBaseError = null;
                ResponseModel responseModel = new ResponseModel();

                addKnowledgeBaseResponseModel = await _knowledgebaseRepository.AddKnowledgeBase(createKnowledgeBaseDto);


                if (addKnowledgeBaseResponseModel.Succeeded)
                {
                    if (addKnowledgeBaseResponseModel == null && addKnowledgeBaseResponseModel?.data == null)
                    {
                        responseModel.ResponseMessage = addKnowledgeBaseResponseModel.message;
                        responseModel.IsSuccess = addKnowledgeBaseResponseModel.Succeeded;
                    }
                    if (addKnowledgeBaseResponseModel != null)
                    {
                        if (addKnowledgeBaseResponseModel?.data != null)
                        {
                            responseModel.ResponseMessage = addKnowledgeBaseResponseModel.message;
                            responseModel.IsSuccess = addKnowledgeBaseResponseModel.Succeeded;
                            ViewBag.AddKnowledgeBaseSuccess = "KnowledgeBase Added Successfully";
                            ModelState.Clear();
                            KnowledgeBaseViewModel knowledgeBaseViewModel = new KnowledgeBaseViewModel();
                            knowledgeBaseViewModel.Categories = await _common.GetAllCategory();
                            return View("KnowledgeBase", knowledgeBaseViewModel);
                        }
                        else
                        {
                            responseModel.ResponseMessage = addKnowledgeBaseResponseModel.message;
                            responseModel.IsSuccess = addKnowledgeBaseResponseModel.Succeeded;
                            ViewBag.AddKnowledgeBaseError = addKnowledgeBaseResponseModel.message;
                            return View("KnowledgeBase", KnowledgeBaseViewModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = addKnowledgeBaseResponseModel.message;
                    responseModel.IsSuccess = addKnowledgeBaseResponseModel.Succeeded;
                    ViewBag.AddKnowledgeBaseError = addKnowledgeBaseResponseModel.message;
                }
            }
            return View("KnowledgeBase", KnowledgeBaseViewModel);
        }
    }
}
