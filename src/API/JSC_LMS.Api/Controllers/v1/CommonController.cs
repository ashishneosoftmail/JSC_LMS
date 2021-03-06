using JSC_LMS.Application.Features.Common.Categories.Commands;
using JSC_LMS.Application.Features.Common.Categories.Queries.GetCategoriesList;
using JSC_LMS.Application.Features.Common.Cities.Queries.GetCitiesListWithStateId;
using JSC_LMS.Application.Features.Common.Roles.Commands.CreateRole;
using JSC_LMS.Application.Features.Common.Roles.Commands.DeleteRole;
using JSC_LMS.Application.Features.Common.Roles.Commands.UpdateRole;
using JSC_LMS.Application.Features.Common.Roles.Queries.GetRoleById;
using JSC_LMS.Application.Features.Common.Roles.Queries.GetRolesList;
using JSC_LMS.Application.Features.Common.States.Queries.GetStatesList;
using JSC_LMS.Application.Features.Common.Users.Queries.GetUsersList;
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
    #region-Developed By Harsh Chheda
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
        /// <summary>
        /// Returns all the roles - developed by harsh chheda
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllRole", Name = "GetAllRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllRoles()
        {
            _logger.LogInformation("GetAllRoles Initiated");
            var dtos = await _mediator.Send(new GetRolesListQuery());
            _logger.LogInformation("GetAllRoles Completed");
            return Ok(dtos);
        }

        [HttpGet("GetAllUsers", Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllUsers()
        {
            _logger.LogInformation("GetAllUsers Initiated");
            var dtos = await _mediator.Send(new GetUsersListQuery());
            _logger.LogInformation("GetAllUsers Completed");
            return Ok(dtos);
        }
        /* [HttpGet("{id}", Name = "GetRoleById")]
         public async Task<ActionResult> GetRoleById(int id)
         {
             var getRoleDetailQuery = new GetRoleQuery() { Id = id };
             return Ok(await _mediator.Send(getRoleDetailQuery));
         }*/
        [HttpPost("AddCategory", Name = "AddCategories")]
        public async Task<ActionResult> AddCategories(string categoryName, bool isActive)
        {
            var createCategoryCommand = new CreateCategoryCommand()
            {
                CategoryName = categoryName,
                IsActive = isActive
            };
            var id = await _mediator.Send(createCategoryCommand);
            return Ok(id);
        }
        /// <summary>
        /// Adds the role into the database - developed by harsh chheda
        /// </summary>
        /// <param name="createRoleCommand"></param>
        /// <returns></returns>
        [HttpPost(Name = "AddRole")]
        public async Task<ActionResult> Create([FromBody] CreateRoleCommand createRoleCommand)
        {
            var id = await _mediator.Send(createRoleCommand);
            return Ok(id);
        }
        /// <summary>
        /// Updates the role based on the role id -developed by harsh chheda
        /// </summary>
        /// <param name="updateRoleCommand"></param>
        /// <returns></returns>
        [HttpPut(Name = "UpdateRole")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateRoleCommand updateRoleCommand)
        {
            var response = await _mediator.Send(updateRoleCommand);
            return Ok(response);
        }
        /// <summary>
        /// delete the role based on the role id -developed by harsh chheda
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// returns all the category - developed by harsh chheda
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// returns all the state - developed by harsh chheda
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllState", Name = "GetAllState")]
        public async Task<ActionResult> GetAllStates()
        {
            _logger.LogInformation("GetAllStates Initiated");
            var dtos = await _mediator.Send(new GetStatesListQuery());
            _logger.LogInformation("GetAllStates Completed");
            return Ok(dtos);
        }

        //City
        /// <summary>
        /// returns all the city by the state id - developed by harsh chheda
        /// </summary>
        /// <param name="StateId"></param>
        /// <returns></returns>
        [HttpGet("{StateId}", Name = "GetCitiesByStateId")]
        public async Task<ActionResult> GetCitiesByStateId(int StateId)
        {
            var getAllCitiesByStateId = new GetCitiesListByStateIdQuery() { StateId = StateId };
            return Ok(await _mediator.Send(getAllCitiesByStateId));
        }

        //Zip
        /// <summary>
        /// Returns all the zip code by the city id - developed by harsh chheda
        /// </summary>
        /// <param name="CityId"></param>
        /// <returns></returns>
        [HttpGet("/Zip/{CityId}", Name = "GetZipcodesByCityId")]
        public async Task<ActionResult> GetZipcodesByCityId(int CityId)
        {
            var getAllZipByCityId = new GetZipcodeListWithCityIdQuery() { CityId = CityId };
            return Ok(await _mediator.Send(getAllZipByCityId));
        }
    }
    #endregion
}
