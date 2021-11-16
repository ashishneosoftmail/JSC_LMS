using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JSC_LMS.Application.Features.Students.Commands.CreateStudent;
using JSC_LMS.Application.Features.Students.Commands.UpdateStudent;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JSC_LMS.Api.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public StudentController(IMediator mediator, ILogger<StudentController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("Add" , Name = "AddStudent")]
        public async Task<ActionResult> Create(CreateStudentDto createStudentDto)
        {
            var createStudentCommand = new CreateStudentCommand(createStudentDto);
            var result = await _mediator.Send(createStudentCommand);
            return Ok(result);
        }

        [HttpPut("Update", Name = "UpdateStudent")]       
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update(UpdateStudentDto updateStudentDto)
        {
            var updateStudentCommand = new UpdateStudentCommand(updateStudentDto);
            var response = await _mediator.Send(updateStudentCommand);
            return Ok(response);
        }
    }
}
