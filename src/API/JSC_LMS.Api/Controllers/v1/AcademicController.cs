using JSC_LMS.Application.Features.Academics.Commands.CreateAcademic;
using JSC_LMS.Application.Features.Academics.Commands.UpdateAcademic;
using JSC_LMS.Application.Features.Academics.Queries.GetAcademicByFilter;
using JSC_LMS.Application.Features.Academics.Queries.GetAcademicById;
using JSC_LMS.Application.Features.Academics.Queries.GetAcademicList;
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

        [HttpPut("UpdateAcademic", Name = "UpdateAcademic")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateKnowledgeBase(UpdateAcademicDto updateAcademicDto)
        {
            var updateAcademicCommand = new UpdateAcademicCommand(updateAcademicDto);
            var response = await _mediator.Send(updateAcademicCommand);
            return Ok(response);
        }

        [HttpGet("all", Name = "GetAllAcademic")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllAcademic()
        {
            _logger.LogInformation("GetAllAcademic Initiated");
            var dtos = await _mediator.Send(new GetAcademicListQuery());
            _logger.LogInformation("GetAllAcademic Completed");
            return Ok(dtos);
        }

        [HttpGet("id", Name = "GetAcademicById")]
        public async Task<ActionResult> GetAcademicById(int id)
        {
            var getAcademicDetailQuery = new GetAcademicByIdQuery() { Id = id };
            return Ok(await _mediator.Send(getAcademicDetailQuery));
        }

        [HttpGet("GetAcademicByFilter", Name = "GetAcademicByFilter")]
        public async Task<ActionResult> GetStudentByFilter(string ClassName, string SchoolName, string SubjectName, string SectionName, string TeacherName, string Type, bool IsActive, DateTime CreatedDate)
        {
            var getAcademicByFilterQuery = new GetAcademicByFilterQuery(ClassName, SchoolName, SubjectName, SectionName, TeacherName,Type, IsActive, CreatedDate);
            return Ok(await _mediator.Send(getAcademicByFilterQuery));
        }
    }
}
