using JSC_LMS.Application.Features.Class.Commands.CreateClass;
using JSC_LMS.Application.Features.Class.Commands.UpdateClass;
using JSC_LMS.Application.Features.Class.Queries.GetClassByFilter;
using JSC_LMS.Application.Features.Class.Queries.GetClassById;
using JSC_LMS.Application.Features.Class.Queries.GetClassByPagination;
using JSC_LMS.Application.Features.Class.Queries.GetClassList;
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
    public class ClassController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public ClassController(IMediator mediator, ILogger<ClassController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpGet("GetAllClass", Name = "GetAllClass")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllClass()
        {
            _logger.LogInformation("GetAllClass Initiated");
            var dtos = await _mediator.Send(new GetClassListQuery());
            _logger.LogInformation("GetAllClass Completed");
            return Ok(dtos);
        }

        [HttpGet("Pagination", Name = "GetAllClassByPagination")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllClassByPagination(int _page, int _size)
        {
            _logger.LogInformation("GetAllClass Initiated");
            var dtos = await _mediator.Send(new GetClassByPaginationQuery() { page = _page, size = _size });
            _logger.LogInformation("GetAllClass Completed");
            return Ok(dtos);
        }


        [HttpGet("{id}", Name = "GetClassById")]
        public async Task<ActionResult> GetClassById(int id)
        {
            var getClassDetailQuery = new GetClassByIdQuery() { Id = id };
            return Ok(await _mediator.Send(getClassDetailQuery));
        }


        [HttpGet("GetClassByFilter", Name = "GetClassByFilter")]
        public async Task<ActionResult> GetClassByFilter(string SchoolName, string ClassName, bool IsActive, DateTime CreatedDate)
        {
            var getClassByFilterQuery = new GetClassByFilterQuery(SchoolName, ClassName, IsActive, CreatedDate);
            return Ok(await _mediator.Send(getClassByFilterQuery));
        }




        [HttpPost(Name = "AddClass")]
        public async Task<ActionResult> Create(CreateClassDto createClassDto)
        {
            var createClassCommand = new CreateClassCommand(createClassDto);
            var id = await _mediator.Send(createClassCommand);
            return Ok(id);
        }

        [HttpPut("UpdateClass", Name = "UpdateClass")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateClass(UpdateClassDto updateClassDto)
        {
            var updateClassCommand = new UpdateClassCommand(updateClassDto);
            var response = await _mediator.Send(updateClassCommand);
            return Ok(response);
        }
    }
}
