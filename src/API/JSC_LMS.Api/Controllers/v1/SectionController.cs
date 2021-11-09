using JSC_LMS.Application.Features.Section.Commands.CreateSection;
using JSC_LMS.Application.Features.Section.Commands.CreateUpdate;
using JSC_LMS.Application.Features.Section.Queries.GetSectionById;
using JSC_LMS.Application.Features.Section.Queries.GetSectionByPagination;
using JSC_LMS.Application.Features.Section.Queries.GetSectionFilter;
using JSC_LMS.Application.Features.Section.Queries.GetSectionList;
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
    public class SectionController : Controller
    {

        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public SectionController(IMediator mediator, ILogger<SectionController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost(Name = "AddSection")]
        
        public async Task<ActionResult> Create(CreateSectionDto createSectionDto)
        {
            var createSectionCommand = new CreateSectionCommand(createSectionDto);
            var id = await _mediator.Send(createSectionCommand);
            return Ok(id);
        }

        [HttpPut("UpdateSection", Name = "UpdateSection")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
      
        public async Task<ActionResult> UpdateSection(UpdateSectionDto updateSectionDto)
        {
            var updateSectionCommand = new UpdateSectionCommand(updateSectionDto);
            var response = await _mediator.Send(updateSectionCommand);
            return Ok(response);
        }

        [HttpGet("{id}", Name = "GetSectionById")]
        public async Task<ActionResult> GetSectionById(int id)
        {
            var getSectionDetailQuery = new GetSectionByIdQuery() { Id = id };
            return Ok(await _mediator.Send(getSectionDetailQuery));
        }

        [HttpGet("all", Name = "GetAllSection")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllSection()
        {
            _logger.LogInformation("GetAllSection Initiated");
            var dtos = await _mediator.Send(new GetSectionListQuery());
            _logger.LogInformation("GetAllSection Completed");
            return Ok(dtos);
        }

        [HttpGet("Pagination", Name = "GetAllSectionByPagination")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllSectionByPagination(int _page, int _size)
        {
            _logger.LogInformation("GetAllSection Initiated");
            var dtos = await _mediator.Send(new GetSectionByPaginationQuery() { page = _page, size = _size });
            _logger.LogInformation("GetAllSection Completed");
            return Ok(dtos);
        }


        [HttpGet("Filter", Name = "GetSectionByFilter")]
        public async Task<ActionResult> GetSectionFilter(string _className,  string _schoolName, string _sectionName, bool _isActive, DateTime _createdDate)
        {
            var getSectionByFilterQuery = new GetSectionFilterQuery(_schoolName,  _className, _sectionName, _isActive, _createdDate);
            return Ok(await _mediator.Send(getSectionByFilterQuery));
        }
    }
}
