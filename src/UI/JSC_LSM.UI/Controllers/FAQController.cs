using ClosedXML.Excel;
using JSC_LMS.Application.Features.Categories.Commands;
using JSC_LMS.Application.Features.Common.Categories.Commands;
using JSC_LMS.Application.Features.FAQ.Commands.CreateFAQ;
using JSC_LMS.Application.Features.FAQ.Commands.UpdateFAQ;
using JSC_LSM.UI.Models;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Controllers
{
    public class FAQController : BaseController
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly JSC_LSM.UI.Common.Common _common;
        private readonly IFAQRepository _faqRepository;
        public FAQController(ICategoryRepository categoryRepository, IFAQRepository faqRepository, JSC_LSM.UI.Common.Common common)
        {
            _categoryRepository = categoryRepository;
            _faqRepository = faqRepository;
            _common = common;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> FAQ()
        {
           FAQViewModel faqViewModel = new FAQViewModel();
            faqViewModel.Categories = await _common.GetAllCategory();
            return View(faqViewModel);
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

                            return RedirectToAction("FAQ", "FAQ");
                        }
                        else
                        {
                            responseModel.ResponseMessage = createCategoryResponseModel.message;
                            responseModel.IsSuccess = createCategoryResponseModel.Succeeded;
                            ViewBag.AddCategoryError = createCategoryResponseModel.message;
                            return View("FAQ");
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
            return View("FAQ");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFAQ(FAQViewModel FAQViewModel)
        {
            ViewBag.AddKnowledgeBaseSuccess = null;
            ViewBag.AddKnowledgeBaseError = null;

            CreateFAQDto createFAQBaseDto = new CreateFAQDto();
            FAQViewModel.Categories = await _common.GetAllCategory();
            if (ModelState.IsValid)
            {
                createFAQBaseDto.CategoryId = FAQViewModel.AddFAQ.CategoryId;
                createFAQBaseDto.FAQTitle = FAQViewModel.AddFAQ.FAQTitle;
                createFAQBaseDto.Content = FAQViewModel.AddFAQ.Content;
                createFAQBaseDto.Question = FAQViewModel.AddFAQ.Question;

                createFAQBaseDto.IsActive = true;

                AddFAQBaseResponseModel addFAQResponseModel = null;
                ViewBag.AddFAQSuccess = null;
                ViewBag.AddFAQError = null;

                ResponseModel responseModel = new ResponseModel();

                addFAQResponseModel = await _faqRepository.AddFAQ(createFAQBaseDto);


                if (addFAQResponseModel.Succeeded)
                {
                    if (addFAQResponseModel == null && addFAQResponseModel?.data == null)
                    {
                        responseModel.ResponseMessage = addFAQResponseModel.message;
                        responseModel.IsSuccess = addFAQResponseModel.Succeeded;
                    }
                    if (addFAQResponseModel != null)
                    {
                        if (addFAQResponseModel?.data != null)
                        {
                            responseModel.ResponseMessage = addFAQResponseModel.message;
                            responseModel.IsSuccess = addFAQResponseModel.Succeeded;
                            ViewBag.AddKnowledgeBaseSuccess = "FAQ Updated Successfully";
                            ModelState.Clear();
                           FAQViewModel faqViewModel = new FAQViewModel();
                            faqViewModel.Categories = await _common.GetAllCategory();
                            return RedirectToAction("FAQList", "FAQ");
                        }
                        else
                        {
                            responseModel.ResponseMessage = addFAQResponseModel.message;
                            responseModel.IsSuccess = addFAQResponseModel.Succeeded;
                            ViewBag.AddFAQError = addFAQResponseModel.message;
                            return View(FAQViewModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = addFAQResponseModel.message;
                    responseModel.IsSuccess = addFAQResponseModel.Succeeded;
                    ViewBag.AddFAQError = addFAQResponseModel.message;
                }
            }
            return View(FAQViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> EditFAQ(int id)
        {
            FAQViewModel faqViewModel = new FAQViewModel();
            var faq = await _faqRepository.GetFAQById(id);
            if (faq.data == null)
            {
                TempData["GetKnowledgeBaseById"] = faq.message;
                return RedirectToAction("KnowledgeBase", "Knowledge");
            }

            faqViewModel.UpdateFAQ = new UpdateFAQ()
            {
                Id = faq.data.Id,
                CategoryId = faq.data.CategoryId,
                Question = faq.data.Question,
                FAQTitle = faq.data.FAQTitle,
                IsActive = faq.data.IsActive,
                Content = faq.data.Content
            };
            TempData["EditId"] = faqViewModel.UpdateFAQ.Id;
            faqViewModel.Categories = await _common.GetAllCategory();
            return View(faqViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFAQ(FAQViewModel faqViewModel)
        {

            ViewBag.UpdateFAQSuccess = null;
            ViewBag.UpdateFAQError = null;
            faqViewModel.Categories = await _common.GetAllCategory();
            UpdateFAQDto updateKnowledgeBaseDto = new UpdateFAQDto();
            if (ModelState.IsValid)
            {
                updateKnowledgeBaseDto.Id = Convert.ToInt32(TempData["EditId"].ToString());
                updateKnowledgeBaseDto.CategoryId = faqViewModel.UpdateFAQ.CategoryId;
                updateKnowledgeBaseDto.FAQTitle = faqViewModel.UpdateFAQ.FAQTitle;
                updateKnowledgeBaseDto.Question
                    = faqViewModel.UpdateFAQ.Question;
                updateKnowledgeBaseDto.IsActive = faqViewModel.UpdateFAQ.IsActive;
                updateKnowledgeBaseDto.Content = faqViewModel.UpdateFAQ.Content;

                UpdateFAQResponseModel updateFAQResponseModel = null;
                ViewBag.UpdateFAQSuccess = null;
                ViewBag.UpdateFAQError = null;
                ResponseModel responseModel = new ResponseModel();

                updateFAQResponseModel = await _faqRepository.EditFAQ(updateKnowledgeBaseDto);


                if (updateFAQResponseModel.Succeeded)
                {
                    if (updateFAQResponseModel == null && updateFAQResponseModel?.data == null)
                    {
                        responseModel.ResponseMessage = updateFAQResponseModel.message;
                        responseModel.IsSuccess = updateFAQResponseModel.Succeeded;
                    }
                    if (updateFAQResponseModel != null)
                    {
                        if (updateFAQResponseModel?.data != null)
                        {
                            responseModel.ResponseMessage = updateFAQResponseModel.message;
                            responseModel.IsSuccess = updateFAQResponseModel.Succeeded;
                            ViewBag.UpdateFAQSuccess = "Details Updated Successfully";

                            return RedirectToAction("FAQist", "FAQ");
                        }
                        else
                        {
                            responseModel.ResponseMessage = updateFAQResponseModel.message;
                            responseModel.IsSuccess = updateFAQResponseModel.Succeeded;
                            ViewBag.UpdateFAQError = updateFAQResponseModel.message;
                            return View(faqViewModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = updateFAQResponseModel.message;
                    responseModel.IsSuccess = updateFAQResponseModel.Succeeded;
                    ViewBag.UpdateFAQError = updateFAQResponseModel.message;
                }
            }
            return View(faqViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> FAQList()
        {
            var page = 1;
            var size = 5;
            int recsCount = (await _faqRepository.GetAllFAQList()).data.Count();
            if (page < 1)
                page = 1;
            /*ViewBag.GetPrincipalById = TempData["GetPrincipalById"] as string;*/
            var pager = new Pager(recsCount, page, size);
            ViewBag.Pager = pager;
            return View(pager);
        }

        [HttpGet]
        public async Task<IEnumerable<FAQList>> GetAllFAQByPagination(int page = 1, int size = 5)
        {
            int recsCount = (await _faqRepository.GetAllFAQList()).data.Count();
            if (page < 1)
                page = 1;
            var pager = new Pager(recsCount, page, size);

            ViewBag.Pager = pager;
            var data = new List<FAQList>();

            var dataList = await _faqRepository.GetAllFAQByPagination(page, size);

            foreach (var faq in dataList.data)
            {
                data.Add(new FAQList()
                {
                    Id = faq.Id,
                    FAQTitle = faq.FAQTitle,
                    CategoryName = faq.Category.CategoryName,
                    Content = faq.Content,
                    IsActive= faq.IsActive
                });
                ;
            }
            return data;
        }

        [HttpGet]
        public async Task<List<SelectListItem>> GetAllCategory()
        {
            var categories = await _common.GetAllCategory();
            return categories;
        }


        [HttpGet]
        public async Task<IEnumerable<FAQList>> GetFAQByFilters(string faqtitle, bool isactive, string categoryname)
        {
            var data = new List<FAQList>();
            var dataList = await _faqRepository.GetAllFAQByFilters(faqtitle, isactive, categoryname);
            if (dataList.data != null)
            {
                foreach (var faq in dataList.data)
                {
                    data.Add(new FAQList()
                    {
                        Id = faq.Id,
                        FAQTitle = faq.FAQTitle,
                        Content=faq.Content,
                        CategoryName = faq.Category.CategoryName,
                        IsActive = faq.IsActive
                    });
                }
            }
            return data;
        }

        [HttpGet]
        public async Task<IActionResult> ViewFAQ(int id)
        {
           FAQList faqList = new FAQList();
            var faq = await _faqRepository.GetFAQById(id);
            if (faq.data == null)
            {
                TempData["GetKnowledgeBaseById"] = faq.message;
                return RedirectToAction("KnowledgeBase", "Knowledge");
            }

            faqList = new FAQList()
            {
                Id = faq.data.Id,
                CategoryName = faq.data.Category.CategoryName,
                FAQTitle = faq.data.FAQTitle,
                Question = faq.data.Question,
                IsActive = faq.data.IsActive,
                Content = faq.data.Content
            };

            return View(faqList);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadExcel()
        {
            var data = new List<PrincipalDetailsViewModel>();

            var dataList = await _faqRepository.GetAllFAQList();
            //Creating DataTable  
            DataTable dt = new DataTable();
            //Setiing Table Name  
            dt.TableName = "FAQ";
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Category Name", typeof(string));
            dt.Columns.Add("FAQTitle", typeof(string));
            dt.Columns.Add("Question", typeof(string));
         
            dt.Columns.Add("Content", typeof(string));
            foreach (var faq in dataList.data)
            {
                dt.Rows.Add(faq.Id, faq.Category.CategoryName, faq.FAQTitle, faq.FAQTitle, faq.Content);
            }
            string fileName = "FAQData_" + DateTime.Now.ToShortDateString() + ".xlsx";

            using (XLWorkbook wb = new XLWorkbook())
            {

                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);

                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }

            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteFAQ(int id)
        {
            await _faqRepository.DeleteFAQ(id);
            return RedirectToAction("FAQList");
        }

    }
}
