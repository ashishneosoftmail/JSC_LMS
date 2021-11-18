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

namespace JSC_LMS.Application.Features.ParentsFeature.Queries.GetParentsList
{
    public class GetParentsListQueryHandler : IRequestHandler<GetParentsListQuery, Response<IEnumerable<GetParentsListDto>>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IParentsRepository _parentsRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetParentsListQueryHandler(IMapper mapper, IStudentRepository studentRepository, IParentsRepository parentsRepository, IClassRepository classRepository, ISectionRepository sectionRepository, IAuthenticationService authenticationService, ILogger<GetParentsListQueryHandler> logger)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
            _parentsRepository = parentsRepository;
            _classRepository = classRepository;
            _sectionRepository = sectionRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public async Task<Response<IEnumerable<GetParentsListDto>>> Handle(GetParentsListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allParent = await _parentsRepository.ListAllAsync();
            List<GetParentsListDto> parentsList = new List<GetParentsListDto>();
           
            foreach (var parent in allParent)
            {
                string[] studentList = parent.StudentId.Split(",");
                List<StudentDto> studentData = new List<StudentDto>();
                foreach (var studentname in studentList)
                {
                    var stdnt = await _studentRepository.GetByIdAsync(Convert.ToInt32(studentname));
                    studentData.Add(new StudentDto()
                    {
                        Id = stdnt.Id,
                        StudentName = stdnt.StudentName
                    });
                }


                var user = await _authenticationService.GetUserById(parent.UserId);
                parentsList.Add(new GetParentsListDto()
                {
                    Id = parent.Id,
                    AddressLine1 = parent.AddressLine1,
                    AddressLine2 = parent.AddressLine2,
                    Mobile = parent.Mobile,
                    Username = user.Username,
                    Email = user.Email,
                    IsActive = parent.IsActive,
                    ParentName = parent.ParentName,
                    UserType = parent.UserType,
                    CreatedDate = (DateTime)parent.CreatedDate,
                    City = _mapper.Map<CityDto>(parent.City),
                    State = _mapper.Map<StateDto>(parent.State),
                    Zip = _mapper.Map<ZipDto>(parent.Zip),
                    Class = new ClassDto()
                    {
                        Id = parent.ClassId,
                        ClassName = (await _classRepository.GetByIdAsync(parent.ClassId)).ClassName
                    },
                    Section = new SectionDto()
                    {
                        Id = parent.SectionId,
                        SectionName = (await _sectionRepository.GetByIdAsync(parent.SectionId)).SectionName
                    } ,
                    Student = studentData
                    
                });
            }
            _logger.LogInformation("Handle Completed");
            return new Response<IEnumerable<GetParentsListDto>>(parentsList, "success");
        }

    }
}
