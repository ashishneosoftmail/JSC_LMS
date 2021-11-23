using JSC_LMS.Application.Features.FAQ.Commands.CreateFAQ;
using JSC_LMS.Application.Features.FAQ.Commands.DeleteFAQ;
using JSC_LMS.Application.Features.FAQ.Commands.UpdateFAQ;
using JSC_LMS.Application.Features.FAQ.Queries.GetFAQByFilter;
using JSC_LMS.Application.Features.FAQ.Queries.GetFAQById;
using JSC_LMS.Application.Features.FAQ.Queries.GetFAQByPagination;
using JSC_LMS.Application.Features.FAQ.Queries.GetFAQList;
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
    public class FAQController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public FAQController(IMediator mediator, ILogger<FAQController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
    
        [HttpDelete("{id}", Name = "DeleteFAQ")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteFAQ(int id)
        {
            var deleteFAQCommand = new DeleteFAQCommand() { Id = id };
            await _mediator.Send(deleteFAQCommand);
            return NoContent();
        }

        [HttpPut("UpdateFAQ", Name = "UpdateFAQ")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateFAQ(UpdateFAQDto updateKnowledgeBaseDto)
        {
            var updateKnowledgebaseCommand = new UpdateFAQCommand(updateKnowledgeBaseDto);
            var response = await _mediator.Send(updateKnowledgebaseCommand);
            return Ok(response);
        }

        [HttpGet("all", Name = "GetAllFAQ")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllFAQ()
        {
            var dtos = await _mediator.Send(new GetFAQListQuery());
            return Ok(dtos);
        }

        [HttpGet("{id}", Name = "GetFAQById")]
        public async Task<ActionResult> GetFAQById(int id)
        {
            var getKnowledgeBaseDetailQuery = new GetFAQByIdQuery() { Id = id };
            return Ok(await _mediator.Send(getKnowledgeBaseDetailQuery));
        }

        [HttpGet("Pagination", Name = "GetAllFAQByPagination")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllFAQByPagination(int _page, int _size)
        {
            var dtos = await _mediator.Send(new GetFAQByPaginationQuery() { page = _page, size = _size });
            return Ok(dtos);
        }

        [HttpGet("GetFAQByFilter", Name = "GetFAQByFilter")]
        public async Task<ActionResult> GetFAQByFilter(string _FAQTitle, bool _IsActive, string _Category)
        {
            var getKnowledgeBaseByFilterQuery = new GetFAQByFilterQuery(_FAQTitle, _IsActive, _Category);
            return Ok(await _mediator.Send(getKnowledgeBaseByFilterQuery));
        }


        [HttpPost(Name = "AddFAQ")]
        public async Task<ActionResult> AddFAQ(CreateFAQDto createKnowledgeBaseDto)
        {
            var createKnowledegeBaseCommand = new CreateFAQCommand(createKnowledgeBaseDto);
            var result = await _mediator.Send(createKnowledegeBaseCommand);
            return Ok(result);
        }
    }
}
