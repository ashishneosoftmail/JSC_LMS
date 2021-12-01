using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Subject.Queries.GetSubjectFilter
{
 
    public class GetSubjectFilterQueryHandler : IRequestHandler<GetSubjectFilterQuery, Response<IEnumerable<GetSubjectFilterDto>>>
    {
     
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ISectionRepository _sectionRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISchoolRepository _schoolRepository;
    
        private readonly ISubjectRepository _subjectRepository;

        public GetSubjectFilterQueryHandler(IMapper mapper, ISubjectRepository subjectRepository, ISchoolRepository schoolRepository,  IClassRepository classRepository, ISectionRepository sectionRepository, IAuthenticationService authenticationService, ILogger<GetSubjectFilterQueryHandler> logger)
        {
            _mapper = mapper;
       
            _logger = logger;
            _authenticationService = authenticationService;
            _sectionRepository = sectionRepository;
            _classRepository = classRepository;
            _schoolRepository = schoolRepository;
            
            _subjectRepository = subjectRepository;


        }

        public async Task<Response<IEnumerable<GetSubjectFilterDto>>> Handle(GetSubjectFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allSubject = await _subjectRepository.ListAllAsync();
            var searchFilter = (allSubject.Where<JSC_LMS.Domain.Entities.Subject>(x => (x.SubjectName == request.SubjectName) || (x.CreatedDate?.ToShortDateString() == request.CreatedDate.ToShortDateString()) || (x.IsActive == request.IsActive)).Select(x => (x)));

            if (request.SubjectName != "Select Subject")
            {
                allSubject = allSubject.Where<JSC_LMS.Domain.Entities.Subject>(x => (x.SubjectName == request.SubjectName)).ToList();
            }
            if (request.CreatedDate.ToShortDateString() != "01-01-0001")
            {
                allSubject = allSubject.Where<JSC_LMS.Domain.Entities.Subject>(x => x.CreatedDate?.ToShortDateString() == request.CreatedDate.ToShortDateString()).ToList();
            }
            if (request.IsActive)
            {
                allSubject = allSubject.Where<JSC_LMS.Domain.Entities.Subject>(x => x.IsActive == request.IsActive).ToList();
            }
            else
            {
                allSubject = allSubject.Where<JSC_LMS.Domain.Entities.Subject>(x => x.IsActive == request.IsActive).ToList();
            }
            Response<IEnumerable<GetSubjectFilterDto>> responseData = new Response<IEnumerable<GetSubjectFilterDto>>();
            List<GetSubjectFilterDto> subjectList = new List<GetSubjectFilterDto>();
            foreach (var subject in allSubject)
            {
                if (request.SchoolName != "Select School")
                {
                    var school = (await _schoolRepository.GetByIdAsync(subject.SchoolId)).SchoolName == request.SchoolName;
                    if (school)
                    {

                        subjectList.Add(new GetSubjectFilterDto()
                        {
                            Id = subject.Id,


                            CreatedDate = (DateTime)subject.CreatedDate,

                            IsActive = subject.IsActive,

                            SubjectName = subject.SubjectName,

                            School = new SchoolDto()
                            {
                                Id = subject.SchoolId,
                                SchoolName = (await _schoolRepository.GetByIdAsync(subject.SchoolId)).SchoolName
                            },

                            Section = new SectionDto()
                            {
                                Id = subject.SectionId,
                                SectionName = (await _sectionRepository.GetByIdAsync(subject.SectionId)).SectionName
                            },
                          
                            Class = new ClassDto()
                            {
                                Id = subject.ClassId,
                                ClassName = (await _classRepository.GetByIdAsync(subject.ClassId)).ClassName
                            }



                        });
                    }
                }
                else
                {

                    subjectList.Add(new GetSubjectFilterDto()
                    {
                        Id = subject.Id,


                        CreatedDate = (DateTime)subject.CreatedDate,

                        IsActive = subject.IsActive,

                        SubjectName = subject.SubjectName,

                        Section = new SectionDto()
                        {
                            Id = subject.SectionId,
                            SectionName = (await _sectionRepository.GetByIdAsync(subject.SectionId)).SectionName
                        },
                        School = new SchoolDto()
                        {
                            Id = subject.SchoolId,
                            SchoolName = (await _schoolRepository.GetByIdAsync(subject.SchoolId)).SchoolName
                        },
                        Class = new ClassDto()
                        {
                            Id = subject.SchoolId,
                            ClassName = (await _classRepository.GetByIdAsync(subject.ClassId)).ClassName
                        }



                    });
                }
            }
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetSubjectFilterDto>>(subjectList, "success");
        }


    }
}
