using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Models.Authentication;
using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Students.Commands.UpdateStudent
{

    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Response<int>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        public UpdateStudentCommandHandler(IMapper mapper, IStudentRepository studentRepository, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
            _authenticationService = authenticationService;
        }

        public async Task<Response<int>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var studentToUpdate = await _studentRepository.GetByIdAsync(request.updateStudentDto.Id);
            Response<int> UpdateStudentCommandResponse = new Response<int>();
            if (studentToUpdate == null)
            {
                UpdateStudentCommandResponse.Message = "No User Found";
                return UpdateStudentCommandResponse;
            }

            var validator = new UpdateStudentCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                UpdateStudentCommandResponse.Succeeded = false;
                UpdateStudentCommandResponse.Errors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    UpdateStudentCommandResponse.Errors.Add(error.ErrorMessage);
                }
            }

            var userUpdate = new UpdateUserRequest()
            {
                UserId = request.updateStudentDto.UserId,
                Email = request.updateStudentDto.Email,
                Username = request.updateStudentDto.Username
            };

            var updateUser = await _authenticationService.UpdateUser(userUpdate);
            if (!updateUser.Succeeded)
            {
                UpdateStudentCommandResponse.Errors = updateUser.Errors;
                UpdateStudentCommandResponse.Succeeded = false;
                UpdateStudentCommandResponse.Message = "User Already Exist";
                return UpdateStudentCommandResponse;
            }

            studentToUpdate.ClassId = request.updateStudentDto.ClassId;
            studentToUpdate.SectionId = request.updateStudentDto.SectionId;
            studentToUpdate.StudentName = request.updateStudentDto.StudentName;
            //studentToUpdate.UserType = request.updateStudentDto.UserType;
            studentToUpdate.AddressLine1 = request.updateStudentDto.AddressLine1;
            studentToUpdate.AddressLine2 = request.updateStudentDto.AddressLine2;
            studentToUpdate.SchoolId = request.updateStudentDto.SchoolId;
            studentToUpdate.UserId = updateUser.UserId;
            studentToUpdate.Mobile = request.updateStudentDto.Mobile;
            studentToUpdate.CityId = request.updateStudentDto.CityId;
            studentToUpdate.StateId = request.updateStudentDto.StateId;
            studentToUpdate.ZipId = request.updateStudentDto.ZipId;
            studentToUpdate.IsActive = request.updateStudentDto.IsActive;
            await _studentRepository.UpdateAsync(studentToUpdate);

            return new Response<int>(request.updateStudentDto.Id, "Updated successfully ");
        }
    }
}
