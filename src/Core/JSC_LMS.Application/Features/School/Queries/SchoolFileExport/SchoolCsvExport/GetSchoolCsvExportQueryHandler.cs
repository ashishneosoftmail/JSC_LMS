using AutoMapper;
using JSC_LMS.Application.CommonDtos;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Infrastructure;
using JSC_LMS.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.School.Queries.SchoolFileExport.SchoolCsvExport
{
    public class GetSchoolCsvExportQueryHandler : IRequestHandler<GetSchoolCsvExportQuery, SchoolCsvExportFileVm>
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly IInstituteRepository _instituteRepository;
       
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ICsvExporter _csvExporter;

        public GetSchoolCsvExportQueryHandler(IMapper mapper, ISchoolRepository schoolRepository, IInstituteRepository instituteRepository, IAuthenticationService authenticationService, ILogger<GetSchoolCsvExportQueryHandler> logger, ICsvExporter csvExporter)
        {
            _mapper = mapper;
            _schoolRepository = schoolRepository;
            _instituteRepository = instituteRepository;
            _logger = logger;
            _authenticationService = authenticationService;
            _csvExporter = csvExporter;
        }
        public async Task<SchoolCsvExportFileVm> Handle(GetSchoolCsvExportQuery request, CancellationToken cancellationToken)
        {

            var allSchool = await _schoolRepository.ListAllAsync();
            List<SchoolCsvExportDto> schoolList = new List<SchoolCsvExportDto>();
            foreach (var school in allSchool)
            {
                
                schoolList.Add(new SchoolCsvExportDto()

                {
                    Id = school.Id,
                    AddressLine1 = school.Address1,
                    AddressLine2 = school.Address2,
                    SchoolName = school.SchoolName,
                    Mobile = school.Mobile,
                    ContactPerson = school.ContactPerson,
                    Email = school.Email,
                    IsActive = school.IsActive,

                    City = _mapper.Map<CityDto>(school.City),
                    State = _mapper.Map<StateDto>(school.State),
                    Zip = _mapper.Map<ZipDto>(school.Zip),
                    Institute = new InstituteExportDto()
                    {
                        Id = school.InstituteId,
                        InstituteName = (await _instituteRepository.GetByIdAsync(school.InstituteId)).InstituteName
                    }
                });
            }

            var fileData = _csvExporter.ExportSchoolToCsv(schoolList);

            var eventExportFileDto = new SchoolCsvExportFileVm() { ContentType = "text/csv", Data = (byte[])fileData, SchoolExportFileName = $"School.csv" };

            return eventExportFileDto;
        }
    }
}
