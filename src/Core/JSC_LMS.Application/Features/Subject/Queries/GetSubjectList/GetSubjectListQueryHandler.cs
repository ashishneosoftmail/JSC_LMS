using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Subject.Queries.GetSubjectList
{
 
        public class GetSubjectListQueryHandler : IRequestHandler<GetSubjectListQuery, Response<IEnumerable<GetSubjectListDto>>>
        {
          
            private readonly IMapper _mapper;
            private readonly ILogger _logger;
            private readonly ISectionRepository _sectionRepository;
            private readonly IClassRepository _classRepository;
            private readonly ISchoolRepository _schoolRepository;
           
            private readonly ISubjectRepository _subjectRepository;

            public GetSubjectListQueryHandler(IMapper mapper, ISubjectRepository subjectRepository, ISchoolRepository schoolRepository, IClassRepository classRepository, ISectionRepository sectionRepository,  ILogger<GetSubjectListQueryHandler> logger)
            {
                _mapper = mapper;
                
                _logger = logger;
                _sectionRepository = sectionRepository;
                _classRepository = classRepository;
                _schoolRepository = schoolRepository;
               
                _subjectRepository = subjectRepository;

            }

            public async Task<Response<IEnumerable<GetSubjectListDto>>> Handle(GetSubjectListQuery request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("Handle Initiated");
                var allSubjects = await _subjectRepository.ListAllAsync();
                List<GetSubjectListDto> subjectList = new List<GetSubjectListDto>();
                foreach (var subject in allSubjects)
                {
                //var user = await _authenticationService.GetUserById(principal.UserId);
                subjectList.Add(new GetSubjectListDto()
                    {
                        Id = subject.Id,
                        CreatedDate = (DateTime)subject.CreatedDate,
                
                       
                        
                        IsActive = subject.IsActive,
                        //SubjectId = teacher.SubjectId,
                        SubjectName = subject.SubjectName,
                      
                        SectionId = new SectionDto()
                        {
                            Id = subject.SectionId,
                            SectionName = (await _sectionRepository.GetByIdAsync(subject.SectionId)).SectionName
                        },
                        SchoolId = new SchoolDto()
                        {
                            Id = subject.SchoolId,
                            SchoolName = (await _schoolRepository.GetByIdAsync(subject.SchoolId)).SchoolName
                        },
                        ClassId = new ClassDto()
                        {
                            Id = subject.SchoolId,
                            ClassName = (await _classRepository.GetByIdAsync(subject.ClassId)).ClassName
                        },
                    
                    
                       
                     


                    });

                }

                //var teacher = _mapper.Map<List<GetTeacherListVm>>(allTeachers);
                _logger.LogInformation("Handle Completed");
                return new Response<IEnumerable<GetSubjectListDto>>(subjectList, "success");
                //return new Response<IEnumerable<GetSchoolListDto>>(schoolList, "success");
            }
        }
}
