using AutoMapper;
using JSC_LMS.Application.CommonDtos;
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

namespace JSC_LMS.Application.Features.ParentsFeature.Queries.GetParentsByPagination
{
    
    public class GetParentsByPaginationQueryHandler : IRequestHandler<GetParentsByPaginationQuery, Response<GetParentsByPaginationResponse>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;
        private readonly IParentsRepository _parentsRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetParentsByPaginationQueryHandler(IMapper mapper, IStudentRepository studentRepository, IParentsRepository parentsRepository, IClassRepository classRepository, ISectionRepository sectionRepository, IAuthenticationService authenticationService, ILogger
            <GetParentsByPaginationQueryHandler> logger)
        {
            _mapper = mapper;
            _parentsRepository = parentsRepository;
            _studentRepository = studentRepository;
            _classRepository = classRepository;
            _sectionRepository = sectionRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public async Task<Response<GetParentsByPaginationResponse>> Handle(GetParentsByPaginationQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allParentsCount = await _parentsRepository.ListAllAsync();
            var allParents = await _parentsRepository.GetPagedReponseAsync(request.page, request.size);
            var allStudents = await _studentRepository.ListAllAsync();
            List<StudentParentsDto> studentParentTempData = new List<StudentParentsDto>();

            foreach (var parent in allParents)
            {
                string[] studentList = parent.StudentId.Split(",");
                foreach (var TempStudent in studentList)
                {
                    studentParentTempData.Add(new StudentParentsDto()
                    {
                        Id = parent.Id,
                        StudentId = Convert.ToInt32(TempStudent),
                        ParentName = parent.ParentName,
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
                        CreatedDate = parent.CreatedDate,
                        IsActive = parent.IsActive
                    });
                }
            }

            var data = (from prt in studentParentTempData
                        join std in allStudents on prt.StudentId equals std.Id
                        select new
                        {
                            prt.Id,
                            prt.ParentName,
                            std.StudentName,
                            prt.Class.ClassName,
                            prt.Section.SectionName,
                            prt.CreatedDate,
                            prt.IsActive

                        }).ToList();


            GetParentsByPaginationResponse getParentsListByPaginationResponse = new GetParentsByPaginationResponse();
            List<GetParentsByPaginationDto> parentList = new List<GetParentsByPaginationDto>();
            foreach (var parent in data)
            {
                parentList.Add(new GetParentsByPaginationDto()
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
            getParentsListByPaginationResponse.GetParentsListPaginationDto = parentList;
            getParentsListByPaginationResponse.Count = allParentsCount.Count();

             _logger.LogInformation("Handle Completed");
            return new Response<GetParentsByPaginationResponse>(getParentsListByPaginationResponse, "success");
        }

    }
}
