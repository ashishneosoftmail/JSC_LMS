using AutoMapper;
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

namespace JSC_LMS.Application.Features.Academics.Queries.GetAcademicByPagination
{
    public class GetAcademicByPaginationQueryHandler : IRequestHandler<GetAcademicByPaginationQuery, Response<GetAcademicListByPaginationResponse>>
    {
        private readonly IClassRepository _classRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IAcademicRepository _academicRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetAcademicByPaginationQueryHandler(IMapper mapper, IClassRepository classRepository, ISchoolRepository schoolRepository, ISectionRepository sectionRepository, ISubjectRepository subjectRepository, ITeacherRepository teacherRepository, IAcademicRepository academicRepository, IAuthenticationService authenticationService, ILogger<GetAcademicByPaginationQueryHandler> logger)
        {
            _mapper = mapper;
            _classRepository = classRepository;
            _schoolRepository = schoolRepository;
            _sectionRepository = sectionRepository;
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
            _academicRepository = academicRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }
        public async Task<Response<GetAcademicListByPaginationResponse>> Handle(GetAcademicByPaginationQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allAcademicCount = await _academicRepository.ListAllAsync();
            var allAcademic = await _academicRepository.GetPagedReponseAsync(request.page, request.size);
            GetAcademicListByPaginationResponse getAcademicListByPaginationResponse = new GetAcademicListByPaginationResponse();
            List<GetAcademicListPaginationDto> academicList = new List<GetAcademicListPaginationDto>();
            foreach (var academic in allAcademic)
            {

                academicList.Add(new GetAcademicListPaginationDto()
                {
                    Id = academic.Id,
                   
                    IsActive = academic.IsActive,
                 
                    Type = academic.Type,
                    CreatedDate = (DateTime)academic.CreatedDate,
                   
                    Class = new ClassDto()
                    {
                        Id = academic.ClassId,
                        ClassName = (await _classRepository.GetByIdAsync(academic.ClassId)).ClassName
                    },
                    School = new SchoolDto()
                    {
                        Id = academic.SchoolId,
                        SchoolName = (await _schoolRepository.GetByIdAsync(academic.SchoolId)).SchoolName
                    },
                    Subject = new SubjectDto()
                    {
                        Id = academic.SubjectId,
                        SubjectName = (await _subjectRepository.GetByIdAsync(academic.SubjectId)).SubjectName
                    },

                    Teacher = new TeacherDto()
                    {
                        Id = academic.TeacherId,
                        TeacherName = (await _teacherRepository.GetByIdAsync(academic.TeacherId)).TeacherName
                    },
                    Section = new SectionDto()
                    {
                        Id = academic.SectionId,
                        SectionName = (await _sectionRepository.GetByIdAsync(academic.SectionId)).SectionName
                    }

                });
            }
            getAcademicListByPaginationResponse.GetAcademicListPaginationDto = academicList;
            getAcademicListByPaginationResponse.Count = allAcademicCount.Count();

            /* var principal = _mapper.Map<List<GetPrincipalListDto>>(allPrincipal);
             _logger.LogInformation("Hanlde Completed");
             return new Response<IEnumerable<GetPrincipalListDto>>(principal, "success");*/
            _logger.LogInformation("Handle Completed");
            return new Response<GetAcademicListByPaginationResponse>(getAcademicListByPaginationResponse, "success");
        }

    }
}
