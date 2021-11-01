using JSC_LMS.Api.Utility;
using JSC_LMS.Application.Features.Institutes.Commands.CreateInstitute;
using JSC_LMS.Application.Features.Institutes.Commands.UpdateInstitute;
using JSC_LMS.Application.Features.Institutes.Queries.GetInstituteById;
using JSC_LMS.Application.Features.Institutes.Queries.GetInstituteFilter;
using JSC_LMS.Application.Features.Institutes.Queries.GetInstituteList;
using JSC_LMS.Application.Features.Institutes.Queries.InstituteFileExport.InstituteCsvExport;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LMS.Api.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class InstituteController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public InstituteController(IMediator mediator, ILogger<InstituteController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost(Name = "AddInstitute")]
        public async Task<ActionResult> Create(CreateInstituteDto createInstituteDto)
        {
            var createInstituteCommand = new CreateInstituteCommand(createInstituteDto);
            var id = await _mediator.Send(createInstituteCommand);
            return Ok(id);
        }
        [HttpPut(Name = "UpdateInstitute")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateInstituteCommand updateInstituteCommand)
        {
            var response = await _mediator.Send(updateInstituteCommand);
            return Ok(response);
        }

        //[Authorize]
        [HttpGet("all", Name = "GetAllInstitutes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllInstitutes()
        {
            _logger.LogInformation("GetAllInstitutes Initiated");
            var dtos = await _mediator.Send(new GetInstituteListQuery());
            _logger.LogInformation("GetAllInstitutes Completed");
            return Ok(dtos);
        }

        [HttpGet("{id}", Name = "GetInstituteById")]
        public async Task<ActionResult> GetInstituteById(int id)
        {
            var getInstituteDetailQuery = new GetInstituteQuery() { Id = id };
            return Ok(await _mediator.Send(getInstituteDetailQuery));
        }

        [HttpGet(Name = "GetInstituteByFilter")]
        public async Task<ActionResult> GetInstituteByFilter(string InstituteName, string City,string State, bool IsActive, DateTime LicenseExpiry)
        {
            var getInstituteByFilterQuery = new GetInstituteFilterQuery(InstituteName, City,State, IsActive, LicenseExpiry);
            return Ok(await _mediator.Send(getInstituteByFilterQuery));
        }

        [HttpGet("export", Name = "ExportInstitute")]
        [FileResultContentType("text/csv")]
        public async Task<FileResult> ExportInstitute()
        {
            var fileDto = await _mediator.Send(new GetInstituteCsvExportQuery());

            return File(fileDto.Data, fileDto.ContentType, fileDto.InstituteExportFileName);
        }
    }
}
