using JSC_LMS.Application.Features.FeedbackTitle.Queries.GetFeedbackTitleList;
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
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class FeedbackTitleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public FeedbackTitleController(IMediator mediator, ILogger<FeedbackTitleController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpGet("GetFeedbackTitleList", Name = "GetFeedbackTitleList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllFeedbackTitle()
        {
            _logger.LogInformation("GetAllFeedback Initiated");
            var dtos = await _mediator.Send(new GetFeedbackTitleListQuery());
            _logger.LogInformation("GetAllFeedback Completed");
            return Ok(dtos);
        }


    }
}
