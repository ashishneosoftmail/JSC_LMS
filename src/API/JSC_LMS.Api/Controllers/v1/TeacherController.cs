using JSC_LMS.Api.Utility;
using JSC_LMS.Application.Features.Teachers.Commands.CreateTeacher;
using JSC_LMS.Application.Features.Teachers.Commands.UpdateTeacher;
using JSC_LMS.Application.Features.Teachers.Queries.GetTeacherById;
using JSC_LMS.Application.Features.Teachers.Queries.GetTeacherByPagination;
using JSC_LMS.Application.Features.Teachers.Queries.GetTeacherByUserId;
using JSC_LMS.Application.Features.Teachers.Queries.GetTeacherFilter;
using JSC_LMS.Application.Features.Teachers.Queries.GetTeacherList;
using JSC_LMS.Application.Features.Teachers.Queries.TeacherFileExport.TeacherCsvExport;
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
    public class TeacherController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public TeacherController(IMediator mediator, ILogger<TeacherController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost(Name ="AddTeacher")]
        public async Task<ActionResult> Create(CreateTeacherDto createTeacherDto)
        {
            var createTeacherCommand = new CreateTeacherCommand(createTeacherDto);
            var id = await _mediator.Send(createTeacherCommand);
            return Ok(id);
        }
        [HttpPut("UpdateTeacher" , Name = "UpdateTeacher")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update(UpdateTeacherDto updateTeacherDto)
        {
            var updateTeacherCommand = new UpdateTeacherCommand(updateTeacherDto);
            var response = await _mediator.Send(updateTeacherCommand);
            return Ok(response);
        }

        [HttpGet("{id}", Name = "GetTeacherById")]
        public async Task<ActionResult> GetTeacherById(int id)
        {
            var getTeacherDetailQuery = new GetTeacherQuery() { Id = id };
            return Ok(await _mediator.Send(getTeacherDetailQuery));
        }

        [HttpGet("all", Name = "GetAllTeacher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllTeacher()
        {
            _logger.LogInformation("GetAllTeacher Initiated");
            var dtos = await _mediator.Send(new GetTeacherListQuery());
            _logger.LogInformation("GetAllTeachers Completed");
            return Ok(dtos);
        }

       
       [HttpGet("Filter",Name = "GetTeacherByFilter")]
        public async Task<ActionResult> GetTeacherByFilter(string _schoolName, string _ClassName, string _SectionName, string _SubjectName, string _TeacherName, bool _IsActive, DateTime _CreatedDate)
        {
            var getTeachereByFilterQuery = new GetTeacherFilterQuery(_schoolName, _ClassName, _SectionName,_SubjectName, _TeacherName,  _IsActive, _CreatedDate);
            return Ok(await _mediator.Send(getTeachereByFilterQuery));
        }

        [HttpGet("export", Name = "ExportTeacher")]
        [FileResultContentType("text/csv")]
        public async Task<FileResult> ExportTeacher()
        {
            var fileDto = await _mediator.Send(new GetTeacherCsvExportQuery());

            return File(fileDto.Data, fileDto.ContentType, fileDto.TeacherExportFileName);
        }

        /// <summary>
        /// Custom Pagination for Teacher list : by Shivani Goswami
        /// </summary>
        /// <param name="_page"></param>
        /// <param name="_size"></param>
        /// <returns></returns>

        [HttpGet("Pagination", Name = "GetAllTeacherByPagination")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllTeacherByPagination(int _page, int _size)
        {
            _logger.LogInformation("GetAllTeacher Initiated");
            var dtos = await _mediator.Send(new GetTeacherByPaginationQuery() { page = _page, size = _size });
            _logger.LogInformation("GetAllTeacher Completed");
            return Ok(dtos);
        }

        [HttpGet("GetTeacherByUserId", Name = "GetTeacherByUserId")]
        public async Task<ActionResult> GetTeacherByUserId(string UserId)
        {
            var getTeacherByUserIdQuery = new GetTeacherByUserIdQuery() { UserId = UserId };
            return Ok(await _mediator.Send(getTeacherByUserIdQuery));
        }
    }
}
