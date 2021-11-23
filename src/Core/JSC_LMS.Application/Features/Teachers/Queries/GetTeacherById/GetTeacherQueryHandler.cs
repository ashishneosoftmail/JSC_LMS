using AutoMapper;
using JSC_LMS.Application.CommonDtos;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Features.Principal.Queries.GetPrincipalById;
using JSC_LMS.Application.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Teachers.Queries.GetTeacherById
{
    public class GetTeacherQueryHandler : IRequestHandler<GetTeacherQuery, Response<GetTeacherByIdVm>>
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




        public GetTeacherQueryHandler(IMapper mapper, ISubjectRepository subjectRepository, IZipRepository zipRepository, IStateRepository stateRepository, ICityRepository cityRepository, ISchoolRepository schoolRepository, ITeacherRepository teacherRepository, IClassRepository classRepository, ISectionRepository sectionRepository, IAuthenticationService authenticationService, ILogger<GetTeacherQueryHandler> logger)
        {
            _mapper = mapper;
            _teacherRepository = teacherRepository;
            _logger = logger;
            _authenticationService = authenticationService;
            _sectionRepository = sectionRepository;
            _classRepository = classRepository;
            _schoolRepository = schoolRepository;
            _cityRepository = cityRepository;
            _stateRepository = stateRepository;
            _zipRepository = zipRepository;
            _subjectRepository = subjectRepository;

        }


        public async Task<Response<GetTeacherByIdVm>> Handle(GetTeacherQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            Response<GetTeacherByIdVm> responseData = new Response<GetTeacherByIdVm>();
            var teacher = await _teacherRepository.GetByIdAsync(request.Id);
            if (teacher == null)
            {
                responseData.Succeeded = false;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }

            var user = await _authenticationService.GetUserById(teacher.UserId);
            var teacherData = new GetTeacherByIdVm()
            {
                Id = teacher.Id,
                UserId = teacher.UserId,
                AddressLine1 = teacher.AddressLine1,
                AddressLine2 = teacher.AddressLine2,
                CreatedDate = (DateTime)teacher.CreatedDate,
                Mobile = teacher.Mobile,
                Username = user.Username,
                Email = user.Email,
                IsActive = teacher.IsActive,
               // SubjectId = teacher.SubjectId,
                TeacherName = teacher.TeacherName,
                UserType = teacher.UserType,
                Section = new SectionDto()
                {
                    Id = teacher.SectionId,
                    SectionName = (await _sectionRepository.GetByIdAsync(teacher.SectionId)).SectionName
                },
                School = new SchoolDto()
                {
                    Id = teacher.SchoolId,
                    SchoolName = (await _schoolRepository.GetByIdAsync(teacher.SchoolId)).SchoolName
                },
                Class = new ClassDto()
                {
                    Id = teacher.SchoolId,
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
                Subject = new SubjectDto()
                {
                    Id = teacher.SchoolId,
                    SubjectName = (await _subjectRepository.GetByIdAsync(teacher.SubjectId)).SubjectName
                },



            };
            _logger.LogInformation("Handle Completed");
            return new Response<GetTeacherByIdVm>(teacherData, "success");
        }
        }
}
