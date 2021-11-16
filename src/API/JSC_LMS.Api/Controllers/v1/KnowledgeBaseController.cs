using JSC_LMS.Application.Features.KnowledgeBase.Commands.CreateKnowledgeBase;
using JSC_LMS.Application.Features.KnowledgeBase.Commands.DeleteKnowledgeBase;
using JSC_LMS.Application.Features.KnowledgeBase.Commands.UpdateKnowledgeBase;
using JSC_LMS.Application.Features.KnowledgeBase.Queries.GetKnowledgeBaseByFilter;
using JSC_LMS.Application.Features.KnowledgeBase.Queries.GetKnowledgeBaseById;
using JSC_LMS.Application.Features.KnowledgeBase.Queries.GetKnowledgeBaseByPagination;
using JSC_LMS.Application.Features.KnowledgeBase.Queries.GetKnowledgeBaseList;
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

        [HttpGet("all", Name = "GetAllKnowledgeBase")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllKnowledgeBase()
        {
            var dtos = await _mediator.Send(new GetKnowledgeBaseListQuery());
            return Ok(dtos);
        }
        [HttpGet("{id}", Name = "GetKnowledgeBaseById")]
        public async Task<ActionResult> GetKnowledgeBaseById(int id)
        {
            var getKnowledgeBaseDetailQuery = new GetKnowledgeBaseByIdQuery() { Id = id };
            return Ok(await _mediator.Send(getKnowledgeBaseDetailQuery));
        }
        [HttpGet("Pagination", Name = "GetAllKnowledgeBaseByPagination")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllKnowledgeBaseByPaginations(int _page, int _size)
        {
            var dtos = await _mediator.Send(new GetKnowledgeBaseByPaginationQuery() { page = _page, size = _size });
            return Ok(dtos);
        }
        [HttpGet("GetKnowledgeBaseByFilter", Name = "GetKnowledgeBaseByFilter")]
        public async Task<ActionResult> GetKnowledgeBaseByFilter(string _DocTitle, string _Subtitle, string _Category)
        {
            var getKnowledgeBaseByFilterQuery = new GetKnowledgeBaseByFilterQuery(_DocTitle, _Subtitle, _Category);
            return Ok(await _mediator.Send(getKnowledgeBaseByFilterQuery));
        }
    }
}
