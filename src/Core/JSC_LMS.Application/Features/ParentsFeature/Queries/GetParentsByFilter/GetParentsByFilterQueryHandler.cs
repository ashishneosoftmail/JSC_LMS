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

            //if (request.StudentName != "Select Student")
            //{
            //    List<Parents> parentData = new List<Parents>();
            //    foreach (var parent in allParents)
            //    {
            //        string[] studentList = parent.StudentId.Split(",");
            //        foreach (var studentname in studentList)
            //        {
            //            var stdnt = await _studentRepository.GetByIdAsync(Convert.ToInt32(studentname));
            //            if (stdnt.StudentName == request.StudentName)
            //            {
            //                parentData.Add(parent);
            //            }
            //        }
            //    }
           
            //}

            if (request.ClassName != "Select Class")
            {
                allParents = allParents.Where<Parents>(x => (x.Class.ClassName == request.ClassName)).ToList();

            }
            if (request.SectionName != "Select Section")
            {
                allParents = allParents.Where<Parents>(x => (x.Section.SectionName == request.SectionName)).ToList();

            }
            if (request.ParentName != "Select Parent")
            {
                allParents = allParents.Where<Parents>(x => (x.ParentName == request.StudentName)).ToList();

            }

            if (request.IsActive)
            {
                allParents = allParents.Where<Parents>(x => x.IsActive == request.IsActive).ToList();
            }
            else
            {
                allParents = allParents.Where<Parents>(x => x.IsActive == request.IsActive).ToList();
            }
            Response<IEnumerable<GetParentsByFilterDto>> responseData = new Response<IEnumerable<GetParentsByFilterDto>>();
            List<GetParentsByFilterDto> parentsList = new List<GetParentsByFilterDto>();
            foreach (var parent in allParents)
            {
                string[] studentList = parent.StudentId.Split(",");
                List<StudentDto> studentData = new List<StudentDto>();
                foreach (var studentname in studentList)
                {
                    var stdnt = await _studentRepository.GetByIdAsync(Convert.ToInt32(studentname));
                    studentData.Add(new StudentDto()
                    {
                        Id = stdnt.Id,
                        StudentName = stdnt.StudentName
                    });
                }


                var user = await _authenticationService.GetUserById(parent.UserId);
                parentsList.Add(new GetParentsByFilterDto()
                {
                    Id = parent.Id,
                    AddressLine1 = parent.AddressLine1,
                    AddressLine2 = parent.AddressLine2,
                    Mobile = parent.Mobile,
                    Username = user.Username,
                    Email = user.Email,
                    IsActive = parent.IsActive,
                    ParentName = parent.ParentName,
                    UserType = parent.UserType,
                    CreatedDate = (DateTime)parent.CreatedDate,
                    City = _mapper.Map<CityDto>(parent.City),
                    State = _mapper.Map<StateDto>(parent.State),
                    Zip = _mapper.Map<ZipDto>(parent.Zip),
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
                    Student = studentData

                });
            }
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetParentsByFilterDto>>(parentsList, "success");
        }


    }
}
