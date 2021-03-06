using JSC_LMS.Application.Features.Gallary.Commands.DeleteAllImage;
using JSC_LMS.Application.Features.Gallary.Commands.DeleteImage;
using JSC_LMS.Application.Features.Gallary.Commands.UploadImage;
using JSC_LMS.Application.Features.Gallary.Queries.GetAllGallaryFilter;
using JSC_LMS.Application.Features.Gallary.Queries.GetAllGallaryList;
using JSC_LMS.Application.Features.Gallary.Queries.GetGallaryById;
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
    public class GallaryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public GallaryController(IMediator mediator, ILogger<GallaryController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost("AddGallary",Name = "AddGallary")]
        public async Task<ActionResult> AddGallary(UploadImageDto uploadImageDto)
        {
            var uploadImageCommand = new UploadImageCommand(uploadImageDto);
            var result = await _mediator.Send(uploadImageCommand);
            return Ok(result);
        }

        [HttpDelete("delete", Name = "DeleteImage")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteImage(int id)
        {
            var deleteImageCommand = new DeleteImageCommand() { Id = id };
            await _mediator.Send(deleteImageCommand);
            return NoContent();
        }
        [HttpGet("all", Name = "GetAllGallary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllGallary()
        {
            var dtos = await _mediator.Send(new GetAllGallaryQuery());
            return Ok(dtos);
        }

        [HttpGet("id", Name = "GetGallaryById")]
        public async Task<ActionResult> GetGallaryById(int id)
        {
            var getGallaryByIdQuery = new GetGallaryByIdQuery() { Id = id };
            return Ok(await _mediator.Send(getGallaryByIdQuery));
        }

        [HttpGet("GetGallaryByFilter", Name = "GetGallaryByFilter")]
        public async Task<ActionResult> GetGallaryByFilter(int SchoolId )
        {
            var getAllGallaryByFilterQuery = new GetAllGallaryByFilterQuery(SchoolId);
            return Ok(await _mediator.Send(getAllGallaryByFilterQuery));
        }

        [HttpDelete("deleteAll", Name = "DeleteAllImage")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteAllImage()
        {
            var deleteAllImageCommand = new DeleteAllImageCommand() ;
            await _mediator.Send(deleteAllImageCommand);
            return NoContent();
        }
    }
}
