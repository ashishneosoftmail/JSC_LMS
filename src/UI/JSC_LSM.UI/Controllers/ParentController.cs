using JSC_LSM.UI.Models;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Controllers
{
    public class ParentController : Controller
    {
        private readonly ICircularRepository _circularRepository;
        private readonly IParentsRepository _parentRepository;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ParentController(ICircularRepository circularRepository, IParentsRepository parentRepository, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _circularRepository = circularRepository;
            _parentRepository = parentRepository;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ManageCircular(int page = 1, int size = 5)
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var principal = await _parentRepository.GetParentByUserId(userId);
            int recsCount = (await _circularRepository.GetAllCircularBySchoolList(principal.data.SchoolId)).data.Count();
            if (page < 1)
                page = 1;
            var pager = new Pager(recsCount, page, size);
            ViewBag.Pager = pager;
            ManageCircularModel model = new ManageCircularModel();
            model.Pager = pager;

            var paginationData = await _circularRepository.GetCircularListBySchoolPagination(page, size, principal.data.SchoolId);
            List<CircularPagination> pagedData = new List<CircularPagination>();
            foreach (var data in paginationData.data)
            {
                if (data.Status)
                {
                    pagedData.Add(new CircularPagination()
                    {
                        Id = data.Id,
                        CircularTitle = data.CircularTitle,
                        Description = data.Description,
                        SchoolData = new JSC_LMS.Application.Features.Circulars.Queries.GetCircularListByPagination.SchoolDto() { Id = data.SchoolData.Id, SchoolName = data.SchoolData.SchoolName },
                        Status = data.Status,
                        CreatedDate = data.CreatedDate,

                    });
                }

            }
            model.CircularListPagination = pagedData;
            return View(model);
        }
        [HttpGet]
        public async Task<GetCircularByIdResponseModel> ViewCircular(int Id)
        {
            var circular = await _circularRepository.GetCircularById(Id);
            return circular;
        }
        [HttpGet]
        public IActionResult ViewFileData(string fileName)
        {
            ViewCircularFileModel model = new ViewCircularFileModel();
            model.FileName = fileName;

            var FilePath = _configuration["Circulars"];
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath + FilePath);
            model.FilePath = uploadsFolder + fileName;
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> SearchCircular(string circularTitle, string description, DateTime date)
        {
            var userId = Convert.ToString(Request.Cookies["Id"]);
            var principal = await _parentRepository.GetParentByUserId(userId);
            List<CircularPagination> data = new List<CircularPagination>();
            var dataList = await _circularRepository.GetAllCircularListByFilterSchoolAndCreatedDate(circularTitle, description, date, principal.data.SchoolId);
            if (dataList.data != null)
            {
                foreach (var d in dataList.data)
                {
                    if (d.Status)
                    {
                        data.Add(new CircularPagination()
                        {
                            Id = d.Id,
                            CircularTitle = d.CircularTitle,
                            Description = d.Description,
                            SchoolData = new JSC_LMS.Application.Features.Circulars.Queries.GetCircularListByPagination.SchoolDto() { Id = d.SchoolData.Id, SchoolName = d.SchoolData.SchoolName },
                            Status = d.Status,
                            CreatedDate = d.CreatedDate,


                        });
                    }
                }
            }
            else
            {

            }
            ManageCircularModel model = new ManageCircularModel();
            model.CircularListPagination = data;
            if (dataList.data.Count() == 0)
            {
                model.Pager = new Pager(1, 1, 1);
            }
            else
            {
                model.Pager = new Pager(dataList.data.Count(), 1, dataList.data.Count());
            }
            ViewBag.Pager = model.Pager;
            return View("ManageCircular", model);
        }

    }
}
