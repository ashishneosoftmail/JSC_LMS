using AutoMapper;
using JSC_LMS.Application.CommonDtos;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#region - Query handler for Get Teacher By Pagination: by Shivani Goswami
namespace JSC_LMS.Application.Features.Teachers.Queries.GetTeacherByPagination
{
    public class GetTeacherByPaginationQueryHandler : IRequestHandler<GetTeacherByPaginationQuery, Response<GetTeacherListByPaginationResponse>>
    {
        
        private readonly IAuthenticationService _authenticationService;       
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ISectionRepository _sectionRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IZipRepository _zipRepository;
        private readonly ISubjectRepository _subjectRepository;


        public GetTeacherByPaginationQueryHandler(IMapper mapper, ITeacherRepository teacherRepository, ISchoolRepository schoolRepository, IAuthenticationService authenticationService, ILogger<GetTeacherByPaginationQueryHandler> logger, ISectionRepository sectionRepository , IClassRepository classRepository, ICityRepository cityRepository , IStateRepository stateRepository, IZipRepository zipRepository , ISubjectRepository subjectRepository)
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

        /// <summary>
        /// Gives teachers' list with custom pagination : by Shivani Goswami
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Response<GetTeacherListByPaginationResponse>> Handle(GetTeacherByPaginationQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allTeacherCount = await _teacherRepository.ListAllAsync();
            var allTeacher = await _teacherRepository.GetPagedReponseAsync(request.page, request.size);
            GetTeacherListByPaginationResponse getTeacherListByPaginationResponse = new GetTeacherListByPaginationResponse();
            List<GetTeacherListPaginationDto> teacherList = new List<GetTeacherListPaginationDto>();
            foreach (var teacher in allTeacher)
            {
                var user = await _authenticationService.GetUserById(teacher.UserId);
                teacherList.Add(new GetTeacherListPaginationDto()
                {
                    Id = teacher.Id,
                    AddressLine1 = teacher.AddressLine1,
                    AddressLine2 = teacher.AddressLine2,
                    CreatedDate = (DateTime)teacher.CreatedDate,
                    Mobile = teacher.Mobile,
                    // Username = teacher.Username,
                    //  Email = teacher.Email,
                    IsActive = teacher.IsActive,
                    //SubjectId = teacher.SubjectId,
                    TeacherName = teacher.TeacherName,
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
                    SubjectId = new SubjectDto()
                    {
                        Id = teacher.SchoolId,
                        SubjectName = (await _subjectRepository.GetByIdAsync(teacher.SubjectId)).SubjectName
                    }
                });
            }
            getTeacherListByPaginationResponse.GetTeacherListPaginationDto = teacherList;
            getTeacherListByPaginationResponse.Count = allTeacherCount.Count();

            /* var principal = _mapper.Map<List<GetPrincipalListDto>>(allPrincipal);
             _logger.LogInformation("Hanlde Completed");
             return new Response<IEnumerable<GetPrincipalListDto>>(principal, "success");*/
            _logger.LogInformation("Hanlde Completed");
            return new Response<GetTeacherListByPaginationResponse>(getTeacherListByPaginationResponse, "success");
        }

    }
}
#endregion