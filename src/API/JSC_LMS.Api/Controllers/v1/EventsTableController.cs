using JSC_LMS.Application.Features.EventsFeature.Commands.CreateEvents;
using JSC_LMS.Application.Features.EventsFeature.Commands.UpdateEvents;
using JSC_LMS.Application.Features.EventsFeature.Queries.GetAllEventsList;
using JSC_LMS.Application.Features.EventsFeature.Queries.GetAllEventsListByPagination;
using JSC_LMS.Application.Features.EventsFeature.Queries.GetAllEventsListBySchool;
using JSC_LMS.Application.Features.EventsFeature.Queries.GetAllEventsListBySchoolPagination;
using JSC_LMS.Application.Features.EventsFeature.Queries.GetEventsById;
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
    public class EventsTableController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public EventsTableController(IMediator mediator, ILogger<EventsTableController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost("Add" , Name = "AddEventDetails")]
        public async Task<ActionResult> AddCircular(CreateEventsDto createEventsDto)
        {
            var createEventsCommand = new CreateEventsCommand(createEventsDto);
            var result = await _mediator.Send(createEventsCommand);
            return Ok(result);
        }
        [HttpPut("Update", Name = "UpdateEvents")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateCircular(UpdateEventsDto updateEventsDto)
        {
            var updateEventsCommand = new UpdateEventsCommand(updateEventsDto);
            var response = await _mediator.Send(updateEventsCommand);
            return Ok(response);
        }
        [HttpGet("{id}", Name = "GetEventsById")]
        public async Task<ActionResult> GetEventsById(int id)
        {
            var getEventsByIdQuery = new GetEventsByIdQuery() { Id = id };
            return Ok(await _mediator.Send(getEventsByIdQuery));
        }

        [HttpGet("all", Name = "GetAllEventsData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllEventsData()
        {
            var dtos = await _mediator.Send(new GetAllEventsListQuery());
            return Ok(dtos);
        }

        [HttpGet("GetAllEventsBySchool", Name = "GetAllEventsBySchool")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllEventsBySchool(int schoolid)
        {
            var dtos = await _mediator.Send(new GetAllEventsListBySchoolQuery() { SchoolId = schoolid });
            return Ok(dtos);
        }
        [HttpGet("Pagination", Name = "GetAllEventsListByPagination")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllEventsListByPagination(int _page, int _size)
        {
            var dtos = await _mediator.Send(new GetAllEventsListByPaginationQuery() { page = _page, size = _size });
            return Ok(dtos);
        }
        [HttpGet("GetEventsListBySchoolPagination", Name = "GetEventsListBySchoolPagination")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetEventsListBySchoolPagination(int _page, int _size, int _schoolId)
        {
            var dtos = await _mediator.Send(new GetAllEventsListBySchoolPaginationQuery() { page = _page, size = _size, SchoolId = _schoolId });
            return Ok(dtos);
        }
    }
}
