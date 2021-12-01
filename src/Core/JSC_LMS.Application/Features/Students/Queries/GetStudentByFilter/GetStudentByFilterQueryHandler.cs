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
using JSC_LMS.Domain.Entities;
using System.Linq;

namespace JSC_LMS.Application.Features.Students.Queries.GetStudentByFilter
{
    public class GetStudentByFilterQueryHandler : IRequestHandler<GetStudentByFilterQuery, Response<IEnumerable<GetStudentByFilterDto>>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetStudentByFilterQueryHandler(IMapper mapper, IStudentRepository studentRepository, IClassRepository classRepository, ISectionRepository sectionRepository, IAuthenticationService authenticationService, ILogger<GetStudentByFilterQueryHandler> logger)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
            _classRepository = classRepository;
            _sectionRepository = sectionRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }


        public async Task<Response<IEnumerable<GetStudentByFilterDto>>> Handle(GetStudentByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allStudent = await _studentRepository.ListAllAsync();

            if (request.SchoolId>0)
            {
                allStudent = allStudent.Where<JSC_LMS.Domain.Entities.Students>(x => (x.SchoolId == request.SchoolId)).ToList();

            }

            if (request.ClassName != "Select Class")
            {
                allStudent = allStudent.Where<JSC_LMS.Domain.Entities.Students>(x => (x.Class.ClassName == request.ClassName)).ToList();
                
            }
            if (request.SectionName != "Select Section")
            {
                allStudent = allStudent.Where<JSC_LMS.Domain.Entities.Students>(x => (x.Section.SectionName == request.SectionName)).ToList();

            }
            if (request.StudentName != "Select Student")
            {
                allStudent = allStudent.Where<JSC_LMS.Domain.Entities.Students>(x => (x.StudentName == request.StudentName)).ToList();

            }
            if (request.CreatedDate.ToShortDateString() != "01-01-0001")
            {
                allStudent = allStudent.Where<JSC_LMS.Domain.Entities.Students>(x => x.CreatedDate?.ToShortDateString() == request.CreatedDate.ToShortDateString()).ToList();
            }
            if (request.IsActive)
            {
                allStudent = allStudent.Where<JSC_LMS.Domain.Entities.Students>(x => x.IsActive == request.IsActive).ToList();
            }
            else
            {
                allStudent = allStudent.Where<JSC_LMS.Domain.Entities.Students>(x => x.IsActive == request.IsActive).ToList();
            }
            Response<IEnumerable<GetStudentByFilterDto>> responseData = new Response<IEnumerable<GetStudentByFilterDto>>();
            List<GetStudentByFilterDto> studentList = new List<GetStudentByFilterDto>();
            foreach (var student in allStudent)
            {
                    var user = await _authenticationService.GetUserById(student.UserId);
                    studentList.Add(new GetStudentByFilterDto()
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
                        }
                    });

                
            }
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetStudentByFilterDto>>(studentList, "success");
        }


    }
}
