using JSC_LMS.Application.Features.Common.Categories.Queries.GetCategoriesList;
using JSC_LMS.Application.Features.Common.Cities.Queries.GetCitiesListWithStateId;
using JSC_LMS.Application.Features.Common.Roles.Commands.CreateRole;
using JSC_LMS.Application.Features.Common.Roles.Commands.DeleteRole;
using JSC_LMS.Application.Features.Common.Roles.Commands.UpdateRole;
using JSC_LMS.Application.Features.Common.Roles.Queries.GetRoleById;
using JSC_LMS.Application.Features.Common.Roles.Queries.GetRolesList;
using JSC_LMS.Application.Features.Common.States.Queries.GetStatesList;
using JSC_LMS.Application.Features.Common.ZipCodes.Queries.GetZipcodeListWithCityId;
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
    public class CommonController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public CommonController(IMediator mediator, ILogger<CommonController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        //Roles
        //[Authorize]
        [HttpGet("GetAllRole", Name = "GetAllRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllRoles()
        {
            _logger.LogInformation("GetAllRoles Initiated");
            var dtos = await _mediator.Send(new GetRolesListQuery());
            _logger.LogInformation("GetAllRoles Completed");
            return Ok(dtos);
        }
        /* [HttpGet("{id}", Name = "GetRoleById")]
         public async Task<ActionResult> GetRoleById(int id)
         {
             var getRoleDetailQuery = new GetRoleQuery() { Id = id };
             return Ok(await _mediator.Send(getRoleDetailQuery));
         }*/


        [HttpPost(Name = "AddRole")]
        public async Task<ActionResult> Create([FromBody] CreateRoleCommand createRoleCommand)
        {
            var id = await _mediator.Send(createRoleCommand);
            return Ok(id);
        }
        [HttpPut(Name = "UpdateRole")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateRoleCommand updateRoleCommand)
        {
            var response = await _mediator.Send(updateRoleCommand);
            return Ok(response);
        }
        [HttpDelete("{id}", Name = "DeleteRole")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var deleteRoleCommand = new DeleteRoleCommand() { Id = id };
            await _mediator.Send(deleteRoleCommand);
            return NoContent();
        }

        //Category
        [HttpGet("GetAllCategory", Name = "GetAllCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllCategory()
        {
            _logger.LogInformation("GetAllCategories Initiated");
            var dtos = await _mediator.Send(new GetCategoriesListQuery());
            _logger.LogInformation("GetAllCategories Completed");
            return Ok(dtos);
        }

        //State
        [HttpGet("GetAllState", Name = "GetAllState")]
        public async Task<ActionResult> GetAllStates()
        {
            _logger.LogInformation("GetAllStates Initiated");
            var dtos = await _mediator.Send(new GetStatesListQuery());
            _logger.LogInformation("GetAllStates Completed");
            return Ok(dtos);
        }

        //City
        [HttpGet("{StateId}", Name = "GetCitiesByStateId")]
        public async Task<ActionResult> GetCitiesByStateId(int StateId)
        {
            var getAllCitiesByStateId = new GetCitiesListByStateIdQuery() { StateId = StateId };
            return Ok(await _mediator.Send(getAllCitiesByStateId));
        }

        //Zip
        [HttpGet("/Zip/{CityId}", Name = "GetZipcodesByCityId")]
        public async Task<ActionResult> GetZipcodesByCityId(int CityId)
        {
            var getAllZipByCityId = new GetZipcodeListWithCityIdQuery() { CityId = CityId };
            return Ok(await _mediator.Send(getAllZipByCityId));
        }
    }
}
