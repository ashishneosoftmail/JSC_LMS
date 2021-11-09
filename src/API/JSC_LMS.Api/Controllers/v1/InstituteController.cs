using JSC_LMS.Api.Utility;
using JSC_LMS.Application.Features.Institutes.Commands.CreateInstitute;
using JSC_LMS.Application.Features.Institutes.Commands.UpdateInstitute;
using JSC_LMS.Application.Features.Institutes.Queries.GetInstituteById;
using JSC_LMS.Application.Features.Institutes.Queries.GetInstituteByPagination;
using JSC_LMS.Application.Features.Institutes.Queries.GetInstituteFilter;
using JSC_LMS.Application.Features.Institutes.Queries.GetInstituteList;
using JSC_LMS.Application.Features.Institutes.Queries.InstituteFileExport.InstituteCsvExport;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#region - developed by Shivani Goswami
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

        /// <summary>
        /// Adds new institute's details : by Shivani Goswami
        /// </summary>
        /// <param name="createInstituteDto"></param>
        /// <returns></returns>
        [HttpPost(Name = "AddInstitute")]
        public async Task<ActionResult> Create(CreateInstituteDto createInstituteDto)
        {
            var createInstituteCommand = new CreateInstituteCommand(createInstituteDto);
            var id = await _mediator.Send(createInstituteCommand);
            return Ok(id);
        }

        /// <summary>
        /// Updates the institute details : by Shivani Goswami
        /// </summary>
        /// <param name="updateInstituteDto"></param>
        /// <returns></returns>
        [HttpPut("UpdateInstitute", Name = "UpdateInstitute")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update(UpdateInstituteDto updateInstituteDto)
        {
            var updateInstituteCommand = new UpdateInstituteCommand(updateInstituteDto);
            var response = await _mediator.Send(updateInstituteCommand);
            return Ok(response);
        }
        
        /// <summary>
        /// Method to get the list of Institute : by Shivani Goswami
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        [HttpGet("all", Name = "GetAllInstitutes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllInstitutes()
        {
            _logger.LogInformation("GetAllInstitutes Initiated");
            var dtos = await _mediator.Send(new GetInstituteListQuery());
            _logger.LogInformation("GetAllInstitutes Completed");
            return Ok(dtos);
        }

        /// <summary>
        /// Method to get Institute details by taking Id as parameter: by Shivani Goswami
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetInstituteById")]
        public async Task<ActionResult> GetInstituteById(int id)
        {
            var getInstituteDetailQuery = new GetInstituteQuery() { Id = id };
            return Ok(await _mediator.Send(getInstituteDetailQuery));
        }

        /// <summary>
        /// Method to get the filtered data for Institute: by Shivani Goswami
        /// </summary>
        /// <param name="InstituteName"></param>
        /// <param name="City"></param>
        /// <param name="State"></param>
        /// <param name="IsActive"></param>
        /// <param name="LicenseExpiry"></param>
        /// <returns></returns>
        [HttpGet("GetInstituteByFilter",Name = "GetInstituteByFilter")]
        public async Task<ActionResult> GetInstituteByFilter(string InstituteName, string City,string State, bool IsActive, DateTime LicenseExpiry)
        {
            var getInstituteByFilterQuery = new GetInstituteFilterQuery(InstituteName, City,State, IsActive, LicenseExpiry);
            return Ok(await _mediator.Send(getInstituteByFilterQuery));
        }

        /// <summary>
        /// Exports the Institute details list in CSV format: by Shivani Goswami
        /// </summary>
        /// <returns></returns>
        [HttpGet("export", Name = "ExportInstitute")]
        [FileResultContentType("text/csv")]
        public async Task<FileResult> ExportInstitute()
        {
            var fileDto = await _mediator.Send(new GetInstituteCsvExportQuery());
            return File(fileDto.Data, fileDto.ContentType, fileDto.InstituteExportFileName);
        }

        /// <summary>
        /// Custom Pagination for institute details' List: by Shivani Goswami
        /// </summary>
        /// <param name="_page"></param>
        /// <param name="_size"></param>
        /// <returns></returns>
        [HttpGet("Pagination", Name = "GetAllInstituteByPagination")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllInstituteByPagination(int _page, int _size)
        {
            _logger.LogInformation("GetAllInstitute Initiated");
            var dtos = await _mediator.Send(new GetInstituteByPaginationQuery() { page = _page, size = _size });
            _logger.LogInformation("GetAllInstitute Completed");
            return Ok(dtos);
        }
       
    }
}
#endregion