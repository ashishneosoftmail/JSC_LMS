﻿using JSC_LMS.Application.Features.Principal.Commands.CreatePrincipal;
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
using JSC_LMS.Application.Features.Principal.Queries.PrincipalFileExport.PrincipalExcelExport;

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

        [HttpGet("all", Name = "GetAllPrincipal")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllPrincipal()
        {
            _logger.LogInformation("GetAllPrincipal Initiated");
            var dtos = await _mediator.Send(new GetPrincipalListQuery());
            _logger.LogInformation("GetAllPrincipal Completed");
            return Ok(dtos);
        }
        [HttpGet("Pagination", Name = "GetAllPrincipalByPagination")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllPrincipalByPagination(int _page, int _size)
        {
            _logger.LogInformation("GetAllPrincipal Initiated");
            var dtos = await _mediator.Send(new GetPrincipalByPaginationQuery() { page = _page, size = _size });
            _logger.LogInformation("GetAllPrincipal Completed");
            return Ok(dtos);
        }
        [HttpGet("{id}", Name = "GetPrincipalById")]
        public async Task<ActionResult> GetPrincipalById(int id)
        {
            var getPrincipalDetailQuery = new GetPrincipalByIdQuery() { Id = id };
            return Ok(await _mediator.Send(getPrincipalDetailQuery));
        }

        [HttpPost(Name = "AddPrincipal")]
        public async Task<ActionResult> Create(CreatePrincipalDto createPrincipalDto)
        {
            var createPrincipalCommand = new CreatePrincipalCommand(createPrincipalDto);
            var result = await _mediator.Send(createPrincipalCommand);
            return Ok(result);
        }
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
        [HttpGet("GetPrincipalByFilter", Name = "GetPrincipalByFilter")]
        public async Task<ActionResult> GetPrincipalByFilter(string SchoolName, string PrincipalName, bool IsActive, DateTime CreatedDate)
        {
            var getPrincipalByFilterQuery = new GetPrincipalByFilterQuery(SchoolName, PrincipalName, IsActive, CreatedDate);
            return Ok(await _mediator.Send(getPrincipalByFilterQuery));
        }
        [HttpGet("export", Name = "ExportPrincipal")]
        [FileResultContentType("text/csv")]
        public async Task<FileResult> ExportPrincipal()
        {
            var fileDto = await _mediator.Send(new GetPrincipalCsvExportQuery());

            return File(fileDto.Data, fileDto.ContentType, fileDto.PrincipalExportFileName);
        }
        [HttpGet("exportToExcel", Name = "ExportPrincipalviaExcel")]
        [FileResultContentType("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        public async Task<FileResult> ExportPrincipalviaExcel()
        {
            var fileDto = await _mediator.Send(new GetPrincipalExcelExportQuery());

            return File(fileDto.Data, fileDto.ContentType, fileDto.PrincipalExportFileName);
        }
    }
}
