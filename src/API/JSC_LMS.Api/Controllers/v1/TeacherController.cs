using JSC_LMS.Application.Features.Teachers.Commands.CreateTeacher;
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
    public class TeacherController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public TeacherController(IMediator mediator, ILogger<TeacherController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost(Name = "AddTeacher")]
        public async Task<ActionResult> Create([FromBody] CreateTeacherCommand createTeacherCommand)
        {
            var id = await _mediator.Send(createTeacherCommand);
            return Ok(id);
        }

    }
}
