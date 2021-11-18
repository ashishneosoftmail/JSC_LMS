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

    }
}
