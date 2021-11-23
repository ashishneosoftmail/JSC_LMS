using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Models.Authentication;
using JSC_LMS.Application.Response;
using JSC_LMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Teachers.Commands.CreateTeacher
{
    public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, Response<CreateTeacherDto>>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        public CreateTeacherCommandHandler(IMapper mapper, ITeacherRepository teacherRepository, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _teacherRepository = teacherRepository;
            _authenticationService = authenticationService;
        }
        public async Task<Response<CreateTeacherDto>> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            var createTeacherCommandResponse = new Response<CreateTeacherDto>();

            var validator = new CreateTeacherCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
            {
                createTeacherCommandResponse.Succeeded = false;
                createTeacherCommandResponse.Errors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createTeacherCommandResponse.Errors.Add(error.ErrorMessage);
                }
            }
            else
            {
                var UserData = new RegistrationRequest()
                {
                    FirstName = request.createTeacherDto.TeacherName,
                    LastName = "T",
                    UserName = request.createTeacherDto.Username,
                    Email = request.createTeacherDto.Email,
                    Password = request.createTeacherDto.Password,
                    RoleName = "Teacher"
                };
                var User = await _authenticationService.RegisterAsync(UserData);
                if (User.UserId == null)
                {

                    createTeacherCommandResponse.Succeeded = false;
                    createTeacherCommandResponse.Message = "Email and/or Username already exists.";
                }
                else
                {
                    var data = new Teacher()
                    {
                        UserId = User.UserId,
                        TeacherName = request.createTeacherDto.TeacherName,
                        AddressLine1 = request.createTeacherDto.AddressLine1,
                        AddressLine2 = request.createTeacherDto.AddressLine2,
                        SubjectId = request.createTeacherDto.SubjectId,
                        ClassId = request.createTeacherDto.ClassId,
                        CityId = request.createTeacherDto.CityId,
                        StateId = request.createTeacherDto.StateId,
                        ZipId = request.createTeacherDto.ZipId,
                        UserType = request.createTeacherDto.UserType,
                        IsActive = request.createTeacherDto.IsActive,
                        SectionId = request.createTeacherDto.SectionId,
                        Mobile = request.createTeacherDto.Mobile,
                        SchoolId = request.createTeacherDto.SchoolId,
                    };
                    var teacher = await _teacherRepository.AddAsync(data);
                    createTeacherCommandResponse.Data = _mapper.Map<CreateTeacherDto>(teacher);
                    createTeacherCommandResponse.Succeeded = true;
                    createTeacherCommandResponse.Message = "success";
                }
            }
            return createTeacherCommandResponse;

        }
    }
}
