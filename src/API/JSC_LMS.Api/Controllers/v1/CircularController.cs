using JSC_LMS.Application.Features.Circulars.Commands.CreateCircular;
using JSC_LMS.Application.Features.Circulars.Commands.DeleteCircular;
using JSC_LMS.Application.Features.Circulars.Commands.UpdateCircular;
using JSC_LMS.Application.Features.Circulars.Queries.GetAllCircularByFilter;
using JSC_LMS.Application.Features.Circulars.Queries.GetAllCircularByFilterAndSchool;
using JSC_LMS.Application.Features.Circulars.Queries.GetAllCircularList;
using JSC_LMS.Application.Features.Circulars.Queries.GetAllCircularListBySchool;
using JSC_LMS.Application.Features.Circulars.Queries.GetCircularById;
using JSC_LMS.Application.Features.Circulars.Queries.GetCircularListByPagination;
using JSC_LMS.Application.Features.Circulars.Queries.GetCircularListByPaginationBySchool;
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
    public class CircularController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public CircularController(IMediator mediator, ILogger<CircularController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost(Name = "AddCircular")]
        public async Task<ActionResult> AddCircular(CreateCircularDto createCircularDto)
        {
            var createCircularCommand = new CreateCircularCommand(createCircularDto);
            var result = await _mediator.Send(createCircularCommand);
            return Ok(result);
        }
        [HttpPut("UpdateCircular", Name = "UpdateCircular")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateCircular(UpdateCircularDto updateCircularDto)
        {
            var updateCircularCommand = new UpdateCircularCommand(updateCircularDto);
            var response = await _mediator.Send(updateCircularCommand);
            return Ok(response);
        }
        [HttpDelete("{id}", Name = "DeleteCircular")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteCircular(int id)
        {
            var deleteCircularCommand = new DeleteCircularCommand() { Id = id };
            await _mediator.Send(deleteCircularCommand);
            return NoContent();
        }
        [HttpGet("all", Name = "GetAllCircular")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllCircular()
        {
            var dtos = await _mediator.Send(new GetAllCircularQuery());
            return Ok(dtos);
        }
        [HttpGet("{id}", Name = "GetCircularById")]
        public async Task<ActionResult> GetCircularById(int id)
        {
            var getCircularByIdQuery = new GetCircularByIdQuery() { Id = id };
            return Ok(await _mediator.Send(getCircularByIdQuery));
        }
        [HttpGet("Pagination", Name = "GetAllCircularListByPagination")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllCircularListByPagination(int _page, int _size)
        {
            var dtos = await _mediator.Send(new GetAllCircularListByPaginationQuery() { page = _page, size = _size });
            return Ok(dtos);
        }
        [HttpGet("GetCircularByFilter", Name = "GetCircularByFilter")]
        public async Task<ActionResult> GetKnowledgeBaseByFilter(string _CircularTitle, string _Description, bool _Status)
        {
            var getKnowledgeBaseByFilterQuery = new GetAllCircularByFiltersQuery(_CircularTitle, _Description, _Status);
            return Ok(await _mediator.Send(getKnowledgeBaseByFilterQuery));
        }

        [HttpGet("GetCircularListBySchoolPagination", Name = "GetCircularListBySchoolPagination")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetCircularListBySchoolPagination(int _page, int _size, int _schoolId)
        {
            var dtos = await _mediator.Send(new GetCircularListByPaginationBySchoolQuery() { page = _page, size = _size, SchoolId = _schoolId });
            return Ok(dtos);
        }

        [HttpGet("GetAllCircularBySchool", Name = "GetAllCircularBySchool")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllCircularBySchool(int schoolid)
        {
            var dtos = await _mediator.Send(new GetAllCircularListBySchoolQuery() { SchoolId = schoolid });
            return Ok(dtos);
        }
        [HttpGet("GetCircularByFilterAndSchool", Name = "GetCircularByFilterAndSchool")]
        public async Task<ActionResult> GetCircularByFilterAndSchool(string _CircularTitle, string _Description, bool _Status, int schoolid)
        {
            var getAllCircularByFilterAndSchoolQuery = new GetAllCircularByFilterAndSchoolQuery(_CircularTitle, _Description, _Status, schoolid);
            return Ok(await _mediator.Send(getAllCircularByFilterAndSchoolQuery));
        }
    }
}
