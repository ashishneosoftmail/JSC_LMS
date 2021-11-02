using JSC_LMS.Application.Features.Subject.Commands.CreateSubject;
using JSC_LMS.Application.Features.Subject.Commands.UpdateSubject;
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
    public class SubjectController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public SubjectController(IMediator mediator, ILogger<SubjectController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpPost(Name = "AddSubject")]
        public async Task<ActionResult> Create([FromBody] CreateSubjectCommand createSubjectCommand)
        {
            var id = await _mediator.Send(createSubjectCommand);
            return Ok(id);
        }

        [HttpPut(Name = "UpdateSubject")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateSubjectCommand updateSubjectCommand)
        {
            var response = await _mediator.Send(updateSubjectCommand);
            return Ok(response);
        }
    }
}
