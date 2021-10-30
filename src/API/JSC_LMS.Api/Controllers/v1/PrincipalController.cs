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
        [HttpGet("{id}", Name = "GetPrincipalById")]
        public async Task<ActionResult> GetPrincipalById(int id)
        {
            var getPrincipalDetailQuery = new GetPrincipalByIdQuery() { Id = id };
            return Ok(await _mediator.Send(getPrincipalDetailQuery));
        }
        [HttpGet(Name = "GetPrincipalByFilter")]
        public async Task<ActionResult> GetPrincipalByFilter(string SchoolName, string PrincipalName, bool IsActive, DateTime CreatedDate)
        {
            var getPrincipalByFilterQuery = new GetPrincipalByFilterQuery(SchoolName, PrincipalName, IsActive, CreatedDate);
            return Ok(await _mediator.Send(getPrincipalByFilterQuery));
        }
        [HttpPost(Name = "AddPrincipal")]
        public async Task<ActionResult> Create([FromBody] CreatePrincipalCommand createPrincipalCommand)
        {
            var result = await _mediator.Send(createPrincipalCommand);
            return Ok(result);
        }
    }
}
