﻿using JSC_LMS.Application.Features.School.Commands.CreateSchool;
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
    public class SchoolController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public SchoolController(IMediator mediator, ILogger<SchoolController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost(Name = "AddSchool")]
        public async Task<ActionResult> Create([FromBody] CreateSchoolCommand createSchoolCommand)
        {
            var id = await _mediator.Send(createSchoolCommand);
            return Ok(id);
        }
    }
}