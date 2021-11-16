using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JSC_LMS.Application.Features.Students.Commands.CreateStudent;
using JSC_LMS.Application.Features.Students.Commands.UpdateStudent;
using JSC_LMS.Application.Features.Students.Queries.GetStudentByFilter;
using JSC_LMS.Application.Features.Students.Queries.GetStudentById;
using JSC_LMS.Application.Features.Students.Queries.GetStudentByPagination;
using JSC_LMS.Application.Features.Students.Queries.GetStudentList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JSC_LMS.Api.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public StudentController(IMediator mediator, ILogger<StudentController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("Add" , Name = "AddStudent")]
        public async Task<ActionResult> Create(CreateStudentDto createStudentDto)
        {
            var createStudentCommand = new CreateStudentCommand(createStudentDto);
            var result = await _mediator.Send(createStudentCommand);
            return Ok(result);
        }

        [HttpPut("Update", Name = "UpdateStudent")]       
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update(UpdateStudentDto updateStudentDto)
        {
            var updateStudentCommand = new UpdateStudentCommand(updateStudentDto);
            var response = await _mediator.Send(updateStudentCommand);
            return Ok(response);
        }

        [HttpGet("all", Name = "GetAllStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllStudent()
        {
            _logger.LogInformation("GetAllStudent Initiated");
            var dtos = await _mediator.Send(new GetStudentListQuery());
            _logger.LogInformation("GetAllStudent Completed");
            return Ok(dtos);
        }

        [HttpGet("id", Name = "GetStudentById")]
        public async Task<ActionResult> GetStudentById(int id)
        {
            var getStudentDetailQuery = new GetStudentByIdQuery() { Id = id };
            return Ok(await _mediator.Send(getStudentDetailQuery));
        }

        [HttpGet("Pagination", Name = "GetAllStudentByPagination")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllStudentByPagination(int _page, int _size)
        {
            _logger.LogInformation("GetAllStudent Initiated");
            var dtos = await _mediator.Send(new GetStudentByPaginationQuery() { page = _page, size = _size });
            _logger.LogInformation("GetAllStudent Completed");
            return Ok(dtos);
        }

        [HttpGet("GetStudentByFilter", Name = "GetStudentByFilter")]
        public async Task<ActionResult> GetStudentByFilter(string ClassName, string SectionName,string StudentName, bool IsActive, DateTime CreatedDate)
        {
            var getStudentByFilterQuery = new GetStudentByFilterQuery(ClassName, SectionName, StudentName, IsActive, CreatedDate);
            return Ok(await _mediator.Send(getStudentByFilterQuery));
        }
    }
}
