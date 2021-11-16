using JSC_LMS.Application.Features.Academics.Commands.CreateAcademic;
using MediatR;
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
    public class AcademicController :ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public AcademicController(IMediator mediator, ILogger<AcademicController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost(Name = "AddAcademic")]
        public async Task<ActionResult> AddAcademic(CreateAcademicDto createAcademicDto)
        {
            var createAcademicCommand = new CreateAcademicCommand(createAcademicDto);
            var result = await _mediator.Send(createAcademicCommand);
            return Ok(result);
        }

    }
}
