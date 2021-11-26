using JSC_LMS.Api.Utility;
using JSC_LMS.Application.Features.School.Commands.CreateSchool;
using JSC_LMS.Application.Features.School.Commands.UpdateSchool;
using JSC_LMS.Application.Features.School.Queries.GetSchoolByFilter;
using JSC_LMS.Application.Features.School.Queries.GetSchoolById;
using JSC_LMS.Application.Features.School.Queries.GetSchoolByPagination;
using JSC_LMS.Application.Features.School.Queries.GetSchoolList;
using JSC_LMS.Application.Features.School.Queries.SchoolFileExport.SchoolCsvExport;
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
    public class SchoolController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public SchoolController(IMediator mediator, ILogger<SchoolController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        #region-Developed By Harsh Chheda
        /// <summary>
        /// Adds the new school - Developed By Harsh Chheda
        /// </summary>
        /// <param name="createSchoolCommand"></param>
        /// <returns></returns>
        [HttpPost(Name = "AddSchool")]
        public async Task<ActionResult> Create([FromBody] CreateSchoolCommand createSchoolCommand)
        {
            var id = await _mediator.Send(createSchoolCommand);
            return Ok(id);
        }
        /// <summary>
        /// Updates the school data - Developed By Harsh Chheda
        /// </summary>
        /// <param name="updateSchoolCommand"></param>
        /// <returns></returns>
        [HttpPut("UpdateSchool", Name = "UpdateSchool")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateSchoolCommand updateSchoolCommand)
        {
            var response = await _mediator.Send(updateSchoolCommand);
            return Ok(response);
        }
        #endregion
        [HttpGet("GetAllSchool", Name = "GetAllSchool")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllSchool()
        {
            _logger.LogInformation("GetAllSchool Initiated");
            var dtos = await _mediator.Send(new GetSchoolListQuery());
            _logger.LogInformation("GetAllSchool Completed");
            return Ok(dtos);
        }

        [HttpGet("{id}", Name = "GetSchoolById")]
        public async Task<ActionResult> GetSchoolByIdQuery(int id)
        {
            var getSchoolDetailQuery = new GetSchoolByIdQuery() { Id = id };
            return Ok(await _mediator.Send(getSchoolDetailQuery));
        }

        [HttpGet("export", Name = "ExportSchool")]
        [FileResultContentType("text/csv")]
        public async Task<FileResult> ExportPrincipal()
        {
            var fileDto = await _mediator.Send(new GetSchoolCsvExportQuery());

            return File(fileDto.Data, fileDto.ContentType, fileDto.SchoolExportFileName);
        }

        [HttpGet("GetSchoolByFilter", Name = "GetSchoolByFilter")]
        public async Task<ActionResult> GetSchoolByFilter(string SchoolName, string InstituteName, string City, string State, bool IsActive, DateTime CreatedDate)
        {
            var getPrincipalByFilterQuery = new GetSchoolByFilterQuery(SchoolName, City,State, InstituteName, IsActive, CreatedDate);
            return Ok(await _mediator.Send(getPrincipalByFilterQuery));
        }

        [HttpGet("Pagination", Name = "GetSchoolByPagination")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetSchoolByPagination(int _page, int _size)
        {
            _logger.LogInformation("GetAllSchool Initiated");
            var dtos = await _mediator.Send(new GetSchoolByPaginationQuery() { page = _page, size = _size });
            _logger.LogInformation("GetAllSchool Completed");
            return Ok(dtos);
        }
    }
}
