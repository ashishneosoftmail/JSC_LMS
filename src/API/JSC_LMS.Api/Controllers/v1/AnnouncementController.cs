using JSC_LMS.Application.Features.Announcement.Commands.CreateAnnouncement;
using JSC_LMS.Application.Features.Announcement.Commands.UpdateAnnouncement;
using JSC_LMS.Application.Features.Announcement.Queries.GetAllAnnouncementListBySchool;
using JSC_LMS.Application.Features.Announcement.Queries.GetAllAnnouncementListBySchoolClassSection;
using JSC_LMS.Application.Features.Announcement.Queries.GetAllAnnouncementListBySchoolClassSectionPagination;
using JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementbyFilter;
using JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementById;
using JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementByPagination;
using JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementList;
using JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementListByPaginationBySchool;
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
    public class AnnouncementController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public AnnouncementController(IMediator mediator, ILogger<AnnouncementController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost("Add" , Name = "AddAnnouncement")]
        public async Task<ActionResult> AddAnnouncement(CreateAnnouncementDto createAnnouncementDto)
        {
            var createAnnouncementCommand = new CreateAnnouncementCommand(createAnnouncementDto);
            var result = await _mediator.Send(createAnnouncementCommand);
            return Ok(result);
        }

        [HttpPut("Update", Name = "UpdateAnnouncement")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateAnnouncement(UpdateAnnouncementDto updateAnnouncementDto)
        {
            var updateAnnouncementCommand = new UpdateAnnouncementCommand(updateAnnouncementDto);
            var response = await _mediator.Send(updateAnnouncementCommand);
            return Ok(response);
        }

        [HttpGet("all", Name = "GetAllAnnouncement")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllAnnouncement()
        {
            _logger.LogInformation("GetAllAnnouncement Initiated");
            var dtos = await _mediator.Send(new GetAnnouncementListQuery());
            _logger.LogInformation("GetAllAnnouncement Completed");
            return Ok(dtos);
        }

        [HttpGet("id", Name = "GetAnnouncementById")]
        public async Task<ActionResult> GetAcademicById(int id)
        {
            var getAnnouncementDetailQuery = new GetAnnouncementByIdQuery() { Id = id };
            return Ok(await _mediator.Send(getAnnouncementDetailQuery));
        }

        [HttpGet("Pagination", Name = "GetAllAnnouncementByPagination")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllAnnouncementByPagination(int _page, int _size)
        {
            _logger.LogInformation("GetAllPagination Initiated");
            var dtos = await _mediator.Send(new GetAnnouncementByPaginationQuery() { page = _page, size = _size });
            _logger.LogInformation("GetAllPagination Completed");
            return Ok(dtos);
        }

        [HttpGet("Filter", Name = "GetAnnouncementByFilter")]
        public async Task<ActionResult> GetAnnouncementByFilter(int SchoolId, int ClassId, int SectionId, int SubjectId, string TeacherName, string AnnouncementMadeBy, string AnnouncementTitle, string AnnouncementContent, DateTime CreatedDate)
        {
            var getAnnouncementByFilterQuery = new GetAnnouncementByFilterQuery(SchoolId, ClassId, SectionId,  SubjectId, TeacherName, AnnouncementMadeBy, AnnouncementTitle, AnnouncementContent,CreatedDate);
            return Ok(await _mediator.Send(getAnnouncementByFilterQuery));
        }

        [HttpGet("GetAnnouncementListBySchoolPagination", Name = "GetAnnouncementListBySchoolPagination")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAnnouncementListBySchoolPagination(int _page, int _size, int _schoolId)
        {
            var dtos = await _mediator.Send(new GetAnnouncementListByPaginationBySchoolQuery() { page = _page, size = _size, SchoolId = _schoolId });
            return Ok(dtos);
        }

        [HttpGet("GetAllAnnouncementBySchool", Name = "GetAllAnnouncementBySchool")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllAnnouncementBySchool(int schoolid)
        {
            var dtos = await _mediator.Send(new GetAllAnnouncementListBySchoolQuery() { SchoolId = schoolid });
            return Ok(dtos);
        }
        [HttpGet("GetAllAnnouncementBySchoolClassSection", Name = "GetAllAnnouncementBySchoolClassSection")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllAnnouncementBySchoolClassSection(int schoolid , int classid , int sectionid)
        {
            var dtos = await _mediator.Send(new GetAllAnnouncementListBySchoolClassSectionQuery() { SchoolId = schoolid , ClassId = classid , SectionId=sectionid});
            return Ok(dtos);
        }

        [HttpGet("GetAnnouncementListBySchoolClassSectionPagination", Name = "GetAnnouncementListBySchoolClassSectionPagination")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAnnouncementListBySchoolClassSectionPagination(int _page, int _size, int _schoolId, int _classid, int _sectionid)
        {
            var dtos = await _mediator.Send(new GetAllAnnouncementListBySchoolClassSectionPaginationQuery() { page = _page, size = _size, SchoolId = _schoolId , ClassId = _classid, SectionId = _sectionid });
            return Ok(dtos);
        }
    }
}
