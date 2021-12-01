using AutoMapper;
using JSC_LMS.Application.CommonDtos;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Students.Queries.GetStudentById
{
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, Response<GetStudentByIdDto>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetStudentByIdQueryHandler(IMapper mapper, IStudentRepository studentRepository, IClassRepository classRepository, ISectionRepository sectionRepository, IAuthenticationService authenticationService, ILogger<GetStudentByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
            _classRepository = classRepository;
            _sectionRepository = sectionRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public async Task<Response<GetStudentByIdDto>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            Response<GetStudentByIdDto> responseData = new Response<GetStudentByIdDto>();
            var student = await _studentRepository.GetByIdAsync(request.Id);
            if (student == null)
            {
                responseData.Succeeded = true;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }
            var user = await _authenticationService.GetUserById(student.UserId);


            var studentData = new GetStudentByIdDto()
            {
                Id = student.Id,
                UserId = student.UserId,
                AddressLine1 = student.AddressLine1,
                AddressLine2 = student.AddressLine2,
                Mobile = student.Mobile,
                Username = user.Username,
                Email = user.Email,
                IsActive = student.IsActive,
                StudentName = student.StudentName,
                UserType = student.UserType,
                CreatedDate = (DateTime)student.CreatedDate,
                City = _mapper.Map<CityDto>(student.City),
                State = _mapper.Map<StateDto>(student.State),
                SchoolId = student.SchoolId,
                Zip = _mapper.Map<ZipDto>(student.Zip),
                Class = new ClassDto()
                {
                    Id = student.ClassId,
                    ClassName = (await _classRepository.GetByIdAsync(student.ClassId)).ClassName
                },
                Section = new SectionDto()
                {
                    Id = student.SectionId,
                    SectionName = (await _sectionRepository.GetByIdAsync(student.SectionId)).SectionName
                }
            };
            _logger.LogInformation("Hanlde Completed");
            return new Response<GetStudentByIdDto>(studentData, "success");
        }

    }
}
