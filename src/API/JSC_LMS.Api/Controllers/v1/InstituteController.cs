﻿using MediatR;
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
    public class InstituteController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public InstituteController(IMediator mediator, ILogger<InstituteController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
    }
}