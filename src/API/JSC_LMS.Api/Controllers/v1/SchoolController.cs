using JSC_LMS.Application.Features.School.Commands.CreateSchool;
using JSC_LMS.Application.Features.School.Commands.UpdateSchool;
using JSC_LMS.Application.Features.School.Queries.GetSchoolList;
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
        [HttpPost(Name = "AddSchool")]
        public async Task<ActionResult> Create([FromBody] CreateSchoolCommand createSchoolCommand)
        {
            var id = await _mediator.Send(createSchoolCommand);
            return Ok(id);
        }
        [HttpPut(Name = "UpdateSchool")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateSchoolCommand updateSchoolCommand)
        {
            var response = await _mediator.Send(updateSchoolCommand);
            return Ok(response);
        }
        [HttpGet("GetAllSchool", Name = "GetAllSchool")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllSchool()
        {
            _logger.LogInformation("GetAllRoles Initiated");
            var dtos = await _mediator.Send(new GetSchoolListQuery());
            _logger.LogInformation("GetAllRoles Completed");
            return Ok(dtos);
        }
    }
}
