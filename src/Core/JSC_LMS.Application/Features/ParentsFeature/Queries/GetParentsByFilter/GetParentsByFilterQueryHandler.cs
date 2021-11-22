using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JSC_LMS.Domain.Entities;
using System.Linq;
using JSC_LMS.Application.CommonDtos;

namespace JSC_LMS.Application.Features.ParentsFeature.Queries.GetParentsByFilter
{
    public class GetParentsByFilterQueryHandler : IRequestHandler<GetParentsByFilterQuery, Response<IEnumerable<GetParentsByFilterDto>>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IParentsRepository _parentsRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetParentsByFilterQueryHandler(IMapper mapper, IStudentRepository studentRepository, IClassRepository classRepository, ISectionRepository sectionRepository, IAuthenticationService authenticationService, IParentsRepository parentsRepository, ILogger<GetParentsByFilterQueryHandler> logger)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
            _parentsRepository = parentsRepository;
            _classRepository = classRepository;
            _sectionRepository = sectionRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }


        public async Task<Response<IEnumerable<GetParentsByFilterDto>>> Handle(GetParentsByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allParents = await _parentsRepository.ListAllAsync();
            var allStudents = await _studentRepository.ListAllAsync();
            List<StudentParentsDto> studentParentTempData = new List<StudentParentsDto>();
            foreach (var parent in allParents)
            {
                string[] studentList = parent.StudentId.Split(",");
                foreach(var TempStudent in studentList)
                {
                    studentParentTempData.Add(new StudentParentsDto()
                    {
                        Id = parent.Id,
                         StudentId = Convert.ToInt32(TempStudent)  , 
                         ParentName=parent.ParentName,
                        Class = new ClassDto()
                        {
                            Id = parent.ClassId,
                            ClassName = (await _classRepository.GetByIdAsync(parent.ClassId)).ClassName
                        },
                        Section = new SectionDto()
                        {
                            Id = parent.SectionId,
                            SectionName = (await _sectionRepository.GetByIdAsync(parent.SectionId)).SectionName
                        },
                        CreatedDate =parent.CreatedDate,
                        IsActive = parent.IsActive
                    });
                }
            }

            var data = (from prt in studentParentTempData
                        join std in allStudents on prt.StudentId equals std.Id select new { 
                prt.Id,
                prt.ParentName,
                std.StudentName,
                prt.Class.ClassName,
                prt.Section.SectionName,
                prt.CreatedDate,
                prt.IsActive

            }).ToList();

            if(request.StudentName != "Select Student")
            {
                data = data.Where(x => (x.StudentName == request.StudentName)).ToList();
            }

                if (request.ClassName != "Select Class")
            {
                data = data.Where(x => (x.ClassName == request.ClassName)).ToList();

            }
            if (request.SectionName != "Select Section")
            {
                data = data.Where(x => (x.SectionName == request.SectionName)).ToList();

            }
            if (request.ParentName != "Select Parent")
            {
                data = data.Where(x => (x.ParentName == request.ParentName)).ToList();

            }

            if (request.IsActive)
            {
                data = data.Where(x => x.IsActive == request.IsActive).ToList();
            }
            else
            {
                data = data.Where(x => x.IsActive == request.IsActive).ToList();
            }
            Response<IEnumerable<GetParentsByFilterDto>> responseData = new Response<IEnumerable<GetParentsByFilterDto>>();
            List<GetParentsByFilterDto> parentsList = new List<GetParentsByFilterDto>();
            foreach (var parent in data)
            {
                parentsList.Add(new GetParentsByFilterDto()
                {
                        Id = parent.Id,
                        ParentName = parent.ParentName,
                        StudentName = parent.StudentName,
                        CreatedDate = parent.CreatedDate,
                        ClassName = parent.ClassName,
                        SectionName = parent.SectionName,
                        IsActive = parent.IsActive
                });
            }
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetParentsByFilterDto>>(parentsList, "success");
        }


    }
}
