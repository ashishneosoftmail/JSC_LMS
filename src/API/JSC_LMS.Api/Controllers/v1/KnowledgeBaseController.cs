using JSC_LMS.Application.Features.KnowledgeBase.Commands.CreateKnowledgeBase;
using JSC_LMS.Application.Features.KnowledgeBase.Commands.DeleteKnowledgeBase;
using JSC_LMS.Application.Features.KnowledgeBase.Commands.UpdateKnowledgeBase;
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
    public class KnowledgeBaseController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public KnowledgeBaseController(IMediator mediator, ILogger<KnowledgeBaseController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost(Name = "AddKnowledgeBase")]
        public async Task<ActionResult> AddKnowledgeBase(CreateKnowledgeBaseDto createKnowledgeBaseDto)
        {
            var createKnowledegeBaseCommand = new CreateKnowledgeBaseCommand(createKnowledgeBaseDto);
            var result = await _mediator.Send(createKnowledegeBaseCommand);
            return Ok(result);
        }
        [HttpPut("UpdateKnowledgeBase", Name = "UpdateKnowledgeBase")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateKnowledgeBase(UpdateKnowledgeBaseDto updateKnowledgeBaseDto)
        {
            var updateKnowledgebaseCommand = new UpdateKnowledgeBaseCommand(updateKnowledgeBaseDto);
            var response = await _mediator.Send(updateKnowledgebaseCommand);
            return Ok(response);
        }
        [HttpDelete("{id}", Name = "DeleteKnowledgeBase")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteKnowledgeBase(int id)
        {
            var deleteKnowledgeBaseCommand = new DeleteKnowledgeBaseCommand() { Id = id };
            await _mediator.Send(deleteKnowledgeBaseCommand);
            return NoContent();
        }
    }
}
