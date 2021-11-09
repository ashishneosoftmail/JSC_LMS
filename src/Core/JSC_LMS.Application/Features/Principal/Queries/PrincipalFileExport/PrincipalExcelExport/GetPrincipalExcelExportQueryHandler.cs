using AutoMapper;
using ClosedXML.Excel;
using JSC_LMS.Application.CommonDtos;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Principal.Queries.PrincipalFileExport.PrincipalExcelExport
{
    public class GetPrincipalExcelExportQueryHandler : IRequestHandler<GetPrincipalExcelExportQuery, PrincipalExcelExportFileVm>
    {
        private readonly IPrincipalRepository _principalRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetPrincipalExcelExportQueryHandler(IMapper mapper, IPrincipalRepository principalRepository, ISchoolRepository schoolRepository, IAuthenticationService authenticationService, ILogger<GetPrincipalExcelExportQueryHandler> logger)
        {
            _mapper = mapper;
            _principalRepository = principalRepository;
            _schoolRepository = schoolRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }
        public async Task<PrincipalExcelExportFileVm> Handle(GetPrincipalExcelExportQuery request, CancellationToken cancellationToken)
        {
            var allPrincipal = await _principalRepository.ListAllAsync();
            List<PrincipalExcelExportDto> principalList = new List<PrincipalExcelExportDto>();
            foreach (var principal in allPrincipal)
            {
                var user = await _authenticationService.GetUserById(principal.UserId);
                principalList.Add(new PrincipalExcelExportDto()
                {
                    Id = principal.Id,
                    AddressLine1 = principal.AddressLine1,
                    AddressLine2 = principal.AddressLine2,
                    Mobile = principal.Mobile,
                    Username = user.Username,
                    Email = user.Email,
                    IsActive = principal.IsActive,
                    Name = principal.Name,
                    CreatedDate = (DateTime)principal.CreatedDate,

                    CreatedBy = principal.CreatedBy,
                    LastModifiedBy = principal.LastModifiedBy,
                    LastModifiedDate = principal.LastModifiedDate,
                    DeletedBy = principal.DeletedBy,
                    DeletedDate = principal.DeletedDate,
                    City = _mapper.Map<CityDto>(principal.City),
                    State = _mapper.Map<StateDto>(principal.State),
                    Zip = _mapper.Map<ZipDto>(principal.Zip),
                    School = new SchoolExportDto()
                    {
                        Id = principal.SchoolId,
                        SchoolName = (await _schoolRepository.GetByIdAsync(principal.SchoolId)).SchoolName
                    }
                });
            }
            //Creating DataTable  
            DataTable dt = new DataTable();
            //Setiing Table Name  
            dt.TableName = "PrincipalData";
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("AddressLine1", typeof(string));
            dt.Columns.Add("AddressLine2", typeof(string));
            dt.Columns.Add("Mobile", typeof(string));
            dt.Columns.Add("Username", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("IsActive", typeof(string));
            dt.Columns.Add("City_Id", typeof(int));
            dt.Columns.Add("City_Name", typeof(string));
            dt.Columns.Add("State_Id", typeof(int));
            dt.Columns.Add("State_Name", typeof(string));
            dt.Columns.Add("Zip_Id", typeof(int));
            dt.Columns.Add("ZipCode", typeof(string));
            dt.Columns.Add("School_Id", typeof(int));
            dt.Columns.Add("School_Name", typeof(string));
            //dt.Columns.Add("CreatedBy", typeof(int?));
            dt.Columns.Add("CreatedDate", typeof(DateTime));
            //dt.Columns.Add("LastModifiedBy", typeof(int?));
            dt.Columns.Add("LastModifiedDate", typeof(DateTime));
            //dt.Columns.Add("DeletedBy", typeof(int?));
            dt.Columns.Add("DeletedDate", typeof(DateTime));
            foreach (var _principal in principalList)
            {
                dt.Rows.Add(_principal.Id, _principal.Name, _principal.AddressLine1, _principal.AddressLine2, _principal.Mobile, _principal.Username, _principal.Email, _principal.IsActive ? "Active" : "Inactive", _principal.City.Id, _principal.City.CityName, _principal.State.Id, _principal.State.StateName, _principal.Zip.Id, _principal.Zip.Zipcode, _principal.School.Id, _principal.School.SchoolName,  _principal.CreatedDate?.ToShortDateString(),_principal.LastModifiedDate?.ToShortDateString(), _principal.DeletedDate?.ToShortDateString());
            }
            string fileName = "PrincipalData.xlsx";

            using (XLWorkbook wb = new XLWorkbook())
            {
                //Add DataTable in worksheet  
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    //Return xlsx Excel File  
                    /* return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);*/
                    var principalExportFileDto = new PrincipalExcelExportFileVm() { ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Data = stream.ToArray(), PrincipalExportFileName = fileName };

                    return principalExportFileDto;
                }
            }
        }
    }
}
