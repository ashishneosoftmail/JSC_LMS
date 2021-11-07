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

namespace JSC_LMS.Application.Features.Subject.Queries.GetSubjectByPagination
{
    public class GetSubjectByPaginationQueryHandler : IRequestHandler<GetSubjectByPaginationQuery, Response<GetSubjectByPaginationResponse>>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IClassRepository _classRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetSubjectByPaginationQueryHandler(IMapper mapper,ISubjectRepository subjectRepository, ISectionRepository sectionlRepository, IClassRepository classRepository, ISchoolRepository schoolRepository, IAuthenticationService authenticationService, ILogger
            <GetSubjectByPaginationQueryHandler> logger)
        {
            _mapper = mapper;
            _sectionRepository = sectionlRepository;
            _schoolRepository = schoolRepository;
            _classRepository = classRepository;
            _subjectRepository = subjectRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public async Task<Response<GetSubjectByPaginationResponse>> Handle(GetSubjectByPaginationQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allSubjectCount = await _subjectRepository.ListAllAsync();
            var allSubject = await _subjectRepository.GetPagedReponseAsync(request.page, request.size);
            GetSubjectByPaginationResponse getSubjectListByPaginationResponse = new GetSubjectByPaginationResponse();
            List<GetSubjectListPaginationDto> subjectList = new List<GetSubjectListPaginationDto>();
            foreach (var subject in allSubject)
            {

                subjectList.Add(new GetSubjectListPaginationDto()
                {
                    Id = subject.Id,


                    IsActive = subject.IsActive,
                    SubjectName = subject.SubjectName,
                    CreatedDate = (DateTime)subject.CreatedDate,

                    School = new SchoolPaginationDto()
                    {
                        Id = subject.SchoolId,
                        SchoolName = (await _schoolRepository.GetByIdAsync(subject.SchoolId)).SchoolName
                    },

                    Class = new ClassPaginationDto()
                    {
                        Id = subject.SchoolId,
                        ClassName = (await _classRepository.GetByIdAsync(subject.ClassId)).ClassName
                    },
                    Section = new SectionPaginationDto()
                    {
                        Id = subject.SchoolId,
                        SectionName = (await _sectionRepository.GetByIdAsync(subject.SectionId)).SectionName
                    }

                });
            }
            getSubjectListByPaginationResponse.GetSubjectListPaginationDto = subjectList;
            getSubjectListByPaginationResponse.Count = allSubjectCount.Count();

            /* var principal = _mapper.Map<List<GetPrincipalListDto>>(allPrincipal);
             _logger.LogInformation("Hanlde Completed");
             return new Response<IEnumerable<GetPrincipalListDto>>(principal, "success");*/
            _logger.LogInformation("Hanlde Completed");
            return new Response<GetSubjectByPaginationResponse>(getSubjectListByPaginationResponse, "success");
        }
    }
}
