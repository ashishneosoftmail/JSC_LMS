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


namespace JSC_LMS.Application.Features.Students.Queries.GetStudentByPagination
{
    public class GetStudentByPaginationQueryHandler : IRequestHandler<GetStudentByPaginationQuery, Response<GetStudentListByPaginationResponse>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetStudentByPaginationQueryHandler(IMapper mapper, IStudentRepository studentRepository, IClassRepository classRepository, ISectionRepository sectionRepository, IAuthenticationService authenticationService, ILogger
            <GetStudentByPaginationQueryHandler> logger)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
            _classRepository = classRepository;
            _sectionRepository = sectionRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public async Task<Response<GetStudentListByPaginationResponse>> Handle(GetStudentByPaginationQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allStudentCount = await _studentRepository.ListAllAsync();
            var allStudent = await _studentRepository.GetPagedReponseAsync(request.page, request.size);
            GetStudentListByPaginationResponse getStudentListByPaginationResponse = new GetStudentListByPaginationResponse();
            List<GetStudentListPaginationDto> studentList = new List<GetStudentListPaginationDto>();
            foreach (var student in allStudent)
            {
                var user = await _authenticationService.GetUserById(student.UserId);
                studentList.Add(new GetStudentListPaginationDto()
                {
                    Id = student.Id,
                    AddressLine1 = student.AddressLine1,
                    AddressLine2 = student.AddressLine2,
                    Mobile = student.Mobile,
                    Username = user.Username,
                    Email = user.Email,
                    IsActive = student.IsActive,
                    StudentName = student.StudentName,
                    UserType = student.UserType,
                    CreatedDate = (DateTime)student.CreatedDate,
                    City = _mapper.Map<CityDto>(student.City),
                    State = _mapper.Map<StateDto>(student.State),
                    Zip = _mapper.Map<ZipDto>(student.Zip),
                    Class = new ClassDto()
                    {
                        Id = student.ClassId,
                        ClassName = (await _classRepository.GetByIdAsync(student.ClassId)).ClassName
                    },
                    Section = new SectionDto()
                    {
                        Id = student.SectionId,
                        SectionName = (await _sectionRepository.GetByIdAsync(student.SectionId)).SectionName
                    }

                });
            }
            getStudentListByPaginationResponse.GetStudentListPaginationDto = studentList;
            getStudentListByPaginationResponse.Count = allStudentCount.Count();

            /* var principal = _mapper.Map<List<GetPrincipalListDto>>(allPrincipal);
             _logger.LogInformation("Hanlde Completed");
             return new Response<IEnumerable<GetPrincipalListDto>>(principal, "success");*/
            _logger.LogInformation("Handle Completed");
            return new Response<GetStudentListByPaginationResponse>(getStudentListByPaginationResponse, "success");
        }

    }
}
