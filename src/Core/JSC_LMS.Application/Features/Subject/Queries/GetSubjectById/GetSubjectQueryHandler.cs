using AutoMapper;
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

namespace JSC_LMS.Application.Features.Subject.Queries.GetSubjectById
{
   public class GetSubjectQueryHandler : IRequestHandler<GetSubjectQuery, Response<GetSubjectByIdVm>>
    {
       
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ISectionRepository _sectionRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly ISubjectRepository _subjectRepository;





        public GetSubjectQueryHandler(IMapper mapper,   ISchoolRepository schoolRepository, ISubjectRepository subjectRepository, IClassRepository classRepository, ISectionRepository sectionRepository, IAuthenticationService authenticationService, ILogger<GetSubjectQueryHandler> logger)
        {
            _mapper = mapper;
   
            _logger = logger;
            _authenticationService = authenticationService;
            _sectionRepository = sectionRepository;
            _classRepository = classRepository;
            _schoolRepository = schoolRepository;
           
            _subjectRepository = subjectRepository;

        }


        public async Task<Response<GetSubjectByIdVm>> Handle(GetSubjectQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            Response<GetSubjectByIdVm> responseData = new Response<GetSubjectByIdVm>();
            var subject = await _subjectRepository.GetByIdAsync(request.Id);
            if (subject == null)
            {
                responseData.Succeeded = false;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }

           
            var subjectData = new GetSubjectByIdVm()
            {
                Id = subject.Id,
                SubjectName = subject.SubjectName,
              
                CreatedDate = (DateTime)subject.CreatedDate,



                IsActive = subject.IsActive,
              
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
                },
             
            
               
                



            };
            _logger.LogInformation("Handle Completed");
            return new Response<GetSubjectByIdVm>(subjectData, "success");
        }
    }
}
