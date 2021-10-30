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

namespace JSC_LMS.Application.Features.Institutes.Queries.InstituteFileExport.InstituteCsvExport
{

    public class GetInstituteCsvExportQueryHandler : IRequestHandler<GetInstituteCsvExportQuery, InstituteCsvExportFileVm>
    {
        private readonly IInstituteRepository _instituteRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ICsvExporter _csvExporter;

        public GetInstituteCsvExportQueryHandler(IMapper mapper, IInstituteRepository instituteRepository, IAuthenticationService authenticationService, ILogger<GetInstituteCsvExportQueryHandler> logger, ICsvExporter csvExporter)
        {
            _mapper = mapper;
            _instituteRepository = instituteRepository;
            _logger = logger;
            _authenticationService = authenticationService;
            _csvExporter = csvExporter;
        }

        public async Task<InstituteCsvExportFileVm> Handle(GetInstituteCsvExportQuery request, CancellationToken cancellationToken)
        {
            var allInstitute = await _instituteRepository.ListAllAsync();
            List<InstituteCsvExportDto> instituteList = new List<InstituteCsvExportDto>();
            foreach (var institute in allInstitute)
            {
                var user = await _authenticationService.GetUserById(institute.UserId);
                instituteList.Add(new InstituteCsvExportDto()
                {
                    Id = institute.Id,
                    InstituteName = institute.InstituteName,
                    AddressLine1 = institute.AddressLine1,
                    AddressLine2 = institute.AddressLine2,
                    Mobile = institute.Mobile,
                    Username = user.Username,
                    Email = user.Email,
                    IsActive = institute.IsActive,
                    ContactPerson = institute.ContactPerson,
                    CreatedDate = (DateTime)institute.CreatedDate,
                    City = _mapper.Map<CityDto>(institute.City),
                    State = _mapper.Map<StateDto>(institute.State),
                    Zip = _mapper.Map<ZipDto>(institute.Zip),
                    LicenseExpiry = institute.LicenseExpiry,
                    InstituteURL=institute.InstituteURL
                  
                });
            }

            var fileData = _csvExporter.ExportInstituteToCsv(instituteList);

            var eventExportFileDto = new InstituteCsvExportFileVm() { ContentType = "text/csv", Data = fileData, InstituteExportFileName = $"InstituteDetails.csv" };

            return eventExportFileDto;
        }
    }
}
