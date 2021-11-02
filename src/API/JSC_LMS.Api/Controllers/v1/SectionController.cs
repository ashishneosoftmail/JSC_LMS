using JSC_LMS.Application.Features.Section.Commands.CreateSection;
using JSC_LMS.Application.Features.Section.Commands.CreateUpdate;
using JSC_LMS.Application.Features.Section.Queries.GetSectionById;
using JSC_LMS.Application.Features.Section.Queries.GetSectionList;
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
    public class SectionController : Controller
    {

        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public SectionController(IMediator mediator, ILogger<SectionController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost(Name = "AddSection")]
        public async Task<ActionResult> Create([FromBody] CreateSectionCommand createSectionCommand)
        {
            var id = await _mediator.Send(createSectionCommand);
            return Ok(id);
        }

        [HttpPut(Name = "UpdateSection")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateSectionCommand updateSectionCommand)
        {
            var response = await _mediator.Send(updateSectionCommand);
            return Ok(response);
        }

        [HttpGet("{id}", Name = "GetSectionById")]
        public async Task<ActionResult> GetSectionById(int id)
        {
            var getSectionDetailQuery = new GetSectionByIdQuery() { Id = id };
            return Ok(await _mediator.Send(getSectionDetailQuery));
        }

        [HttpGet("all", Name = "GetAllSection")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllSection()
        {
            _logger.LogInformation("GetAllSection Initiated");
            var dtos = await _mediator.Send(new GetSectionListQuery());
            _logger.LogInformation("GetAllSection Completed");
            return Ok(dtos);
        }


    }
}
