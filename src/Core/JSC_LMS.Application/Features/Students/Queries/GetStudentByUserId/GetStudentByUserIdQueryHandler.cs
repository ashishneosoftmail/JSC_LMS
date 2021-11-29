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

namespace JSC_LMS.Application.Features.Students.Queries.GetStudentByUserId
{
    public class GetStudentByUserIdQueryHandler : IRequestHandler<GetStudentByUserIdQuery, Response<GetStudentByUserIdDto>>
    {

        private readonly IStudentRepository _studentRepository;

        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetStudentByUserIdQueryHandler(IMapper mapper, IStudentRepository studentRepository, ILogger<GetStudentByUserIdQueryHandler> logger)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
            _logger = logger;
        }

        public async Task<Response<GetStudentByUserIdDto>> Handle(GetStudentByUserIdQuery request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Handle Initiated");
            Response<GetStudentByUserIdDto> responseData = new Response<GetStudentByUserIdDto>();
            var student = (await _studentRepository.ListAllAsync()).Where<JSC_LMS.Domain.Entities.Students>(e => e.UserId == request.UserId).FirstOrDefault();
            if (student == null)
            {
                responseData.Succeeded = true;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }
            var studentData = new GetStudentByUserIdDto()
            {
                Id = student.Id,
                Name = student.StudentName,
                Mobile = student.Mobile,
                Schoolid = student.SchoolId
            };
            _logger.LogInformation("Hanlde Completed");
            return new Response<GetStudentByUserIdDto>(studentData, "success");
        }

    }
}
