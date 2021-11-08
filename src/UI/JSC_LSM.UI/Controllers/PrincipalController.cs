using JSC_LMS.Application.Features.Principal.Commands.CreatePrincipal;
using JSC_LSM.UI.Models;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Controllers
{
    public class PrincipalController : BaseController
    {
        private readonly JSC_LSM.UI.Common.Common _common;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IPrincipalRepository _principalRepository;
        public PrincipalController(JSC_LSM.UI.Common.Common common, ISchoolRepository schoolRepository, IPrincipalRepository principalRepository)
        {
            _common = common;
            _schoolRepository = schoolRepository;
            _principalRepository = principalRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddPrincipal()
        {
            PrincipalModel principalModel = new PrincipalModel();
            principalModel.States = await _common.GetAllStates();
            principalModel.Schools = await _common.GetSchool();
            return View(principalModel);
        }
        public async Task<List<SelectListItem>> GetCityByStateId(int id)
        {
            var cities = await _common.GetAllCityByStateId(id);
            return cities;
        }
        public async Task<List<SelectListItem>> GetZipByCityId(int cityId)
        {
            var zip = await _common.GetAllZipByCityId(cityId);
            return zip;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPrincipal(PrincipalModel principalModel)
        {
            ViewBag.AddPrincipalSuccess = null;
            ViewBag.AddPrincipalError = null;
            principalModel.States = await _common.GetAllStates();
            principalModel.Schools = await _common.GetSchool();
            CreatePrincipalDto createNewPrincipal = new CreatePrincipalDto();
            principalModel.RoleName = "Principal";
            if (ModelState.IsValid)
            {

                createNewPrincipal.SchoolId = principalModel.SchoolId;
                createNewPrincipal.AddressLine1 = principalModel.AddressLine1;
                createNewPrincipal.AddressLine2 = principalModel.AddressLine2;
                createNewPrincipal.Name = principalModel.Name;
                createNewPrincipal.Email = principalModel.Email;
                createNewPrincipal.Mobile = principalModel.Mobile;
                createNewPrincipal.Password = principalModel.Password;
                createNewPrincipal.Username = principalModel.Username;
                createNewPrincipal.CityId = principalModel.CityId;
                createNewPrincipal.StateId = principalModel.StateId;
                createNewPrincipal.ZipId = principalModel.ZipId;
                createNewPrincipal.IsActive = principalModel.IsActive;
                createNewPrincipal.RoleName = principalModel.RoleName;


                PrincipalResponseModel principalResponseModel = null;
                ViewBag.AddPrincipalSuccess = null;
                ViewBag.AddPrincipalError = null;
                ResponseModel responseModel = new ResponseModel();

                principalResponseModel = await _schoolRepository.AddNewPrinicipal(createNewPrincipal);


                if (principalResponseModel.Succeeded)
                {
                    if (principalResponseModel == null && principalResponseModel.data == null)
                    {
                        responseModel.ResponseMessage = principalResponseModel.message;
                        responseModel.IsSuccess = principalResponseModel.Succeeded;
                    }
                    if (principalResponseModel != null)
                    {
                        if (principalResponseModel.data != null)
                        {
                            responseModel.ResponseMessage = principalResponseModel.message;
                            responseModel.IsSuccess = principalResponseModel.Succeeded;
                            ViewBag.AddPrincipalSuccess = "Details Added Successfully";
                            ModelState.Clear();
                            var newPrincipalModel = new PrincipalModel();
                            newPrincipalModel.States = await _common.GetAllStates();
                            newPrincipalModel.Schools = await _common.GetSchool();
                            return View(newPrincipalModel);
                        }
                        else
                        {
                            responseModel.ResponseMessage = principalResponseModel.message;
                            responseModel.IsSuccess = principalResponseModel.Succeeded;
                            ViewBag.AddPrincipalError = principalResponseModel.message;
                            return View(principalModel);
                        }
                    }
                }
                else
                {
                    responseModel.ResponseMessage = principalResponseModel.message;
                    responseModel.IsSuccess = principalResponseModel.Succeeded;
                    ViewBag.AddPrincipalError = principalResponseModel.message;
                }
            }
            return View(principalModel);

        }
        [HttpGet]
        public async Task<IActionResult> PrincipalDetails()
        {

            /* var data = new List<PrincipalDetailsViewModel>();
             if (principalName == null && schoolName == null)
             {
                 var dataList = await _principalRepository.GetAllPrincipalDetails();
                 foreach (var principal in dataList.data)
                 {
                     data.Add(new PrincipalDetailsViewModel()
                     {
                         Id = principal.Id,
                         Name = principal.Name,
                         AddressLine1 = principal.AddressLine1,
                         AddressLine2 = principal.AddressLine2,
                         CityName = principal.City.CityName,
                         StateName = principal.State.StateName,
                         CreatedDate = principal.CreatedDate,
                         Email = principal.Email,
                         IsActive = principal.IsActive,
                         Mobile = principal.Mobile,
                         SchoolName = principal.School.SchoolName,
                         Username = principal.Username,
                         ZipCode = principal.Zip.Zipcode
                     });
                 }
             }
             else
             {
                 data.Clear();
                 var dataList = await _principalRepository.GetPrincipalByFilters(schoolName, principalName, createdDate, isActive);
                 foreach (var principal in dataList.data)
                 {
                     data.Add(new PrincipalDetailsViewModel()
                     {
                         Id = principal.Id,
                         Name = principal.Name,
                         AddressLine1 = principal.AddressLine1,
                         AddressLine2 = principal.AddressLine2,
                         CityName = principal.City.CityName,
                         StateName = principal.State.StateName,
                         CreatedDate = principal.CreatedDate,
                         Email = principal.Email,
                         IsActive = principal.IsActive,
                         Mobile = principal.Mobile,
                         SchoolName = principal.School.SchoolName,
                         Username = principal.Username,
                         ZipCode = principal.Zip.Zipcode
                     });
                 }
             }*/
            var page = 1;
            var size = 5;
            int recsCount = (await _principalRepository.GetAllPrincipalDetails()).data.Count();
            if (page < 1)
                page = 1;

            var pager = new Pager(recsCount, page, size);
            ViewBag.Pager = pager;
            return View(pager);


        }

        [HttpGet]
        public async Task<IEnumerable<PrincipalDetailsViewModel>> GetPrincipalByFilters(string principalName, string schoolName, DateTime createdDate, bool isActive)
        {
            var data = new List<PrincipalDetailsViewModel>();
            var dataList = await _principalRepository.GetPrincipalByFilters(schoolName, principalName, createdDate, isActive);
            if (dataList.data != null)
            {
                foreach (var principal in dataList.data)
                {
                    data.Add(new PrincipalDetailsViewModel()
                    {
                        Id = principal.Id,
                        Name = principal.Name,
                        AddressLine1 = principal.AddressLine1,
                        AddressLine2 = principal.AddressLine2,
                        CityName = principal.City.CityName,
                        StateName = principal.State.StateName,
                        CreatedDate = principal.CreatedDate,
                        Email = principal.Email,
                        IsActive = principal.IsActive,
                        Mobile = principal.Mobile,
                        SchoolName = principal.School.SchoolName,
                        Username = principal.Username,
                        ZipCode = principal.Zip.Zipcode
                    });
                }
            }
            return data;
        }



        [HttpGet]
        public async Task<IEnumerable<PrincipalDetailsViewModel>> GetAllPrincipalDetails()
        {
            var data = new List<PrincipalDetailsViewModel>();

            var dataList = await _principalRepository.GetAllPrincipalDetails();
            foreach (var principal in dataList.data)
            {
                data.Add(new PrincipalDetailsViewModel()
                {
                    Id = principal.Id,
                    Name = principal.Name,
                    AddressLine1 = principal.AddressLine1,
                    AddressLine2 = principal.AddressLine2,
                    CityName = principal.City.CityName,
                    StateName = principal.State.StateName,
                    CreatedDate = principal.CreatedDate,
                    Email = principal.Email,
                    IsActive = principal.IsActive,
                    Mobile = principal.Mobile,
                    SchoolName = principal.School.SchoolName,
                    Username = principal.Username,
                    ZipCode = principal.Zip.Zipcode
                });
            }
            return data;
        }

        [HttpGet]
        public async Task<IEnumerable<PrincipalDetailsViewModel>> GetAllPrincipalDetailsByPagination(int page = 1, int size = 5)
        {
            int recsCount = (await _principalRepository.GetAllPrincipalDetails()).data.Count();
            if (page < 1)
                page = 1;
            var pager = new Pager(recsCount, page, size);

            ViewBag.Pager = pager;
            var data = new List<PrincipalDetailsViewModel>();

            //int recSkip = (page - 1) * size;
            var dataList = await _principalRepository.GetPrincipalByPagination(page, size);

            foreach (var principal in dataList.data.GetPrincipalListPaginationDto)
            {
                data.Add(new PrincipalDetailsViewModel()
                {
                    Id = principal.Id,
                    Name = principal.Name,
                    AddressLine1 = principal.AddressLine1,
                    AddressLine2 = principal.AddressLine2,
                    CityName = principal.City.CityName,
                    StateName = principal.State.StateName,
                    CreatedDate = principal.CreatedDate,
                    Email = principal.Email,
                    IsActive = principal.IsActive,
                    Mobile = principal.Mobile,
                    SchoolName = principal.School.SchoolName,
                    Username = principal.Username,
                    ZipCode = principal.Zip.Zipcode
                });
            }
            return data;
        }
        [HttpGet]
        public async Task<GetPrincipalByIdResponseModel> GetPrincipalById(int Id)
        {

            var principal = await _principalRepository.GetPrincipalById(Id);
            return principal;
        }

        [HttpGet]
        public async Task<List<SelectListItem>> GetAllSchool()
        {
            var schools = await _common.GetSchool();
            return schools;
        }
        [HttpGet]
        public async Task<List<SelectListItem>> GetPrincipalName()
        {
            var data = await _principalRepository.GetAllPrincipalDetails();
            List<SelectListItem> principal = new List<SelectListItem>();
            foreach (var item in data.data)
            {
                principal.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = Convert.ToString(item.Id)
                });
            }
            return principal;
        }

        [HttpGet]
        public IActionResult EditPrincipal()
        {
            return View();
        }
    }
}
