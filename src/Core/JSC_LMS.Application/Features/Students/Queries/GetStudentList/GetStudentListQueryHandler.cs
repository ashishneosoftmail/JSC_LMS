using AutoMapper;
using JSC_LMS.Application.CommonDtos;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Students.Queries.GetStudentList
{
    

    public class GetStudentListQueryHandler : IRequestHandler<GetStudentListQuery, Response<IEnumerable<GetStudentListDto>>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetStudentListQueryHandler(IMapper mapper, IStudentRepository studentRepository, IClassRepository classRepository, ISectionRepository sectionRepository,IAuthenticationService authenticationService, ILogger<GetStudentListQueryHandler> logger)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
            _classRepository = classRepository;
            _sectionRepository = sectionRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public async Task<Response<IEnumerable<GetStudentListDto>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allStudent = await _studentRepository.ListAllAsync();
            List<GetStudentListDto> studentList = new List<GetStudentListDto>();
            foreach (var student in allStudent)
            {
                var user = await _authenticationService.GetUserById(student.UserId);
                studentList.Add(new GetStudentListDto()
                {
                    Id = student.Id,
                    AddressLine1 = student.AddressLine1,
                    AddressLine2 = student.AddressLine2,
                    Mobile = student.Mobile,
                    Username = user.Username,
                    Email = user.Email,
                    IsActive = student.IsActive,
                    StudentName = student.StudentName,
                    UserType=student.UserType,
                    CreatedDate = (DateTime)student.CreatedDate,
                    City = _mapper.Map<CityDto>(student.City),
                    State = _mapper.Map<StateDto>(student.State),
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
                });
            }
            /* var principal = _mapper.Map<List<GetPrincipalListDto>>(allPrincipal);
             _logger.LogInformation("Hanlde Completed");
             return new Response<IEnumerable<GetPrincipalListDto>>(principal, "success");*/
            _logger.LogInformation("Handle Completed");
            return new Response<IEnumerable<GetStudentListDto>>(studentList, "success");
        }

    }
}
