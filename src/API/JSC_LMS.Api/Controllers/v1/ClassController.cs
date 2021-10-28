using JSC_LMS.Application.Features.Class.Commands.CreateClass;
using JSC_LMS.Application.Features.Class.Commands.UpdateClass;
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
    public class ClassController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public ClassController(IMediator mediator, ILogger<ClassController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost(Name = "AddClass")]
        public async Task<ActionResult> Create([FromBody] CreateClassCommand createClassCommand)
        {
            var id = await _mediator.Send(createClassCommand);
            return Ok(id);
        }
       
        [HttpPut(Name = "UpdateClass")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateClassCommand updateClassCommand)
        {
            var response = await _mediator.Send(updateClassCommand);
            return Ok(response);
        }
    }
}
