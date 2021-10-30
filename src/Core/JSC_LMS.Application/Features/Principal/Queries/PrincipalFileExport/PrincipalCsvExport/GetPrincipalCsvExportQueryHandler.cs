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

namespace JSC_LMS.Application.Features.Principal.Queries.PrincipalFileExport.PrincipalCsvExport
{
    public class GetPrincipalCsvExportQueryHandler : IRequestHandler<GetPrincipalCsvExportQuery, PrincipalCsvExportFileVm>
    {
        private readonly IPrincipalRepository _principalRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ICsvExporter _csvExporter;

        public GetPrincipalCsvExportQueryHandler(IMapper mapper, IPrincipalRepository principalRepository, ISchoolRepository schoolRepository, IAuthenticationService authenticationService, ILogger<GetPrincipalCsvExportQueryHandler> logger, ICsvExporter csvExporter)
        {
            _mapper = mapper;
            _principalRepository = principalRepository;
            _schoolRepository = schoolRepository;
            _logger = logger;
            _authenticationService = authenticationService;
            _csvExporter = csvExporter;
        }

        public async Task<PrincipalCsvExportFileVm> Handle(GetPrincipalCsvExportQuery request, CancellationToken cancellationToken)
        {
            var allPrincipal = await _principalRepository.ListAllAsync();
            List<PrincipalCsvExportDto> principalList = new List<PrincipalCsvExportDto>();
            foreach (var principal in allPrincipal)
            {
                var user = await _authenticationService.GetUserById(principal.UserId);
                principalList.Add(new PrincipalCsvExportDto()
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

            var fileData = _csvExporter.ExportPrincipalToCsv(principalList);

            var eventExportFileDto = new PrincipalCsvExportFileVm() { ContentType = "text/csv", Data = fileData, PrincipalExportFileName = $"Principal.csv" };

            return eventExportFileDto;
        }
    }
}
