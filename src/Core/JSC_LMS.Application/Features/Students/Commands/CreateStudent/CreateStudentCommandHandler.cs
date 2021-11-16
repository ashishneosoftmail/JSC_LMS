using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Models.Authentication;
using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Students.Commands.CreateStudent
{

    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Response<CreateStudentDto>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;


        public CreateStudentCommandHandler(IMapper mapper, IStudentRepository studentRepository, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
            _authenticationService = authenticationService;
        }

        public async Task<Response<CreateStudentDto>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var createStudentCommandResponse = new Response<CreateStudentDto>();

            var validator = new CreateStudentCommandValidation();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                createStudentCommandResponse.Succeeded = false;
                createStudentCommandResponse.Errors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createStudentCommandResponse.Errors.Add(error.ErrorMessage);
                }
            }
            else
            {
                var UserData = new RegistrationRequest() { FirstName = request.createStudentDto.StudentName, 
                    LastName = "S", 
                    UserName = request.createStudentDto.Username, 
                    Email = request.createStudentDto.Email,
                    Password = request.createStudentDto.Password,
                RoleName="Student"};
                
                var User = await _authenticationService.RegisterAsync(UserData);
                if (User.UserId == null)
                {

                    createStudentCommandResponse.Succeeded = false;
                    createStudentCommandResponse.Message = "Email Or Username already registered";
                }
                else
                {
                    var data = new JSC_LMS.Domain.Entities.Students()
                    {
                        ClassId = request.createStudentDto.ClassId,
                        SectionId=request.createStudentDto.SectionId,
                        UserId = User.UserId,
                        AddressLine1 = request.createStudentDto.AddressLine1,
                        AddressLine2 = request.createStudentDto.AddressLine2,
                        StudentName = request.createStudentDto.StudentName,
                        UserType=request.createStudentDto.UserType,
                        Mobile = request.createStudentDto.Mobile,
                        CityId = request.createStudentDto.CityId,
                        StateId = request.createStudentDto.StateId,
                        ZipId = request.createStudentDto.ZipId,
                        IsActive = request.createStudentDto.IsActive
                    };
                    var student = await _studentRepository.AddAsync(data);
                    createStudentCommandResponse.Data = _mapper.Map<CreateStudentDto>(student);
                    createStudentCommandResponse.Succeeded = true;
                    createStudentCommandResponse.Message = "success";
                }
            }

            return createStudentCommandResponse;
        }

    }
}
