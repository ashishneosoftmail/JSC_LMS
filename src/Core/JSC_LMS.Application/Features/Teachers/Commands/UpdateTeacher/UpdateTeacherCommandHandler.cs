using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Exceptions;
using JSC_LMS.Application.Models.Authentication;
using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Teachers.Commands.UpdateTeacher
{
    public class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommand, Response<int>>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public UpdateTeacherCommandHandler(IMapper mapper, ITeacherRepository teacherRepository, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _teacherRepository = teacherRepository;
            _authenticationService = authenticationService;
            // _userManager = userManager;
            //  _roleManager = roleManager;
        }

        public async Task<Response<int>> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacherToUpdate = await _teacherRepository.GetByIdAsync(request.updateTeacherDto.Id);
            Response<int> UpdateTeacherCommandResponse = new Response<int>();
            if (teacherToUpdate == null)
            {
                throw new NotFoundException(nameof(JSC_LMS.Domain.Entities.Teacher), request.updateTeacherDto.Id);
            }
            var validator = new UpdateTeacherCommandValidator();
            // var UserClaims = await 
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var userUpdate = new UpdateUserRequest()
            {
                UserId = request.updateTeacherDto.UserId,
                Email = request.updateTeacherDto.Email,                
                Username = request.updateTeacherDto.Username,
               
            };

            var updateUser = await _authenticationService.UpdateUser(userUpdate);
            if (!updateUser.Succeeded)
            {
                UpdateTeacherCommandResponse.Errors = updateUser.Errors;
                UpdateTeacherCommandResponse.Succeeded = false;
                UpdateTeacherCommandResponse.Message = "Email and/or Username already exists.";
                return UpdateTeacherCommandResponse;
            }
            if (updateUser == null) throw new NotFoundException("User Not Found", request.updateTeacherDto.Email);

            teacherToUpdate.UserType = request.updateTeacherDto.UserType;
            teacherToUpdate.UserId = updateUser.UserId;
            teacherToUpdate.TeacherName = request.updateTeacherDto.TeacherName;
            teacherToUpdate.AddressLine1 = request.updateTeacherDto.AddressLine1;
            teacherToUpdate.AddressLine2 = request.updateTeacherDto.AddressLine2;
            teacherToUpdate.ClassId = request.updateTeacherDto.ClassId;
            teacherToUpdate.Mobile = request.updateTeacherDto.Mobile;
            teacherToUpdate.SectionId = request.updateTeacherDto.SectionId;
            teacherToUpdate.CityId = request.updateTeacherDto.CityId;
            teacherToUpdate.StateId = request.updateTeacherDto.StateId;
            teacherToUpdate.ZipId = request.updateTeacherDto.ZipId;
            teacherToUpdate.SubjectId = request.updateTeacherDto.SubjectId;
            teacherToUpdate.IsActive = request.updateTeacherDto.IsActive;
            // = request.updateTeacherDto.Email;

            await _teacherRepository.UpdateAsync(teacherToUpdate);

            return new Response<int>(request.updateTeacherDto.Id, "Updated successfully ");

        }
    }
}
