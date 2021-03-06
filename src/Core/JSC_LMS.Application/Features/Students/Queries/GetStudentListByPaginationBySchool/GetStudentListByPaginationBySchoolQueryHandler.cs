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

namespace JSC_LMS.Application.Features.Students.Queries.GetStudentListByPaginationBySchool
{
   
    public class GetStudentListByPaginationBySchoolQueryHandler : IRequestHandler<GetStudentListByPaginationBySchoolQuery, Response<IEnumerable<GetStudentListByPaginationBySchoolDto>>>
    {
        private readonly IClassRepository _classRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetStudentListByPaginationBySchoolQueryHandler(IMapper mapper, IClassRepository classRepository, ISchoolRepository schoolRepository, ISectionRepository sectionRepository, IAuthenticationService authenticationService, IStudentRepository studentRepository, ILogger
            <GetStudentListByPaginationBySchoolQueryHandler> logger)
        {
            _mapper = mapper;
            _classRepository = classRepository;
            _schoolRepository = schoolRepository;
            _sectionRepository = sectionRepository;
            _authenticationService = authenticationService;
            _studentRepository = studentRepository;
            _logger = logger;
        }

        public async Task<Response<IEnumerable<GetStudentListByPaginationBySchoolDto>>> Handle(GetStudentListByPaginationBySchoolQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allstudentData = await _studentRepository.StudentGetPagedReponseAsyncBySchoolId(request.page, request.size, request.SchoolId);
            List<GetStudentListByPaginationBySchoolDto> studentList = new List<GetStudentListByPaginationBySchoolDto>();
            foreach (var student in allstudentData)
            {
                var user = await _authenticationService.GetUserById(student.UserId);
                studentList.Add(new GetStudentListByPaginationBySchoolDto()
                {
                    Id = student.Id,
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
                    },
                    school = new SchoolDto()
                    {
                        Id = student.SchoolId,
                        SchoolName = (await _schoolRepository.GetByIdAsync(student.SchoolId)).SchoolName
                    }



                });
            }

            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetStudentListByPaginationBySchoolDto>>(studentList, "success");
        }

    }
}
