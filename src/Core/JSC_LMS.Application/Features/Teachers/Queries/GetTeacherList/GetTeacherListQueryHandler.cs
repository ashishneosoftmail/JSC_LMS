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

namespace JSC_LMS.Application.Features.Teachers.Queries.GetTeacherList
{
    public class GetTeacherListQueryHandler : IRequestHandler<GetTeacherListQuery, Response<IEnumerable<GetTeacherListDto>>>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ISectionRepository _sectionRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IZipRepository _zipRepository;
        private readonly ISubjectRepository _subjectRepository;

        public GetTeacherListQueryHandler(IMapper mapper, ISubjectRepository subjectRepository, ISchoolRepository schoolRepository, IClassRepository classRepository, IZipRepository zipRepository, ISectionRepository sectionRepository, IStateRepository stateRepository, ICityRepository cityRepository, ITeacherRepository teacherRepository, ILogger<GetTeacherListQueryHandler> logger, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _teacherRepository = teacherRepository;
            _logger = logger;
            _sectionRepository = sectionRepository;
            _classRepository = classRepository;
            _schoolRepository = schoolRepository;
            _cityRepository = cityRepository;
            _stateRepository = stateRepository;
            _zipRepository = zipRepository;
            _subjectRepository = subjectRepository;
            _authenticationService = authenticationService;

        }

        public async Task<Response<IEnumerable<GetTeacherListDto>>> Handle(GetTeacherListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allTeachers = await _teacherRepository.ListAllAsync();
            List<GetTeacherListDto> teacherList = new List<GetTeacherListDto>();
            foreach (var teacher in allTeachers)
            {
                var user = await _authenticationService.GetUserById(teacher.UserId);
                teacherList.Add(new GetTeacherListDto()
                {
                    Id = teacher.Id,
                    AddressLine1 = teacher.AddressLine1,
                    AddressLine2 = teacher.AddressLine2,
                    CreatedDate = (DateTime)teacher.CreatedDate,
                    Mobile = teacher.Mobile,
                    Username = user.Username,
                    Email = user.Email,
                    IsActive = teacher.IsActive,
                    //SubjectId = teacher.SubjectId,
                    TeacherName = teacher.TeacherName,
                    UserId = teacher.UserId,
                    UserType = teacher.UserType,
                    SectionId = new SectionDto()
                    {
                        Id = teacher.SectionId,
                        SectionName = (await _sectionRepository.GetByIdAsync(teacher.SectionId)).SectionName
                    },
                    SchoolId = new SchoolDto()
                    {
                        Id = teacher.SchoolId,
                        SchoolName = (await _schoolRepository.GetByIdAsync(teacher.SchoolId)).SchoolName
                    },
                    ClassId = new ClassDto()
                    {
                        Id = teacher.ClassId,
                        ClassName = (await _classRepository.GetByIdAsync(teacher.ClassId)).ClassName
                    },
                    City = new CityDto()
                    {
                        Id = (int)teacher.CityId,
                        CityName = (await _cityRepository.GetByIdAsync((int)teacher.CityId)).CityName
                    },
                    State = new StateDto()
                    {
                        Id = (int)teacher.StateId,
                        StateName = (await _stateRepository.GetByIdAsync((int)teacher.StateId)).StateName
                    },
                    Zip = new ZipDto()
                    {
                        Id = (int)teacher.ZipId,
                        Zipcode = (await _zipRepository.GetByIdAsync((int)teacher.ZipId)).Zipcode
                    },
                    SubjectId = new SubjectDto()
                    {
                        Id = teacher.SubjectId,
                        SubjectName = (await _subjectRepository.GetByIdAsync(teacher.SubjectId)).SubjectName
                    }


                }) ;
                
                }

            //var teacher = _mapper.Map<List<GetTeacherListVm>>(allTeachers);
            _logger.LogInformation("Handle Completed");
            return new Response<IEnumerable<GetTeacherListDto>>(teacherList, "success");
            //return new Response<IEnumerable<GetSchoolListDto>>(schoolList, "success");
        }
    }
}
