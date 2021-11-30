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

namespace JSC_LMS.Application.Features.ParentsFeature.Queries.GetParentsListByPaginationBySchool
{

    public class GetParentsListByPaginationBySchoolQueryHandler : IRequestHandler<GetParentsListByPaginationBySchoolQuery, Response<IEnumerable<GetParentsListByPaginationBySchoolDto>>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IParentsRepository _parentsRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetParentsListByPaginationBySchoolQueryHandler(IMapper mapper, IStudentRepository studentRepository, IParentsRepository parentsRepository, IClassRepository classRepository, ISectionRepository sectionRepository, ISchoolRepository schoolRepository, IAuthenticationService authenticationService, ILogger<GetParentsListByPaginationBySchoolQueryHandler> logger)
        {

            _schoolRepository = schoolRepository;
            _mapper = mapper;
            _studentRepository = studentRepository;
            _parentsRepository = parentsRepository;
            _classRepository = classRepository;
            _sectionRepository = sectionRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }
        public async Task<Response<IEnumerable<GetParentsListByPaginationBySchoolDto>>> Handle(GetParentsListByPaginationBySchoolQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allParents = await _parentsRepository.ParentsGetPagedReponseAsyncBySchoolId(request.page, request.size, request.SchoolId);            
            var allStudents = await _studentRepository.ListAllAsync();
            List<StudentParentsDto> studentParentTempData = new List<StudentParentsDto>();

            foreach (var parent in allParents)
            {
                var user = await _authenticationService.GetUserById(parent.UserId);
                string[] studentList = parent.StudentId.Split(",");
                foreach (var TempStudent in studentList)
                {
                    studentParentTempData.Add(new StudentParentsDto()
                    {
                        Id = parent.Id,
                        StudentId = Convert.ToInt32(TempStudent),
                        ParentName = parent.ParentName,
                        School = new SchoolDto()
                        {
                            Id = parent.SchoolId,
                            SchoolName = (await _schoolRepository.GetByIdAsync(parent.SchoolId)).SchoolName
                        },
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
                        City = _mapper.Map<CityDto>(parent.City),
                        State = _mapper.Map<StateDto>(parent.State),
                        Zip = _mapper.Map<ZipDto>(parent.Zip),
                        CreatedDate = parent.CreatedDate,
                        IsActive = parent.IsActive,
                        AddressLine1 = parent.AddressLine1,
                        AddressLine2 = parent.AddressLine2,
                        UserType = parent.UserType,
                        Username = user.Username,
                        Email = user.Email,
                        Mobile = parent.Mobile

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
                            prt.School.SchoolName,
                            prt.Class.ClassName,
                            prt.Section.SectionName,
                            prt.CreatedDate,
                            prt.IsActive,
                            prt.AddressLine1,
                            prt.AddressLine2,
                            prt.City.CityName,
                            prt.State.StateName,
                            prt.Zip.Zipcode,
                            prt.Username,
                            prt.UserType,
                            prt.Mobile,
                            prt.Email


                        }).ToList();




            List<GetParentsListByPaginationBySchoolDto> parentsList = new List<GetParentsListByPaginationBySchoolDto>();
            foreach (var parent in data)
            {

                parentsList.Add(new GetParentsListByPaginationBySchoolDto()
                {
                    Id = parent.Id,
                    ParentName = parent.ParentName,
                    StudentName = parent.StudentName,
                    CreatedDate = parent.CreatedDate,
                    SchoolName = parent.SchoolName,
                    ClassName = parent.ClassName,
                    SectionName = parent.SectionName,
                    IsActive = parent.IsActive,
                    AddressLine1 = parent.AddressLine1,
                    AddressLine2 = parent.AddressLine2,
                    CityName = parent.CityName,
                    State = parent.StateName,
                    Zip = parent.Zipcode,
                    UserType = parent.UserType,
                    Username = parent.Username,
                    Email = parent.Email,
                    Mobile = parent.Mobile

                });
            }
            _logger.LogInformation("Handle Completed");
            return new Response<IEnumerable<GetParentsListByPaginationBySchoolDto>>(parentsList, "success");
        }

    }
}
