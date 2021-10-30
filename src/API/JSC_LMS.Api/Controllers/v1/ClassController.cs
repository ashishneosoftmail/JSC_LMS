using JSC_LMS.Application.Features.Class.Commands.CreateClass;
using JSC_LMS.Application.Features.Class.Commands.UpdateClass;
using JSC_LMS.Application.Features.Class.Queries.GetClassById;
using JSC_LMS.Application.Features.Class.Queries.GetClassList;
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


        [HttpGet("all", Name = "GetAllClass")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllClass()
        {
            _logger.LogInformation("GetAllClass Initiated");
            var dtos = await _mediator.Send(new GetClassListQuery());
            _logger.LogInformation("GetAllClass Completed");
            return Ok(dtos);
        }

        [HttpGet("{id}", Name = "GetClassById")]
        public async Task<ActionResult> GetClassById(int id)
        {
            var getClassDetailQuery = new GetClassByIdQuery() { Id = id };
            return Ok(await _mediator.Send(getClassDetailQuery));
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
