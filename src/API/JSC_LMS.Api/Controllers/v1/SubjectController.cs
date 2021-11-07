using JSC_LMS.Application.Features.Subject.Commands.CreateSubject;
using JSC_LMS.Application.Features.Subject.Commands.UpdateSubject;
using JSC_LMS.Application.Features.Subject.Queries.GetSubjectById;
using JSC_LMS.Application.Features.Subject.Queries.GetSubjectByPagination;
using JSC_LMS.Application.Features.Subject.Queries.GetSubjectFilter;
using JSC_LMS.Application.Features.Subject.Queries.GetSubjectList;
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
    public class SubjectController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public SubjectController(IMediator mediator, ILogger<SubjectController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpPost(Name = "AddSubject")]
        public async Task<ActionResult> Create([FromBody] CreateSubjectCommand createSubjectCommand)
        {
            var id = await _mediator.Send(createSubjectCommand);
            return Ok(id);
        }

        [HttpPut(Name = "UpdateSubject")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateSubjectCommand updateSubjectCommand)
        {
            var response = await _mediator.Send(updateSubjectCommand);
            return Ok(response);
        }


        [HttpGet("{id}", Name = "GetSubjectById")]
        public async Task<ActionResult> GetSubjectById(int id)
        {
            var getSubjectDetailQuery = new GetSubjectQuery() { Id = id };
            return Ok(await _mediator.Send(getSubjectDetailQuery));
        }

        [HttpGet("all", Name = "GetAllSubject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllSubject()
        {
            _logger.LogInformation("GetAllSubject Initiated");
            var dtos = await _mediator.Send(new GetSubjectListQuery());
            _logger.LogInformation("GetAllSubjects Completed");
            return Ok(dtos);
        }

        [HttpGet("Pagination", Name = "GetAllSubjectByPagination")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllSubjectByPagination(int _page, int _size)
        {
            _logger.LogInformation("GetAllSubject Initiated");
            var dtos = await _mediator.Send(new GetSubjectByPaginationQuery() { page = _page, size = _size });
            _logger.LogInformation("GetAllSubject Completed");
            return Ok(dtos);
        }


        [HttpGet("Filter", Name = "GetSubjectByFilter")]
        public async Task<ActionResult> GetSubjectFilter( string _ClassName, string _SubjectName, string _SchoolName, string _SectionName, bool _IsActive, DateTime _CreatedDate)
        {
            var getSubjectByFilterQuery = new GetSubjectFilterQuery( _SchoolName, _SubjectName,_ClassName, _SectionName, _IsActive, _CreatedDate);
            return Ok(await _mediator.Send(getSubjectByFilterQuery));
        }

    }
}
