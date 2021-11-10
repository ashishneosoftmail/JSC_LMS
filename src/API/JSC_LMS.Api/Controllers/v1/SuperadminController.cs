using JSC_LMS.Application.Features.Superadmin.Commands.UpdateSuperadmin;
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
    public class SuperadminController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public SuperadminController(IMediator mediator, ILogger<SuperadminController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPut("UpdateSuperadmin", Name = "UpdateSuperadmin")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateSuperadmin(UpdateSuperadminDto updateSuperadminDto)
        {
            var updateSuperadminCommand = new UpdateSuperadminCommand(updateSuperadminDto);
            var response = await _mediator.Send(updateSuperadminCommand);
            return Ok(response);
        }
    }
}
