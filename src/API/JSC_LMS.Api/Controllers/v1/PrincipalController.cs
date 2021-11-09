using JSC_LMS.Application.Features.Principal.Commands.CreatePrincipal;
using JSC_LMS.Application.Features.Principal.Queries.GetPrincipalById;
using JSC_LMS.Application.Features.Principal.Queries.GetPrincipalList;
using JSC_LMS.Application.Features.Principal.Queries.GetPrincipalByFilter;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JSC_LMS.Api.Utility;
using JSC_LMS.Application.Features.Principal.Queries.PrincipalFileExport.PrincipalCsvExport;
using JSC_LMS.Application.Features.Principal.Commands.UpdatePrincipal;
using JSC_LMS.Application.Features.Principal.Queries.GetPrincipalByPagination;

#region-developed By Harsh Chheda
namespace JSC_LMS.Api.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]

    public class PrincipalController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public PrincipalController(IMediator mediator, ILogger<PrincipalController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        /// <summary>
        /// List All the Principal - Done By Harsh Chheda
        /// </summary>
        /// <returns></returns>
        [HttpGet("all", Name = "GetAllPrincipal")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllPrincipal()
        {
            _logger.LogInformation("GetAllPrincipal Initiated");
            var dtos = await _mediator.Send(new GetPrincipalListQuery());
            _logger.LogInformation("GetAllPrincipal Completed");
            return Ok(dtos);
        }
        /// <summary>
        /// List the principal data based on the pagination -Done By Harsh Chheda
        /// </summary>
        /// <param name="_page"></param>
        /// <param name="_size"></param>
        /// <returns></returns>
        [HttpGet("Pagination", Name = "GetAllPrincipalByPagination")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllPrincipalByPagination(int _page, int _size)
        {
            _logger.LogInformation("GetAllPrincipal Initiated");
            var dtos = await _mediator.Send(new GetPrincipalByPaginationQuery() { page = _page, size = _size });
            _logger.LogInformation("GetAllPrincipal Completed");
            return Ok(dtos);
        }
        /// <summary>
        /// Returns the principal data based on the principal id-Done By Harsh Chheda
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetPrincipalById")]
        public async Task<ActionResult> GetPrincipalById(int id)
        {
            var getPrincipalDetailQuery = new GetPrincipalByIdQuery() { Id = id };
            return Ok(await _mediator.Send(getPrincipalDetailQuery));
        }
        /// <summary>
        /// Add the new record for the principal
        /// </summary>
        /// <param name="createPrincipalDto"></param>
        /// <returns></returns>
        [HttpPost(Name = "AddPrincipal")]
        public async Task<ActionResult> Create(CreatePrincipalDto createPrincipalDto)
        {
            var createPrincipalCommand = new CreatePrincipalCommand(createPrincipalDto);
            var result = await _mediator.Send(createPrincipalCommand);
            return Ok(result);
        }
        /// <summary>
        /// Update the data of the principal
        /// </summary>
        /// <param name="updatePrincipalDto"></param>
        /// <returns></returns>
        [HttpPut("UpdatePrincipal", Name = "Update")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update(UpdatePrincipalDto updatePrincipalDto)
        {
            var updatePrincipalCommand = new UpdatePrincipalCommand(updatePrincipalDto);
            var response = await _mediator.Send(updatePrincipalCommand);
            return Ok(response);
        }
        /// <summary>
        /// Returns the principal based on params passed
        /// </summary>
        /// <param name="SchoolName"></param>
        /// <param name="PrincipalName"></param>
        /// <param name="IsActive"></param>
        /// <param name="CreatedDate"></param>
        /// <returns></returns>
        [HttpGet("GetPrincipalByFilter", Name = "GetPrincipalByFilter")]
        public async Task<ActionResult> GetPrincipalByFilter(string SchoolName, string PrincipalName, bool IsActive, DateTime CreatedDate)
        {
            var getPrincipalByFilterQuery = new GetPrincipalByFilterQuery(SchoolName, PrincipalName, IsActive, CreatedDate);
            return Ok(await _mediator.Send(getPrincipalByFilterQuery));
        }
        /// <summary>
        /// Exports all the data into file(csv format)
        /// </summary>
        /// <returns></returns>
        [HttpGet("export", Name = "ExportPrincipal")]
        [FileResultContentType("text/csv")]
        public async Task<FileResult> ExportPrincipal()
        {
            var fileDto = await _mediator.Send(new GetPrincipalCsvExportQuery());

            return File(fileDto.Data, fileDto.ContentType, fileDto.PrincipalExportFileName);
        }

    }
}
#endregion