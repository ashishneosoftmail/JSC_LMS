using JSC_LMS.Application.Features.Feedback.Commands.CreateFeedback;
using JSC_LMS.Application.Features.Feedback.Queries.GetFeedbackById;
using JSC_LMS.Application.Features.Feedback.Queries.GetFeedbackList;
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
    public class FeedbackController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public FeedbackController(IMediator mediator, ILogger<SchoolController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
   

        [HttpPost("Add", Name = "AddFeedback")]
        public async Task<ActionResult> Create(CreateFeedbackDto createFeedbackDto)
        {
            var createFeedbackCommand = new CreateFeedbackCommand(createFeedbackDto);
            var id = await _mediator.Send(createFeedbackCommand);
            return Ok(id);
        }



        [HttpGet("{id}", Name = "GetFeedbackById")]
        public async Task<ActionResult> GetFeedbackById(int id)
        {
            var getFeedbackDetailQuery = new GetFeedbackByIdQuery() { Id = id };
            return Ok(await _mediator.Send(getFeedbackDetailQuery));
        }


      
        [HttpGet("GetFeedbackList", Name = "GetFeedbackList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllFeedback()
        {
            _logger.LogInformation("GetAllFeedback Initiated");
            var dtos = await _mediator.Send(new GetAllFeedbackListQuery());
            _logger.LogInformation("GetAllFeedback Completed");
            return Ok(dtos);
        }

        //[HttpGet("Pagination", Name = "GetAllFeedbackListByPagination")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<ActionResult> GetAllFeedbackListByPagination(int _page, int _size)
        //{
        //    _logger.LogInformation("GetAllSection Initiated");
        //    var dtos = await _mediator.Send(new GetAllFeedbackListByPaginationQuery() { page = _page, size = _size });
         
        //    return Ok(dtos);
        //}

     

    }
}
