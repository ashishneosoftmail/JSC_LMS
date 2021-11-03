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

namespace JSC_LMS.Application.Features.Principal.Commands.UpdatePrincipal
{
    public class UpdatePrincipalCommandHandler : IRequestHandler<UpdatePrincipalCommand, Response<int>>
    {
        private readonly IPrincipalRepository _principalRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        public UpdatePrincipalCommandHandler(IMapper mapper, IPrincipalRepository principalRepository, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _principalRepository = principalRepository;
            _authenticationService = authenticationService;
        }

        public async Task<Response<int>> Handle(UpdatePrincipalCommand request, CancellationToken cancellationToken)
        {
            var principalToUpdate = await _principalRepository.GetByIdAsync(request.updatePrincipalDto.Id);
            Response<int> UpdatePrincipalCommandResponse = new Response<int>();
            if (principalToUpdate == null)
            {
                UpdatePrincipalCommandResponse.Message = "No User Found";
                return UpdatePrincipalCommandResponse;
            }

            var validator = new UpdatePrincipalCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                UpdatePrincipalCommandResponse.Succeeded = false;
                UpdatePrincipalCommandResponse.Errors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    UpdatePrincipalCommandResponse.Errors.Add(error.ErrorMessage);
                }
            }

            var userUpdate = new UpdateUserRequest()
            {
                UserId = request.updatePrincipalDto.UserId,
                Email = request.updatePrincipalDto.Email,
                Password = request.updatePrincipalDto.Password,
                Username = request.updatePrincipalDto.Username
            };

            var updateUser = await _authenticationService.UpdateUser(userUpdate);
            if (!updateUser.Succeeded)
            {
                UpdatePrincipalCommandResponse.Errors = updateUser.Errors;
                UpdatePrincipalCommandResponse.Succeeded = false;
                UpdatePrincipalCommandResponse.Message = "User Already Exist";
                return UpdatePrincipalCommandResponse;
            }

            principalToUpdate.SchoolId = request.updatePrincipalDto.SchoolId;
            principalToUpdate.AddressLine1 = request.updatePrincipalDto.AddressLine1;
            principalToUpdate.AddressLine2 = request.updatePrincipalDto.AddressLine2;
            principalToUpdate.Name = request.updatePrincipalDto.Name;
            principalToUpdate.UserId = updateUser.UserId;
            principalToUpdate.Mobile = request.updatePrincipalDto.Mobile;
            principalToUpdate.CityId = request.updatePrincipalDto.CityId;
            principalToUpdate.StateId = request.updatePrincipalDto.StateId;
            principalToUpdate.ZipId = request.updatePrincipalDto.ZipId;
            principalToUpdate.IsActive = request.updatePrincipalDto.IsActive;
            await _principalRepository.UpdateAsync(principalToUpdate);

            return new Response<int>(request.updatePrincipalDto.Id, "Updated successfully ");
        }
    }
}
