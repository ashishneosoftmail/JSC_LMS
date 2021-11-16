﻿using JSC_LMS.Application.Features.Academics.Commands.CreateAcademic;
using JSC_LMS.Application.Features.Academics.Commands.UpdateAcademic;
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


    }
}
