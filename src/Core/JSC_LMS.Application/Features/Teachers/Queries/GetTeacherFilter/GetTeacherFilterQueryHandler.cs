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
using JSC_LMS.Domain.Entities;
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

            if (request.TeacherName != "Select Teacher")
            {
                allTeacher = allTeacher.Where<Teacher>(x => (x.TeacherName == request.TeacherName)).ToList();
            }           
            if (request.CreatedDate.ToShortDateString() != "01-01-0001")
            {
                allTeacher = allTeacher.Where<Teacher>(x => x.CreatedDate?.ToShortDateString() == request.CreatedDate.ToShortDateString()).ToList();
            }
            if (request.IsActive)
            {
                allTeacher = allTeacher.Where<Teacher>(x => x.IsActive == request.IsActive).ToList();
            }
            else
            {
                allTeacher = allTeacher.Where<Teacher>(x => x.IsActive == request.IsActive).ToList();
            }

            Response<IEnumerable<GetTeacherByFilterDto>> responseData = new Response<IEnumerable<GetTeacherByFilterDto>>();
            List<GetTeacherByFilterDto> teacherList = new List<GetTeacherByFilterDto>();

            foreach (var teacher in allTeacher)
            {
                if(request.SchoolName != "Select School" || request.ClassName != "Select Class" || request.SectionName != "Select Section" || request.SubjectName != "Select Subject")
                { 
                if (request.SchoolName != "Select School")
                {
                    var school = (await _schoolRepository.GetByIdAsync(teacher.SchoolId)).SchoolName == request.SchoolName;

                    if (school)
                    {
                        var user = await _authenticationService.GetUserById(teacher.UserId);
                        teacherList.Add(new GetTeacherByFilterDto()
                        {
                            Id = teacher.Id,
                            AddressLine1 = teacher.AddressLine1,
                            AddressLine2 = teacher.AddressLine2,
                            CreatedDate = (DateTime)teacher.CreatedDate,
                            Mobile = teacher.Mobile,
                            Username = user.Username,
                            Email = user.Email,
                            IsActive = teacher.IsActive,
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
                            },
                            SchoolId = new SchoolDto()
                            {
                                Id = teacher.SchoolId,
                                SchoolName = (await _schoolRepository.GetByIdAsync(teacher.SchoolId)).SchoolName
                            }

                        });
                    }
                }
                if (request.ClassName != "Select Class")
                {
                    var classes = (await _classRepository.GetByIdAsync(teacher.ClassId)).ClassName == request.ClassName;

                    if (classes)
                    {
                        var user = await _authenticationService.GetUserById(teacher.UserId);
                        teacherList.Add(new GetTeacherByFilterDto()
                        {
                            Id = teacher.Id,
                            AddressLine1 = teacher.AddressLine1,
                            AddressLine2 = teacher.AddressLine2,
                            CreatedDate = (DateTime)teacher.CreatedDate,
                            Mobile = teacher.Mobile,
                            Username = user.Username,
                            Email = user.Email,
                            IsActive = teacher.IsActive,
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
                            },
                            SchoolId = new SchoolDto()
                            {
                                Id = teacher.SchoolId,
                                SchoolName = (await _schoolRepository.GetByIdAsync(teacher.SchoolId)).SchoolName
                            }

                        });
                    }
                }
                if (request.SectionName != "Select Section")
                {
                    var section = (await _sectionRepository.GetByIdAsync(teacher.SectionId)).SectionName == request.SectionName;
                    if (section)
                    {
                        var user = await _authenticationService.GetUserById(teacher.UserId);
                        teacherList.Add(new GetTeacherByFilterDto()
                        {
                            Id = teacher.Id,
                            AddressLine1 = teacher.AddressLine1,
                            AddressLine2 = teacher.AddressLine2,
                            CreatedDate = (DateTime)teacher.CreatedDate,
                            Mobile = teacher.Mobile,
                            Username = user.Username,
                            Email = user.Email,
                            IsActive = teacher.IsActive,
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
                            },
                            SchoolId = new SchoolDto()
                            {
                                Id = teacher.SchoolId,
                                SchoolName = (await _schoolRepository.GetByIdAsync(teacher.SchoolId)).SchoolName
                            }

                        });
                    }

                }
                if (request.SubjectName != "Select Subject")
                {
                    var subject = (await _subjectRepository.GetByIdAsync(teacher.SubjectId)).SubjectName == request.SubjectName;
                    if (subject)
                    {
                        var user = await _authenticationService.GetUserById(teacher.UserId);
                        teacherList.Add(new GetTeacherByFilterDto()
                        {
                            Id = teacher.Id,
                            AddressLine1 = teacher.AddressLine1,
                            AddressLine2 = teacher.AddressLine2,
                            CreatedDate = (DateTime)teacher.CreatedDate,
                            Mobile = teacher.Mobile,
                            Username = user.Username,
                            Email = user.Email,
                            IsActive = teacher.IsActive,
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
                            },
                            SchoolId = new SchoolDto()
                            {
                                Id = teacher.SchoolId,
                                SchoolName = (await _schoolRepository.GetByIdAsync(teacher.SchoolId)).SchoolName
                            }

                        });
                    }

                }
                }
                else {
                    var user = await _authenticationService.GetUserById(teacher.UserId);
                    teacherList.Add(new GetTeacherByFilterDto()
                    {
                        Id = teacher.Id,
                        AddressLine1 = teacher.AddressLine1,
                        AddressLine2 = teacher.AddressLine2,
                        CreatedDate = (DateTime)teacher.CreatedDate,
                        Mobile = teacher.Mobile,
                        Username = user.Username,
                        Email = user.Email,
                        IsActive = teacher.IsActive,
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
                        },
                        SchoolId = new SchoolDto()
                        {
                            Id = teacher.SchoolId,
                            SchoolName = (await _schoolRepository.GetByIdAsync(teacher.SchoolId)).SchoolName
                        }

                    });

                }

            }
         
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetTeacherByFilterDto>>(teacherList, "success");

        }

        }
    }
