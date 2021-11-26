using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Teachers.Queries.GetTeacherByUserId
{
    public class GetTeacherByUserIdQueryHandler : IRequestHandler<GetTeacherByUserIdQuery, Response<GetTeacherByUserIdDto>>
    {

        private readonly ITeacherRepository _teacherRepository;

        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetTeacherByUserIdQueryHandler(IMapper mapper, ITeacherRepository teacherRepository, ILogger<GetTeacherByUserIdQueryHandler> logger)
        {
            _mapper = mapper;
            _teacherRepository = teacherRepository;
            _logger = logger;
        }

        public async Task<Response<GetTeacherByUserIdDto>> Handle(GetTeacherByUserIdQuery request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Handle Initiated");
            Response<GetTeacherByUserIdDto> responseData = new Response<GetTeacherByUserIdDto>();
            var teacher = (await _teacherRepository.ListAllAsync()).Where<JSC_LMS.Domain.Entities.Teacher>(e => e.UserId == request.UserId).FirstOrDefault();
            if (teacher == null)
            {
                responseData.Succeeded = true;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }
            var teacherData = new GetTeacherByUserIdDto()
            {
                Id = teacher.Id,
                Name = teacher.TeacherName,
                Mobile = teacher.Mobile,
                schoolid=teacher.SchoolId
            };
            _logger.LogInformation("Hanlde Completed");
            return new Response<GetTeacherByUserIdDto>(teacherData, "success");
        }

    }
}
