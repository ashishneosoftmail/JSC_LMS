using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Features.Institutes.Queries.GetInstituteFilter;
using JSC_LMS.Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Teachers.Queries.GetTeacherFilter
{
    public class GetTeacherFilterQueryHandler : IRequestHandler<GetTeacherFilterQuery, Response<IEnumerable<GetTeacherByFilterDto>>>
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

        public GetTeacherFilterQueryHandler(IMapper mapper, ISubjectRepository subjectRepository, IZipRepository zipRepository, IStateRepository stateRepository, ICityRepository cityRepository, ISchoolRepository schoolRepository, ITeacherRepository teacherRepository, IClassRepository classRepository, ISectionRepository sectionRepository, IAuthenticationService authenticationService, ILogger<GetTeacherFilterQueryHandler> logger)
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

        public async Task<Response<IEnumerable<GetTeacherByFilterDto>>> Handle(GetTeacherFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allTeacher = await _teacherRepository.ListAllAsync();
            var searchFilter = (allTeacher.Where<JSC_LMS.Domain.Entities.Teacher>(x => (x.TeacherName == request.TeacherName)&& (x.IsActive == request.IsActive) &&(x.CreatedDate== request.CreatedDate)).Select(x => (x)));
            Response<IEnumerable<GetTeacherByFilterDto>> responseData = new Response<IEnumerable<GetTeacherByFilterDto>>();
           
            if (searchFilter.Count() < 1)
            {
                responseData.Succeeded = true;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }

            List<GetTeacherByFilterDto> teacherList = new List<GetTeacherByFilterDto>();
            foreach (var teacher in searchFilter)
            {
                var section = (await _sectionRepository.GetByIdAsync(teacher.SectionId)).SectionName == request.SectionName;
                var classes  = (await _classRepository.GetByIdAsync(teacher.ClassId)).ClassName == request.ClassName;
                var subject = (await _subjectRepository.GetByIdAsync(teacher.SubjectId)).SubjectName == request.SubjectName;
                var user = await _authenticationService.GetUserById(teacher.UserId);


                if (section && classes && subject)
                {
                    teacherList.Add(new GetTeacherByFilterDto()
                    {
                        Id = teacher.Id,
                        AddressLine1 = teacher.AddressLine1,
                        AddressLine2 = teacher.AddressLine2,
                        Mobile = teacher.Mobile,
                        Username = user.Username,
                        Email = user.Email,
                        IsActive = teacher.IsActive,
                        // SubjectId = teacher.SubjectId,
                        TeacherName = teacher.TeacherName,
                        UserType = teacher.UserType,
                        SectionId = new SectionDto()
                        {
                            Id = teacher.SectionId,
                            SectionName = (await _sectionRepository.GetByIdAsync(teacher.SectionId)).SectionName
                        },
                        SubjectId = new SubjectDto()
                        {
                            Id = teacher.SubjectId,
                            SubjectName = (await _subjectRepository.GetByIdAsync(teacher.SubjectId)).SubjectName
                        },
                        ClassId = new ClassDto()
                        {
                            Id = teacher.SchoolId,
                            ClassName = (await _classRepository.GetByIdAsync(teacher.ClassId)).ClassName
                        }



                    });
                    }
            }
            if (teacherList.Count() < 1)
            {
                responseData.Succeeded = true;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }

            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetTeacherByFilterDto>>(teacherList, "success");

        }

        }
    }
