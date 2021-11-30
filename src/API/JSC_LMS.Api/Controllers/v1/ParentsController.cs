using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using JSC_LMS.Application.Features.ParentsFeature.Commands.CreateParents;
using JSC_LMS.Application.Features.ParentsFeature.Commands.UpdateParents;
using JSC_LMS.Application.Features.ParentsFeature.Queries.GetParentsList;
using JSC_LMS.Application.Features.ParentsFeature.Queries.GetParentsById;
using JSC_LMS.Application.Features.ParentsFeature.Queries.GetParentsByPagination;
using JSC_LMS.Application.Features.ParentsFeature.Queries.GetParentsByFilter;
using JSC_LMS.Application.Features.ParentsFeature.Queries.GetParentByUserId;
using JSC_LMS.Application.Features.ParentsFeature.Queries.GetAllParentsListBySchool;
using JSC_LMS.Application.Features.ParentsFeature.Queries.GetParentsListByPaginationBySchool;

namespace JSC_LMS.Api.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ParentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public ParentsController(IMediator mediator, ILogger<ParentsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("Add", Name = "AddParents")]
        public async Task<ActionResult> Create(CreateParentsDto createParentsDto)
        {
            var createParentsCommand = new CreateParentsCommand(createParentsDto);
            var result = await _mediator.Send(createParentsCommand);
            return Ok(result);
        }

        [HttpPut("Update", Name = "UpdateParents")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update(UpdateParentsDto updateParentsDto)
        {
            var updateParentsCommand = new UpdateParentsCommand(updateParentsDto);
            var response = await _mediator.Send(updateParentsCommand);
            return Ok(response);
        }

        [HttpGet("all", Name = "GetAllParents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllParents()
        {
            _logger.LogInformation("GetAllParents Initiated");
            var dtos = await _mediator.Send(new GetParentsListQuery());
            _logger.LogInformation("GetAllParents Completed");
            return Ok(dtos);
        }

        [HttpGet("id", Name = "GetParentsById")]
        public async Task<ActionResult> GetParentsById(int id)
        {
            var getParentsDetailQuery = new GetParentsByIdQuery() { Id = id };
            return Ok(await _mediator.Send(getParentsDetailQuery));
        }

        [HttpGet("Pagination", Name = "GetAllParentsByPagination")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllParentsByPagination(int _page, int _size)
        {
            _logger.LogInformation("GetAllParents Initiated");
            var dtos = await _mediator.Send(new GetParentsByPaginationQuery() { page = _page, size = _size });
            _logger.LogInformation("GetAllParents Completed");
            return Ok(dtos);
        }

        [HttpGet("GetParentsByFilter", Name = "GetParentsByFilter")]
        public async Task<ActionResult> GetParentsByFilter(int SchoolId ,string ClassName, string SectionName, string StudentName,string ParentName, bool IsActive, DateTime CreatedDate)
        {
            var getParentsByFilterQuery = new GetParentsByFilterQuery(SchoolId ,ClassName, SectionName, StudentName,ParentName, IsActive, CreatedDate);
            return Ok(await _mediator.Send(getParentsByFilterQuery));
        }

        [HttpGet("GetParentByUserId", Name = "GetParentByUserId")]
        public async Task<ActionResult> GetParentByUserId(string UserId)
        {
            var getParentByUserIdQuery = new GetParentByUserIdQuery() { UserId = UserId };
            return Ok(await _mediator.Send(getParentByUserIdQuery));
        }

        [HttpGet("GetAllParentsBySchool", Name = "GetAllParentsBySchool")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllParentsBySchool(int schoolid)
        {
            var dtos = await _mediator.Send(new GetAllParentsListBySchoolQuery() { SchoolId = schoolid });
            return Ok(dtos);
        }


        [HttpGet("GetParentsListBySchoolPagination", Name = "GetParentsListBySchoolPagination")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetParentsListBySchoolPagination(int _page, int _size, int _schoolId)
        {
            var dtos = await _mediator.Send(new GetParentsListByPaginationBySchoolQuery() { page = _page, size = _size, SchoolId = _schoolId });
            return Ok(dtos);
        }
    }
}
