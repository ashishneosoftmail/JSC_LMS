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

namespace JSC_LMS.Application.Features.Institutes.Queries.InstituteFileExport.InstituteExcelExport
{
    
    public class GetInstituteExcelExportQueryHandler : IRequestHandler<GetInstituteExcelExportQuery, InstituteExcelExportFileVm>
    {
        private readonly IInstituteRepository _instituteRepository;
       
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetInstituteExcelExportQueryHandler(IMapper mapper, IInstituteRepository instituteRepository, IAuthenticationService authenticationService, ILogger<GetInstituteExcelExportQueryHandler> logger)
        {
            _mapper = mapper;
            _instituteRepository = instituteRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }
        public async Task<InstituteExcelExportFileVm> Handle(GetInstituteExcelExportQuery request, CancellationToken cancellationToken)
        {
            var allInstitute = await _instituteRepository.ListAllAsync();
            List<InstituteExcelExportDto> instituteList = new List<InstituteExcelExportDto>();
            foreach (var institute in allInstitute)
            {
                var user = await _authenticationService.GetUserById(institute.UserId);
                instituteList.Add(new InstituteExcelExportDto()
                {
                    Id = institute.Id,
                    InstituteName = institute.InstituteName,
                    InstituteURL = institute.InstituteURL,
                    LicenseExpiry= (DateTime)institute.LicenseExpiry,
                    ContactPerson=institute.ContactPerson,
                    AddressLine1 = institute.AddressLine1,
                    AddressLine2 = institute.AddressLine2,
                    Mobile = institute.Mobile,
                    Username = user.Username,
                    Email = user.Email,
                    IsActive = institute.IsActive,                   
                    CreatedDate = (DateTime)institute.CreatedDate,
                    CreatedBy = institute.CreatedBy,
                    LastModifiedBy = institute.LastModifiedBy,
                    LastModifiedDate = institute.LastModifiedDate,
                    DeletedBy = institute.DeletedBy,
                    DeletedDate = institute.DeletedDate,
                    City = _mapper.Map<CityDto>(institute.City),
                    State = _mapper.Map<StateDto>(institute.State),
                    Zip = _mapper.Map<ZipDto>(institute.Zip),
                   
                });
            }
            //Creating DataTable  
            DataTable dt = new DataTable();
            //Setiing Table Name  
            dt.TableName = "InstituteData";
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Institute_Name", typeof(string));            
            dt.Columns.Add("Contact_Name", typeof(string));
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
            //dt.Columns.Add("CreatedBy", typeof(int?));
            dt.Columns.Add("CreatedDate", typeof(DateTime));
            //dt.Columns.Add("LastModifiedBy", typeof(int?));
            dt.Columns.Add("LastModifiedDate", typeof(DateTime));
            //dt.Columns.Add("DeletedBy", typeof(int?));
            dt.Columns.Add("DeletedDate", typeof(DateTime));
            dt.Columns.Add("LicenseExpiry", typeof(DateTime));
            dt.Columns.Add("InstituteURL", typeof(string));
            foreach (var _institute in instituteList)
            {
                dt.Rows.Add(_institute.Id, _institute.InstituteName, _institute.ContactPerson, _institute.AddressLine1, _institute.AddressLine2, _institute.Mobile, _institute.Username, _institute.Email, _institute.IsActive ? "Active" : "Inactive", _institute.City.Id, _institute.City.CityName, _institute.State.Id, _institute.State.StateName, _institute.Zip.Id, _institute.Zip.Zipcode, _institute.CreatedDate?.ToShortDateString(), _institute.LastModifiedDate?.ToShortDateString(), _institute.DeletedDate?.ToShortDateString() , _institute.LicenseExpiry.ToShortDateString(), _institute.InstituteURL);
            }
            string fileName = "InstituteData.xlsx";

            using (XLWorkbook wb = new XLWorkbook())
            {
                //Add DataTable in worksheet  
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    //Return xlsx Excel File  
                    /* return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);*/
                    var instituteExportFileDto = new InstituteExcelExportFileVm() { ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Data = stream.ToArray(), InstituteExportFileName = fileName };

                    return instituteExportFileDto;
                }
            }
        }
    }
}
