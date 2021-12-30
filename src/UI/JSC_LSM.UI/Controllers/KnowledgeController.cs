
using ClosedXML.Excel;
using JSC_LMS.Application.Features.Common.Categories.Commands;
using JSC_LMS.Application.Features.KnowledgeBase.Commands.CreateKnowledgeBase;
using JSC_LMS.Application.Features.KnowledgeBase.Commands.UpdateKnowledgeBase;
using JSC_LSM.UI.Helpers;
using JSC_LSM.UI.Models;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Controllers
{
    public class KnowledgeController : BaseController
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly JSC_LSM.UI.Common.Common _common;
        private readonly IKnowledgeBaseRepository _knowledgebaseRepository;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        public KnowledgeController(ICategoryRepository categoryRepository, IKnowledgeBaseRepository knowledgebaseRepository, JSC_LSM.UI.Common.Common common, IOptions<ApiBaseUrl> apiBaseUrl)
        {
            _categoryRepository = categoryRepository;
            _knowledgebaseRepository = knowledgebaseRepository;
            _common = common;
            _apiBaseUrl = apiBaseUrl;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
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

                createCategoryResponseModel = await _categoryRepository.AddCategory(_apiBaseUrl.Value.LmsApiBaseUrl,createCategoryDto);


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
                            KnowledgeBaseViewModel knowledgeBaseViewModel = new KnowledgeBaseViewModel();
                            knowledgeBaseViewModel.Categories = await _common.GetAllCategory();
                            return View("KnowledgeBase", knowledgeBaseViewModel);
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

                addKnowledgeBaseResponseModel = await _knowledgebaseRepository.AddKnowledgeBase(_apiBaseUrl.Value.LmsApiBaseUrl,createKnowledgeBaseDto);


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
                            var page = 1;
                            var size = 5;
                            int recsCount = (await _knowledgebaseRepository.GetAllKnowledgeBaseList(_apiBaseUrl.Value.LmsApiBaseUrl)).data.Count();
                            if (page < 1)
                                page = 1;
                            /*ViewBag.GetPrincipalById = TempData["GetPrincipalById"] as string;*/
                            var pager = new Pager(recsCount, page, size);
                            ViewBag.Pager = pager;
                            return View("KnowledgeBaseList", pager);
                        }
                        else
                        {
                            responseModel.ResponseMessage = addKnowledgeBaseResponseModel.message;
                            responseModel.IsSuccess = addKnowledgeBaseResponseModel.Succeeded;
                            ViewBag.AddKnowledgeBaseError = addKnowledgeBaseResponseModel.message;
                            return View(KnowledgeBaseViewModel);
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
            return View(KnowledgeBaseViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditKnowledge(int id)
        {
            KnowledgeBaseViewModel knowledgeBaseViewModel = new KnowledgeBaseViewModel();
            var knowledgebase = await _knowledgebaseRepository.GetKnowlegebaseById(_apiBaseUrl.Value.LmsApiBaseUrl,id);
            if (knowledgebase.data == null)
            {
                TempData["GetKnowledgeBaseById"] = knowledgebase.message;
                return RedirectToAction("KnowledgeBase", "Knowledge");
            }

            knowledgeBaseViewModel.UpdateKnowledgeBase = new UpdateKnowledgeBase()
            {
                Id = knowledgebase.data.Id,
                CategoryId = knowledgebase.data.CategoryId,
                DocTitle = knowledgebase.data.DocTitle,
                SubTitle = knowledgebase.data.SubTitle,
                SlugUrl = knowledgebase.data.SlugUrl,
                AddContent = knowledgebase.data.AddContent
            };
            TempData["EditId"] = knowledgeBaseViewModel.UpdateKnowledgeBase.Id;
            knowledgeBaseViewModel.Categories = await _common.GetAllCategory();
            return View(knowledgeBaseViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditKnowledge(KnowledgeBaseViewModel knowledgeBaseViewModel)
        {

            ViewBag.UpdateKnowledgeBaseSuccess = null;
            ViewBag.UpdatePrincipalError = null;
            knowledgeBaseViewModel.Categories = await _common.GetAllCategory();
            UpdateKnowledgeBaseDto updateKnowledgeBaseDto = new UpdateKnowledgeBaseDto();
            if (ModelState.IsValid)
            {
                updateKnowledgeBaseDto.Id = Convert.ToInt32(TempData["EditId"].ToString());
                updateKnowledgeBaseDto.CategoryId = knowledgeBaseViewModel.UpdateKnowledgeBase.CategoryId;
                updateKnowledgeBaseDto.DocTitle = knowledgeBaseViewModel.UpdateKnowledgeBase.DocTitle;
                updateKnowledgeBaseDto.SubTitle
                    = knowledgeBaseViewModel.UpdateKnowledgeBase.SubTitle;
                updateKnowledgeBaseDto.SlugUrl = knowledgeBaseViewModel.UpdateKnowledgeBase.SlugUrl;
                updateKnowledgeBaseDto.AddContent = knowledgeBaseViewModel.UpdateKnowledgeBase.AddContent;

                UpdateKnowledgeBaseResponseModel updateKnowledgeBaseResponseModel = null;
                ViewBag.UpdateKnowledgeBaseSuccess = null;
                ViewBag.UpdateKnowledgeBaseError = null;
                ResponseModel responseModel = new ResponseModel();

                updateKnowledgeBaseResponseModel = await _knowledgebaseRepository.EditKnowledgeBase(_apiBaseUrl.Value.LmsApiBaseUrl,updateKnowledgeBaseDto);


                if (updateKnowledgeBaseResponseModel.Succeeded)
                {
                    if (updateKnowledgeBaseResponseModel == null && updateKnowledgeBaseResponseModel?.data == null)
                    {
                        responseModel.ResponseMessage = updateKnowledgeBaseResponseModel.message;
                        responseModel.IsSuccess = updateKnowledgeBaseResponseModel.Succeeded;
                    }
                    if (updateKnowledgeBaseResponseModel != null)
                    {
                        if (updateKnowledgeBaseResponseModel?.data != null)
                        {
                            responseModel.ResponseMessage = updateKnowledgeBaseResponseModel.message;
                            responseModel.IsSuccess = updateKnowledgeBaseResponseModel.Succeeded;
                            ViewBag.UpdateKnowledgeBaseSuccess = "Details Updated Successfully";
                            var page = 1;
                            var size = 5;
                            int recsCount = (await _knowledgebaseRepository.GetAllKnowledgeBaseList(_apiBaseUrl.Value.LmsApiBaseUrl)).data.Count();
                            if (page < 1)
                                page = 1;
                            /*ViewBag.GetPrincipalById = TempData["GetPrincipalById"] as string;*/
                            var pager = new Pager(recsCount, page, size);
                            ViewBag.Pager = pager;
                            return View("KnowledgeBaseList",pager);
                        }
                        else
                        {
                            responseModel.ResponseMessage = updateKnowledgeBaseResponseModel.message;
                            responseModel.IsSuccess = updateKnowledgeBaseResponseModel.Succeeded;
                            ViewBag.UpdateKnowledgeBaseError = updateKnowledgeBaseResponseModel.message;
                            return View(knowledgeBaseViewModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = updateKnowledgeBaseResponseModel.message;
                    responseModel.IsSuccess = updateKnowledgeBaseResponseModel.Succeeded;
                    ViewBag.UpdateKnowledgeBaseError = updateKnowledgeBaseResponseModel.message;
                }
            }
            return View(knowledgeBaseViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> KnowledgeBaseList()
        {
            var page = 1;
            var size = 5;
            int recsCount = (await _knowledgebaseRepository.GetAllKnowledgeBaseList(_apiBaseUrl.Value.LmsApiBaseUrl)).data.Count();
            if (page < 1)
                page = 1;
            /*ViewBag.GetPrincipalById = TempData["GetPrincipalById"] as string;*/
            var pager = new Pager(recsCount, page, size);
            ViewBag.Pager = pager;
            return View(pager);
        }

        [HttpGet]
        public async Task<IEnumerable<KnowledgeBaseList>> GetAllKnowledgeBaseByPagination(int page = 1, int size = 5)
        {
            int recsCount = (await _knowledgebaseRepository.GetAllKnowledgeBaseList(_apiBaseUrl.Value.LmsApiBaseUrl)).data.Count();
            if (page < 1)
                page = 1;
            var pager = new Pager(recsCount, page, size);

            ViewBag.Pager = pager;
            var data = new List<KnowledgeBaseList>();

            var dataList = await _knowledgebaseRepository.GetAllKnowledgeBaseByPagination(_apiBaseUrl.Value.LmsApiBaseUrl,page, size);

            foreach (var knowledgeBase in dataList.data)
            {
                data.Add(new KnowledgeBaseList()
                {
                    Id = knowledgeBase.Id,
                    DocTitle = knowledgeBase.DocTitle,
                    CategoryName = knowledgeBase.Category.CategoryName,
                    SubTitle = knowledgeBase.SubTitle
                });
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
        public async Task<IEnumerable<KnowledgeBaseList>> GetKnowledgeBaseByFilters(string title, string subtitle, string categoryname)
        {
            var data = new List<KnowledgeBaseList>();
            var dataList = await _knowledgebaseRepository.GetAllKnowledgeBaseByFilters(_apiBaseUrl.Value.LmsApiBaseUrl,title, subtitle, categoryname);
            if (dataList.data != null)
            {
                foreach (var knowledgeBase in dataList.data)
                {
                    data.Add(new KnowledgeBaseList()
                    {
                        Id = knowledgeBase.Id,
                        DocTitle = knowledgeBase.DocTitle,
                        CategoryName = knowledgeBase.Category.CategoryName,
                        SubTitle = knowledgeBase.SubTitle
                    });
                }
            }
            return data;
        }

        [HttpGet]
        public async Task<IActionResult> ViewKnowledgeBase(int id)
        {
            KnowledgeBaseList knowledgeBaseList = new KnowledgeBaseList();
            var knowledgebase = await _knowledgebaseRepository.GetKnowlegebaseById(_apiBaseUrl.Value.LmsApiBaseUrl,id);
            if (knowledgebase.data == null)
            {
                TempData["GetKnowledgeBaseById"] = knowledgebase.message;
                return RedirectToAction("KnowledgeBase", "Knowledge");
            }

            knowledgeBaseList = new KnowledgeBaseList()
            {
                Id = knowledgebase.data.Id,
                CategoryName = knowledgebase.data.Category.CategoryName,
                DocTitle = knowledgebase.data.DocTitle,
                SubTitle = knowledgebase.data.SubTitle,
                SlugUrl = knowledgebase.data.SlugUrl,
                AddContent = knowledgebase.data.AddContent
            };

            return View(knowledgeBaseList);
        }
        [HttpGet]
        public async Task<IActionResult> DownloadExcel()
        {
            var data = new List<PrincipalDetailsViewModel>();

            var dataList = await _knowledgebaseRepository.GetAllKnowledgeBaseList(_apiBaseUrl.Value.LmsApiBaseUrl);
            //Creating DataTable  
            DataTable dt = new DataTable();
            //Setiing Table Name  
            dt.TableName = "Knowledge Base";
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Category Name", typeof(string));
            dt.Columns.Add("Doc Title", typeof(string));
            dt.Columns.Add("Sub Title", typeof(string));
            dt.Columns.Add("Slug Url", typeof(string));
            dt.Columns.Add("Add Context", typeof(string));
            foreach (var knowledgeBase in dataList.data)
            {
                dt.Rows.Add(knowledgeBase.Id, knowledgeBase.Category.CategoryName, knowledgeBase.DocTitle, knowledgeBase.SubTitle, knowledgeBase.SlugUrl, knowledgeBase.AddContent);
            }
            string fileName = "KnowledgeBaseData_" + DateTime.Now.ToShortDateString() + ".xlsx";

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
        public async Task<IActionResult> DeleteKnowledge(int id)
        {
            await _knowledgebaseRepository.DeleteKnowledgeBase(_apiBaseUrl.Value.LmsApiBaseUrl,id);
            var page = 1;
            var size = 5;
            int recsCount = (await _knowledgebaseRepository.GetAllKnowledgeBaseList(_apiBaseUrl.Value.LmsApiBaseUrl)).data.Count();
            if (page < 1)
                page = 1;
            /*ViewBag.GetPrincipalById = TempData["GetPrincipalById"] as string;*/
            var pager = new Pager(recsCount, page, size);
            ViewBag.Pager = pager;
            return View("KnowledgeBaseList", pager);
        }

    }
}
